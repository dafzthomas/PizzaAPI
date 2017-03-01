using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PizzaAPI.Models {
    [DataContract]
    public class Topping {
        [DataMember]
        public int ToppingId { get; set; }

        [DataMember]
        [Required]
        [StringLength(150, ErrorMessage = "The {0} was too long.")]
        [Display(Name = "Topping name")]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [RegularExpression(@"large|medium|small", ErrorMessage = "{0} must be either 'large', 'medium' or 'small'.")]
        [Display(Name = "Topping size")]
        public string Size { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        public virtual List<Pizza> Pizzas { get; set; }
    }
}
