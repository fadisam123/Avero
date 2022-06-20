﻿using Avero.Core.Enum;
using Avero.Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avero.Core.Entities
{
    public class Product : BaseEntity
    {

/*        [Key]
        int product_id { get; set; }*/

        [Required]
        public String? name { get; set; }
        public String? desc { get; set; }
        [Required]
        public int quantity_available { get; set; }
        [Required]
        public double price_per_unit { get; set; }
        public double offer_price { get; set; }
        public DateTime? offer_price_start_date { get; set; } = DateTime.Now;
        public DateTime? offer_price_end_date { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public Rating? rating { get; set; }
        [Range(0, Int32.MaxValue)]
        public int rated_people_count { set; get; } = 0;

        [Required]
        public User? wholesealer { get; set; }
        [Required]
        public Catagory? catagory { get; set; }
        public ICollection<Product_review> product_review { set; get; } = new List<Product_review>();
        public ICollection<Order_details> order_details { set; get; } = new List<Order_details>();
        public ICollection<Product_imgs> product_imgs { set; get; } = new List<Product_imgs>();
    }
}