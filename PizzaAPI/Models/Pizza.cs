using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebSite.Models.PizzaCart {
    public class Pizza {

        [Key]
        public int PizzaId { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The {0} was too long.")]
        [Display(Name = "Pizza name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"large|medium|small", ErrorMessage = "{0} must be either 'large', 'medium' or 'small'.")]
        [Display(Name = "Pizza size")]
        public string Size { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        public virtual List<Topping> Toppings { get; set; }
    }
}