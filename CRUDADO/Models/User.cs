using System.ComponentModel.DataAnnotations;

namespace CRUDADO.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Password should be minimum 5 to maximum 10 character !")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        public string DateOfBirth { get; set; }
    }
}
