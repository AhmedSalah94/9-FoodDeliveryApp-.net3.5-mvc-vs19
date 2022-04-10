using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public partial class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Column("Total Price")]
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
