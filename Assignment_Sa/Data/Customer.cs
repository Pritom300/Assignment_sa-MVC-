using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }


        [MaxLength(100)]
        public string Name { get; set; }


        [MaxLength(100)]
        public string Email { get; set; }


        [MaxLength(20)]
        public string Phone { get; set; }


        [MaxLength(200)]
        public string Address { get; set; }


        // Navigation: sales for this customer
        public ICollection<SaleMaster> Sales { get; set; } = new List<SaleMaster>();
    }
}
