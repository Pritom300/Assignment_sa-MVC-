using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Models
{
    public class SaleDetail
    {
        [Key]
        public int DetailId { get; set; }
        public int Quantity { get; set; }

        // decimal(18,2)
        public decimal UnitPrice { get; set; }


        // decimal(18,2)
        public decimal SubTotal { get; set; }


        // Navigation properties
        public int SaleId { get; set; }
        public SaleMaster? SaleMaster { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}