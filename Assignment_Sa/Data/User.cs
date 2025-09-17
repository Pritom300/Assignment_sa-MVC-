using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Required, MaxLength(50)]
        public string Username { get; set; }


        [Required]
        public string PasswordHash { get; set; }


        [MaxLength(100)]
        public string FullName { get; set; }


        [MaxLength(20)]
        public string Role { get; set; }


        public ICollection<SaleMaster> SalesCreated { get; set; } = new List<SaleMaster>();
    }
}
