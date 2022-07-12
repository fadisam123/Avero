using Avero.Core.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class User : IdentityUser // id, email, passwordHash, phoneNumber, Roles (from base class)
    {
        [Required]
        public String? fname { set; get; }
        [Required]
        public String? lname { set; get; }
        public String? img_name { set; get; }
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
        public String? marker_map_address { set; get; }

        [Required]
        public long neighborhood_id { set; get; }

        [Required]
        [ForeignKey("neighborhood_id")]
        public virtual Neighborhood? neighborhood { set; get; }
        public virtual ICollection<Product> product { set; get; } = new List<Product>();
        // there is no relation in the database for this
        public virtual ICollection<Product_review> product_review { set; get; } = new List<Product_review>();
        public virtual ICollection<Order> order { set; get; } = new List<Order>();
    }
}
