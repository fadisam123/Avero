using Avero.Core.Enum;
using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Order_details : BaseEntity
    {
        public long? order_id { get; set; }
        public long? product_id { get; set; }

        [Required]
        public int quantity { get; set; }
        [Required]
        public Order_state? processing_state { get; set; }


        [ForeignKey("order_id")]
        public virtual Order? order { get; set; }
        [ForeignKey("product_id")]
        public virtual Product? product { get; set; }
    }
}