using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Order : BaseEntity
    {

/*        [Key]
        public int order_id { get; set; }*/

        [Required]
        public DateTime? order_date { get; set; } = DateTime.Now;

        [Required]
        public User? retailer { get; set; }
        public ICollection<Order_details> order_details { set; get; } = new List<Order_details>();
    }
} 