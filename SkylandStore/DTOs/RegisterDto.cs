using System.ComponentModel.DataAnnotations;

namespace SkylandStore.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Required Field")]
        [EmailAddress(ErrorMessage = "InValid Email")]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Phone Number Field Is Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password Field Is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmd Password Field Is Required")]
        [Compare("Password", ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }
    }
}