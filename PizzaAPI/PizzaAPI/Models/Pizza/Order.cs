using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Models
{
    public class Order
    {
        public Order(Order order)
        {
            if (order != null)
            {
                List<OrderItem> orderItems = new List<OrderItem>();

                if (order.OrderItems != null && order.OrderItems.Count > 0)
                {
                    foreach (OrderItem item in order.OrderItems)
                    {
                        orderItems.Add(item);
                    }
                }


                OrderItems = orderItems;
                Price = order.Price;
                Discount = order.Discount;
                CurrentVoucher = order.CurrentVoucher;
                Delivery = order.Delivery;
            } else
            {
                OrderItems = new List<OrderItem>();
            }
            
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