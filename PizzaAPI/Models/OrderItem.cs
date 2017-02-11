using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebSite.Models.PizzaCart
{
    public class OrderItem
    {
        public OrderItem()
        {
            ExtraToppings = new List<Topping>();
        }

        [Key]
        public int OrderItemId { get; set; }

        public virtual Order Order { get; set; }

        public Pizza Pizza { get; set; }

        public List<Topping> ExtraToppings { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Price { get; set; } = 0;
    }
}