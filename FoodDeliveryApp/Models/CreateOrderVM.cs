using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryApp.Models
{
    public class CreateOrderVM
    {
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}