using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Models {
    public class Pizza {
        [Key]
        public int PizzaId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public Decimal Price { get; set; }
        public virtual List<Topping> Toppings { get; set; }
    }
}