using System.Data.Entity;

namespace PizzaAPI.Models {
    public class PizzaOrderContext : ApplicationDbContext {
        public PizzaOrderContext() : base() { }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}