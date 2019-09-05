using System.ComponentModel.DataAnnotations;

namespace CRUDADO.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Msg { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }

        [Required]
        public string DateOfBirth { get; set; }
    }
}
