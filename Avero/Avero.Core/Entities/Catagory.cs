using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;

namespace Avero.Core.Entities
{
    public class Catagory: BaseEntity
    {

/*        [Key]
        int catagory_id { set; get; }*/

        [Required]
        public String? name { set; get; }
        // add img  prob

        public ICollection<Product> product { set; get; } = new List<Product>();
    }
}