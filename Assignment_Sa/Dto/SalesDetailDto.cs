using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Dto
{
    public class SalesDetailDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => UnitPrice * Quantity;

    }
}
