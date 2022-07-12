using System.ComponentModel.DataAnnotations;


namespace Avero.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"(?:(.)(?<=^(?:(?!\1).)*\1)(?=(?:(?!\1).)*$).*?){2,}", ErrorMessage = "Password must contain at least 2 unique characters")]
        [MinLength(6, ErrorMessage = "Password length must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
