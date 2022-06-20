using Avero.Core.Enum;
using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Order_details : BaseEntity
    {

/*        [ForeignKey("order_id")]
        public int order_id { get; set; }
        [ForeignKey("product_id")]
        public int product_id { get; set; }*/

        [Required]
        public int quantity { get; set; }
        [Required]
        public Order_state? processing_state { get; set; }

/*        [Required]*/
        public Order? order { get; set; }
/*        [Required]*/
        public Product? product { get; set; }
    }
}