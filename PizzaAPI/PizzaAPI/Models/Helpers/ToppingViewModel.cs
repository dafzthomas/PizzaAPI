using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAPI.Models.Helpers
{
    class ToppingViewModel
    {
        public ToppingViewModel(Topping topping)
        {
            ToppingId = topping.ToppingId;
            Name = topping.Name;
            Size = topping.Size;
            Price = topping.Price;
        }

        public int ToppingId { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public Decimal Price { get; set; }
    }
}
