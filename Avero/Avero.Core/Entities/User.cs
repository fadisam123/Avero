using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Core.Entities
{
    public class User : IdentityUser // id, email, passwordHash, phoneNumber, Roles (from base class)
    {
        String? fname { set; get; }
        String? lname { set; get; }
        // add img attr
        double latitude { set; get; }
        double longitude { set; get; }
        String? street_name { set; get; }
        DateTime? registered_at { set; get; }
        DateTime? last_login { set; get; }

        public ICollection<Rating> ratings { set; get; } = new List<Rating>();
        Neighborhood? neighborhood_id { set; get; }
    }
}
