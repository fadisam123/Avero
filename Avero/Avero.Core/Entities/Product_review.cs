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


        [Required]
        public Product? product { get; set; }
        public User? user { get; set; }
    }
}