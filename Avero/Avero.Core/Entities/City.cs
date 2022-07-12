using Avero.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Core.Entities
{
    public class City : BaseEntity
    {

/*        [Key]
        int city_id { get; set; }*/

        [Required]
        public string? name { get; set; }

        public virtual ICollection<Neighborhood> neighborhoods { get; set; } = new List<Neighborhood>();
    } 
}