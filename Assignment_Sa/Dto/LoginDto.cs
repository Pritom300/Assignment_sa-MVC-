using System.ComponentModel.DataAnnotations;

namespace Assignment_Sa.Dto
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
