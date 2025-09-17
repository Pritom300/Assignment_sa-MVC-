using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }


        [MaxLength(100)]
        public string Name { get; set; }


        
        public decimal UnitPrice { get; set; }


        public int Stock { get; set; }


        // Navigation: sale details that reference this product
        public ICollection<SaleDetail> SalesDetails { get; set; } = new List<SaleDetail>();
    }
}
