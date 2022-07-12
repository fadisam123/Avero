using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;

namespace Avero.Core.Entities
{
    public class Catagory: BaseEntity
    {

        [Required]
        public String? name { set; get; }
        public string? img_name { set; get; }

        public virtual ICollection<Product> product { set; get; } = new List<Product>();
    }
}