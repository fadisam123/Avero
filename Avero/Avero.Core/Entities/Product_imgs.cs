using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Product_imgs : BaseEntity
    {
        public String? img_name { get; set; }
        [Required]
        public long product_id { get; set; }

        [Required]
        [ForeignKey("product_id")]
        public virtual Product? product { get; set; }
    }
}