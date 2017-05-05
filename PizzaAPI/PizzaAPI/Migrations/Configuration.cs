namespace PizzaAPI.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzaOrderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PizzaOrderContext context)
        {
            var cheese_s = new Topping { ToppingId = 1, Name = "Cheese", Size = "small", Price = 0.90M };
            var cheese_m = new Topping { ToppingId = 2, Name = "Cheese", Size = "medium", Price = 1.00M };
            var cheese_l = new Topping { ToppingId = 3, Name = "Cheese", Size = "large", Price = 1.15M };
            var tom_s = new Topping { ToppingId = 4, Name = "Tomato sauce", Size = "small", Price = 0.90M };
            var tom_m = new Topping { ToppingId = 5, Name = "Tomato sauce", Size = "medium", Price = 1.00M };
            var tom_l = new Topping { ToppingId = 6, Name = "Tomato sauce", Size = "large", Price = 1.15M };
            var pepp_s = new Topping { ToppingId = 7, Name = "Pepperoni", Size = "small", Price = 0.90M };
            var pepp_m = new Topping { ToppingId = 8, Name = "Pepperoni", Size = "medium", Price = 1.00M };
            var pepp_l = new Topping { ToppingId = 9, Name = "Pepperoni", Size = "large", Price = 1.15M };
            var ham_s = new Topping { ToppingId = 10, Name = "Ham", Size = "small", Price = 0.90M };
            var ham_m = new Topping { ToppingId = 11, Name = "Ham", Size = "medium", Price = 1.00M };
            var ham_l = new Topping { ToppingId = 12, Name = "Ham", Size = "large", Price = 1.15M };
            var chick_s = new Topping { ToppingId = 13, Name = "Chicken", Size = "small", Price = 0.90M };
            var chick_m = new Topping { ToppingId = 14, Name = "Chicken", Size = "medium", Price = 1.00M };
            var chick_l = new Topping { ToppingId = 15, Name = "Chicken", Size = "large", Price = 1.15M };
            var beef_s = new Topping { ToppingId = 16, Name = "Minced beef", Size = "small", Price = 0.90M };
            var beef_m = new Topping { ToppingId = 17, Name = "Minced beef", Size = "medium", Price = 1.00M };
            var beef_l = new Topping { ToppingId = 18, Name = "Minced beef", Size = "large", Price = 1.15M };
            var onion_s = new Topping { ToppingId = 19, Name = "Onions", Size = "small", Price = 0.90M };
            var onion_m = new Topping { ToppingId = 20, Name = "Onions", Size = "medium", Price = 1.00M };
            var onion_l = new Topping { ToppingId = 21, Name = "Onions", Size = "large", Price = 1.15M };
            var gpepp_s = new Topping { ToppingId = 22, Name = "Green peppers", Size = "small", Price = 0.90M };
            var gpepp_m = new Topping { ToppingId = 23, Name = "Green peppers", Size = "medium", Price = 1.00M };
            var gpepp_l = new Topping { ToppingId = 24, Name = "Green peppers", Size = "large", Price = 1.15M };
            var mush_s = new Topping { ToppingId = 25, Name = "Mushrooms", Size = "small", Price = 0.90M };
            var mush_m = new Topping { ToppingId = 26, Name = "Mushrooms", Size = "medium", Price = 1.00M };
            var mush_l = new Topping { ToppingId = 27, Name = "Mushrooms", Size = "large", Price = 1.15M };
            var sweetcorn_s = new Topping { ToppingId = 28, Name = "Sweetcorn", Size = "small", Price = 0.90M };
            var sweetcorn_m = new Topping { ToppingId = 29, Name = "Sweetcorn", Size = "medium", Price = 1.00M };
            var sweetcorn_l = new Topping { ToppingId = 30, Name = "Sweetcorn", Size = "large", Price = 1.15M };
            var jalp_s = new Topping { ToppingId = 31, Name = "Jalapeno peppers", Size = "small", Price = 0.90M };
            var jalp_m = new Topping { ToppingId = 32, Name = "Jalapeno peppers", Size = "medium", Price = 1.00M };
            var jalp_l = new Topping { ToppingId = 33, Name = "Jalapeno peppers", Size = "large", Price = 1.15M };
            var pineapp_s = new Topping { ToppingId = 34, Name = "Pineapple", Size = "small", Price = 0.90M };
            var pineapp_m = new Topping { ToppingId = 35, Name = "Pineapple", Size = "medium", Price = 1.00M };
            var pineapp_l = new Topping { ToppingId = 36, Name = "Pineapple", Size = "large", Price = 1.15M };
            var saus_s = new Topping { ToppingId = 37, Name = "Sausage", Size = "small", Price = 0.90M };
            var saus_m = new Topping { ToppingId = 38, Name = "Sausage", Size = "medium", Price = 1.00M };
            var saus_l = new Topping { ToppingId = 39, Name = "Sausage", Size = "large", Price = 1.15M };
            var bacon_s = new Topping { ToppingId = 40, Name = "Bacon", Size = "small", Price = 0.90M };
            var bacon_m = new Topping { ToppingId = 41, Name = "Bacon", Size = "medium", Price = 1.00M };
            var bacon_l = new Topping { ToppingId = 42, Name = "Bacon", Size = "large", Price = 1.15M };

            List<Pizza> pizzas = new List<Pizza> {
                new Pizza { PizzaId = 1, Name = "Original", Size = "small", Price = 8.00M, Toppings = new List<Topping> {cheese_s, tom_s } },
                new Pizza { PizzaId = 2, Name = "Original", Size = "medium", Price = 9.00M, Toppings = new List<Topping> {cheese_m, tom_m }  },
                new Pizza { PizzaId = 3, Name = "Original", Size = "large", Price = 11.00M, Toppings = new List<Topping> {cheese_l, tom_l }  },
                new Pizza { PizzaId = 4, Name = "Gimme the Meat", Size = "small", Price = 11.00M, Toppings = new List<Topping> { pepp_s, ham_s, chick_s, beef_s, saus_s, bacon_s } },
                new Pizza { PizzaId = 5, Name = "Gimme the Meat", Size = "medium", Price = 14.50M, Toppings = new List<Topping> { pepp_m, ham_m, chick_m, beef_m, saus_m, bacon_m } },
                new Pizza { PizzaId = 6, Name = "Gimme the Meat", Size = "large", Price = 16.50M, Toppings = new List<Topping> { pepp_l, ham_l, chick_l, beef_l, saus_l, bacon_l } },
                new Pizza { PizzaId = 7, Name = "Veggie Delight", Size = "small", Price = 10.00M, Toppings = new List<Topping> { onion_s, gpepp_s, mush_s, sweetcorn_s } },
                new Pizza { PizzaId = 8, Name = "Veggie Delight", Size = "medium", Price = 13.00M, Toppings = new List<Topping> { onion_m,  gpepp_m, mush_m, sweetcorn_m } },
                new Pizza { PizzaId = 9, Name = "Veggie Delight", Size = "large", Price = 15.00M, Toppings = new List<Topping> { onion_l, gpepp_l, mush_l, sweetcorn_l } },
                new Pizza { PizzaId = 10, Name = "Create Your Own", Size = "small", Price = 8.00M, Toppings = new List<Topping> {cheese_s, tom_s } },
                new Pizza { PizzaId = 11, Name = "Create Your Own", Size = "medium", Price = 9.00M, Toppings = new List<Topping> {cheese_m, tom_m } },
                new Pizza { PizzaId = 12, Name = "Create Your Own", Size = "large", Price = 11.00M, Toppings = new List<Topping> {cheese_l, tom_l } },
                new Pizza { PizzaId = 13, Name = "Make Mine Hot", Size = "small", Price = 11.00M, Toppings = new List<Topping> { chick_s, onion_s, gpepp_s, jalp_s } },
                new Pizza { PizzaId = 14, Name = "Make Mine Hot", Size = "medium", Price = 13.00M, Toppings = new List<Topping> { chick_m, onion_m, gpepp_m, jalp_m } },
                new Pizza { PizzaId = 15, Name = "Make Mine Hot", Size = "large", Price = 15.00M, Toppings = new List<Topping> { chick_l, onion_l, gpepp_l, jalp_l } }
            };
            pizzas.ForEach(pizza => context.Pizzas.AddOrUpdate(p => p.PizzaId, pizza));

            context.SaveChanges();
        }
    }
}
