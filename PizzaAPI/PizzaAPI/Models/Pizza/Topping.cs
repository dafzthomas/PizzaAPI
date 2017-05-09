using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PizzaAPI.Models {
    public class Topping {
        [Key]
        public int ToppingId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public Decimal Price { get; set; }
        public virtual List<Pizza> Pizzas { get; set; }
    }
}
