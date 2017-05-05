﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaAPI.Models
{
    public class OrderItem
    {
        private static List<bool> UsedCounter = new List<bool>();

        private int GetAvailableIndex()
        {
            for (int i = 0; i < UsedCounter.Count; i++)
            {
                if (UsedCounter[i] == false)
                {
                    return i;
                }
            }

            // Nothing available.
            return -1;
        }

        public OrderItem()
        {
            int nextIndex = GetAvailableIndex();
            if (nextIndex == -1)
            {
                nextIndex = UsedCounter.Count;
                UsedCounter.Add(true);
            }

            OrderItemId = nextIndex;
            
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