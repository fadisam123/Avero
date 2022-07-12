using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Order : BaseEntity
    {

        [Required]
        public DateTime? order_date { get; set; } = DateTime.Now;
        [Required]
        public String? retailer_id { get; set; }

        [Required]
        [ForeignKey("retailer_id")]
        public virtual User? retailer { get; set; }
        public virtual ICollection<Order_details> order_details { set; get; } = new List<Order_details>();
    }
} 