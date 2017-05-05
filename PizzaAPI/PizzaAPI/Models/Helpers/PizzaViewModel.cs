using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAPI.Models.Helpers
{
    class PizzaViewModel
    {
        public PizzaViewModel(Pizza pizza)
        {
            PizzaId = pizza.PizzaId;
            Name = pizza.Name;
            Size = pizza.Size;
            Price = pizza.Price;

            List<ToppingViewModel> toppings = new List<ToppingViewModel>();
            
            foreach (Topping topping in pizza.Toppings)
            {
                toppings.Add(new ToppingViewModel(topping));
            }
            Toppings = toppings;
        }
        public int PizzaId { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public Decimal Price { get; set; }

        public virtual List<ToppingViewModel> Toppings { get; set; }
    }
}
