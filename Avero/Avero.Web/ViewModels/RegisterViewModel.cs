using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Avero.Web.ViewModels
{
    public class RegisterViewModel
    {
        public String? id { set; get; }
        [Required]
        [Display(Name = "Name")]
        public String? name { set; get; }

        public String? street_name { set; get; }
        [Required(ErrorMessage = "Please Select Your Neighborhood")]
        public long neighborhood { set; get; }

        [Display(Name = "I am a wholesealer")]
        public Boolean is_wholesealer { set; get; } = false;

        public IFormFile? Photo { get; set; }

        public double latitude { set; get; }
        public double longitude { set; get; }
        public String? marker_map_address { set; get; }

        [Required]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b", ErrorMessage = "Invalid email")]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string? Email { get; set; }

        [MaxLength(14)]
        public string? Phone { get; set; }


        [Required]
        [RegularExpression(@"(?:(.)(?<=^(?:(?!\1).)*\1)(?=(?:(?!\1).)*$).*?){2,}", ErrorMessage = "Password must contain at least 2 unique characters")]
        [MinLength(6, ErrorMessage = "Password length must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
