using Avero.Core.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Core.Entities
{
    public class User : IdentityUser // id, email, passwordHash, phoneNumber, Roles (from base class)
    {
        [Required]
        public String? fname { set; get; }
        [Required]
        public String? lname { set; get; }
        public String? img_path { set; get; }
        public double latitude { set; get; }
        public double longitude { set; get; }
        public String? street_name { set; get; }
        [Required]
        public DateTime? registered_at { set; get; } = DateTime.Now;
        [Required]
        public DateTime? last_login { set; get; }
        public Rating? rating { set; get; }
        [Range(0, Int32.MaxValue)]
        public int rated_people_count { set; get; } = 0;

        [Required]
        public Neighborhood? neighborhood { set; get; }
        public ICollection<Product> product { set; get; } = new List<Product>();
        public ICollection<Product_review> product_review { set; get; } = new List<Product_review>();
        public ICollection<Order> order { set; get; } = new List<Order>();
    }
}
