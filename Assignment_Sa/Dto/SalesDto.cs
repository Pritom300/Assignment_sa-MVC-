using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Dto
{
    public class SalesDto
    {
        [Required]
        public int CustomerId { get; set; }
        public List<SalesDetailDto> SalesDetails { get; set; } = new List<SalesDetailDto>();
        public decimal TotalAmount => SalesDetails.Sum(d => d.SubTotal);
    }
}
