using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAPI.Models.Helpers
{
    public class ClientViewModel
    {
        //public CartViewModel(CartViewModel model)
        //{
        //    pizzaId = model.pizzaId;
        //    extraToppings = model.extraToppings;
        //    order = new Order(model.order);
        //}

        public int pizzaId { get; set; }
        public List<Topping> extraToppings { get; set; }
        public Order order { get; set; }
        public OrderItem orderItem { get; set; }
    }
}
