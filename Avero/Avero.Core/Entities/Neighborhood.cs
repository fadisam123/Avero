using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Neighborhood : BaseEntity
    {

/*      [Key]
        int neighborhood_id { get; set; }*/

        [Required]
        public String? name { get; set; }
        [Required]
        public long city_id { get; set; }

        [Required]
        [ForeignKey("city_id")]
        public virtual City? city { get; set; }
        public virtual ICollection<User> users { get; set; } = new List<User>();
    }
}