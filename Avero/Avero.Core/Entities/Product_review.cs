using Avero.Core.Enum;
using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Product_review : BaseEntity
    {
        public DateTime created_at{ get; set; } = DateTime.Now;
        [Required]
        public String? content{ get; set; }

        public long product_id { get; set; }
        public string? user_id { get; set; }


        [ForeignKey("product_id")]
        public virtual Product? product { get; set; }
        [ForeignKey("user_id")]
        public virtual User? user { get; set; }     // there is no relation in the database
    }
}