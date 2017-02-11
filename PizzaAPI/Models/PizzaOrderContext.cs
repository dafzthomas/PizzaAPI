using System.Data.Entity;

namespace PizzaWebSite.Models.PizzaCart {
    public class PizzaOrderContext : ApplicationDbContext {
        public PizzaOrderContext() { }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
    }
}