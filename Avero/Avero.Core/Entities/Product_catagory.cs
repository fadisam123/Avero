using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Product_catagory : BaseEntity
    {
        public long? catagory_id { get; set; }
        public long? product_id { get; set; }

        [ForeignKey("catagory_id")]
        public virtual Catagory? catagory { get; set; }
        [ForeignKey("product_id")]
        public virtual Product? product { get; set; }
    }
}