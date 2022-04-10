using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public partial class OrderItem
    {
        [Key]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ItemId { get; set; }

        public decimal Quntity { get; set; }
        
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
