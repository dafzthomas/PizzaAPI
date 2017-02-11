using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebSite.Models.PizzaCart
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
            Price = 0;
        }

        [Key]
        public int OrderId { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

        public string UserId { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Discount { get; set; }

        public string CurrentVoucher { get; set; }

        public bool Delivery { get; set; }

    }
}