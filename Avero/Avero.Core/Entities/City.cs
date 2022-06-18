using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Core.Entities
{
    public class City
    {
        int city_id { get; set; }
        string? name { get; set; }

        public ICollection<Neighborhood> neighborhoods { get; set; } = new List<Neighborhood>();
    } 
}