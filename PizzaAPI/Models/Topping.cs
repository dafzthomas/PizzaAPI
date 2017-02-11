using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebSite.Models.PizzaCart {
    public class Topping {

        [Key]
        public int ToppingId { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The {0} was too long.")]
        [Display(Name = "Topping name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"large|medium|small", ErrorMessage = "{0} must be either 'large', 'medium' or 'small'.")]
        [Display(Name = "Topping size")]
        public string Size { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        public virtual List<Pizza> Pizzas { get; set; }
    }
}
