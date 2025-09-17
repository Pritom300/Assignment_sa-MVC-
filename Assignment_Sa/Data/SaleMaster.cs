using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Models
{
    public class SaleMaster
    {
        [Key]
        public int SaleId { get; set; }

        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

       
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
        public ICollection<SaleDetail> SalesDetails { get; set; } = new List<SaleDetail>();
    }
}