using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAPI.Models.Helpers
{
    class OrderItemViewModel
    {

        public OrderItemViewModel(OrderItem orderItem)
        {
            List<ToppingViewModel> extraToppings = new List<ToppingViewModel>();

            foreach (Topping topping in orderItem.ExtraToppings)
            {
                extraToppings.Add(new ToppingViewModel(topping));
            }

            OrderItemId = orderItem.OrderItemId;
            ExtraToppings = extraToppings;
            Pizza = new PizzaViewModel(orderItem.Pizza);
            UserId = orderItem.UserId;
            Price = orderItem.Price;
        }
        
        public int OrderItemId { get; set; }
        public List<ToppingViewModel> ExtraToppings { get; set; }
        public PizzaViewModel Pizza { get; set; }
        public int UserId { get; set; }
        public Decimal Price { get; set; } = 0;
    }
}
