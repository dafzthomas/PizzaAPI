using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAPI.Models.Helpers
{
    class OrderViewModel
    {
        public OrderViewModel(Order order)
        {
            List<OrderItemViewModel> orderItems = new List<OrderItemViewModel>();
            
            foreach (OrderItem orderItem in order.OrderItems)
            {
                orderItems.Add(new OrderItemViewModel(orderItem));
            }
            
            OrderId = order.OrderId;
            OrderItems = orderItems;
            UserId = order.UserId;
            Price = order.Price;
            Discount = order.Discount;
            CurrentVoucher = order.CurrentVoucher;
            Delivery = order.Delivery;
        }
        
        public int OrderId { get; set; }
        public virtual List<OrderItemViewModel> OrderItems { get; set; }
        public string UserId { get; set; }
        public Decimal Price { get; set; }
        public Decimal Discount { get; set; }
        public string CurrentVoucher { get; set; }
        public bool Delivery { get; set; }
    }
}
