using Assignment_Sa.Documents;
using Assignment_Sa.Dto;
using Assignment_Sa.Interfaces;
using Assignment_Sa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using SalesApp.Filters;

namespace Assignment_Sa.Controllers
{
    
    public class SalesController : Controller
    {
        private readonly ISalesMasterRepository _salesMasterRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IProductRepository _productRepo;
        public SalesController(ISalesMasterRepository salesMasterRepo, ICustomerRepository customerRepo, IProductRepository productRepo)
        {
            _salesMasterRepo = salesMasterRepo;
            _customerRepo = customerRepo;
            _productRepo = productRepo;
        }

        // Create Sale Form
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = await _customerRepo.GetAllAsync();
            ViewBag.Products = await _productRepo.GetAllAsync();
            return View(new SalesDto());
        }


        // Save Sale
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesDto model)
        {
            if (!ModelState.IsValid || model.SalesDetails.Count <= 0)
            {
                ViewBag.Customers = await _customerRepo.GetAllAsync();
                ViewBag.Products = await _productRepo.GetAllAsync();
                return View(model);
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var salesMaster = new SaleMaster
            {
                CustomerId = model.CustomerId,
                SaleDate = DateTime.Now,
                CreatedByUserId = userId.Value,
                TotalAmount = model.TotalAmount
            };

            foreach (var detail in model.SalesDetails)
            {
                var product = await _productRepo.GetByIdAsync(detail.ProductId);
                if (product != null)
                {
                    salesMaster.SalesDetails.Add(new SaleDetail
                    {
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        UnitPrice = product.UnitPrice,
                        SubTotal = product.UnitPrice * detail.Quantity
                    });

                    // Deduct stock
                    await _productRepo.UpdateStockAsync(detail.ProductId, detail.Quantity);
                }
            }

            await _salesMasterRepo.AddAsync(salesMaster);

            return RedirectToAction("Index");
        }


        // GET Sales List
        
        [HttpGet]
        public async Task<IActionResult> Index(string searchCustomer, DateTime? fromDate, DateTime? toDate)
        {
            var sales = await _salesMasterRepo.GetAllAsync();

            // filter by customer name
            if (!string.IsNullOrEmpty(searchCustomer))
            {
                sales = sales.Where(s => s.Customer != null &&
                                         s.Customer.Name.Contains(searchCustomer,
                                         StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // filter by date range
            if (fromDate.HasValue)
            {
                sales = sales.Where(s => s.SaleDate >= fromDate.Value).ToList();
            }
            if (toDate.HasValue)
            {
                sales = sales.Where(s => s.SaleDate <= toDate.Value).ToList();
            }

            return View(sales);
        }

      
        public IActionResult Details(int id)
        {
            var sale = _salesMasterRepo.GetByIdAsync(id).Result;

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }


        [HttpGet]
        [AuthorizeUser("Admin")]
        public async Task<IActionResult> ExportToExcel()
        {
            var sales = await _salesMasterRepo.GetAllAsync();

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sales");

            worksheet.Cell(1, 1).Value = "Sale ID";
            worksheet.Cell(1, 2).Value = "Customer";
            worksheet.Cell(1, 3).Value = "Date";
            worksheet.Cell(1, 4).Value = "Total";

            int row = 2;
            foreach (var s in sales)
            {
                worksheet.Cell(row, 1).Value = s.SaleId;
                worksheet.Cell(row, 2).Value = s.Customer?.Name;
                worksheet.Cell(row, 3).Value = s.SaleDate.ToString("yyyy-MM-dd");
                worksheet.Cell(row, 4).Value = s.TotalAmount;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "SalesList.xlsx");
        }

        //Make Pdf Invoice
        public async Task<IActionResult> Invoice(int id)
        {
            var sale = await _salesMasterRepo.GetByIdAsync(id);
            if (sale == null)
                return NotFound();

            var document = new InvoiceDocument(sale);
            var pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", $"Invoice_{id}.pdf");
        }


    }
}
