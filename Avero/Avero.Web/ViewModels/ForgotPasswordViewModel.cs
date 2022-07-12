
using System.ComponentModel.DataAnnotations;

namespace Avero.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b", ErrorMessage = "Invalid email")]
        public string? Email { get; set; }
    }
}
