using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Neighborhood : BaseEntity
    {

/*        [Key]
        int neighborhood_id { get; set; }*/

        [Required]
        public String? name { get; set; }

        [Required]
        public City? city { get; set; }
        public ICollection<User> user { get; set; } = new List<User>();
    }
}