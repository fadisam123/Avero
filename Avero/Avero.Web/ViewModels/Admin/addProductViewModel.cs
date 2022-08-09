using Avero.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Avero.Web.ViewModels.Admin
{
    public class CatagoriesViewModel
    {
        public long? catagory_id { get; set; }
        public string? catagory_name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class addProductViewModel
    {
        public Product? product { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Product Name")]
        public string? name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string? desc { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The quantity must be greater than zero.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid quantity, Enter positive numbers only")]
        [Display(Name = "Quantity Available")]
        public int quantity_available { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The price field must be greater than zero.")]
        [RegularExpression(@"^[0-9]\d*(\.\d+)?$", ErrorMessage = "Invalid price, Enter positive numbers only")]
        [Display(Name = "Price Per Unit")]
        public double price_per_unit { get; set; }

        [Display(Name = "Make Discount")]
        public Boolean make_discount { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The price field must be greater than zero.")]
        [RegularExpression(@"^[0-9]\d*(\.\d+)?$", ErrorMessage = "Invalid price, Enter positive numbers only")]
        public double offer_price { get; set; }
        public DateTime? offer_price_start_date{ get; set; }
        public DateTime? offer_price_end_date{ get; set; }

        public DateTime? created_at{ get; set; }

        public String? wholesealer_id{ get; set; }

        public List<IFormFile>? imgs { get; set; }

        public List<CatagoriesViewModel>? catagories { get; set; }

    }
}
