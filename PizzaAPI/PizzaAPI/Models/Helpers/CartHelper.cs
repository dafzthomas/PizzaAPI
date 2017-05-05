using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAPI.Models.Helpers
{
    class CartHelper
    {
        private static PizzaOrderContext db = DatabaseContextHelper.Db;

        public static void ResetCart(Order cart)
        {
            cart = new Order(null);
        }

        public static void Add(int pizzaId, string selectedToppingString, Order cart)
        {
            OrderItem order = new OrderItem();

            order.Pizza = db.Pizzas.Find(pizzaId);

            order.Price += order.Pizza.Price;

            if (selectedToppingString != null)
            {
                var selectedToppings = selectedToppingString.Split(',');

                foreach (string toppingId in selectedToppings)
                {
                    Topping topping = db.Toppings.Find(Convert.ToInt32(toppingId));

                    order.ExtraToppings.Add(topping);
                    order.Price += topping.Price;
                }
            }

            cart.OrderItems.Add(order);

            AddDealToCart("", cart);
        }

        public static void Remove(Pizza pizza, Order cart)
        {
            var itemToRemove = cart.OrderItems.Single(r => r.Pizza.PizzaId == pizza.PizzaId);

            cart.OrderItems.Remove(itemToRemove);
        }

        public static void Submit(string userId, Order cart)
        {
            cart.UserId = userId;

            foreach (var item in cart.OrderItems)
            {
                cart.Price += item.Price;
            }

            db.Orders.Add(cart);
            db.SaveChanges();
        }

        public static void ApplyDelivery(bool Delivery, Order cart)
        {
            cart.Delivery = Delivery;
        }

        public static void ReorderOrder(int orderId, Order currentCart)
        {
            ResetCart(currentCart);
            var cartToAdd = db.Orders.Find(orderId);

            foreach (var item in cartToAdd.OrderItems)
            {
                currentCart.OrderItems.Add(new OrderItem
                {
                    Pizza = item.Pizza,
                    ExtraToppings = item.ExtraToppings,
                    UserId = item.UserId,
                    Price = item.Price
                });
            }
        }

        public static void AddDealToCart(string voucher, Order cart)
        {
            switch (voucher)
            {
                case "2FOR1TUE":
                    if (cart.OrderItems.Where(o => o.Pizza.Size == "medium" || o.Pizza.Size == "large").Count() == 2)
                    {
                        decimal lowestCost = cart.OrderItems.Min(o => o.Price);

                        cart.Discount = lowestCost;
                        cart.CurrentVoucher = voucher;
                    }

                    break;
                case "3FOR2THUR":
                    if (cart.OrderItems.Where(o => o.Pizza.Size == "medium").Count() == 3)
                    {
                        decimal lowestCost = cart.OrderItems.Min(o => o.Price);

                        cart.Discount = lowestCost;
                        cart.CurrentVoucher = voucher;
                    }

                    break;
                case "FAMFRIDAYCOLL":
                    if (cart.OrderItems.Where(o => o.Pizza.Size == "medium" && o.Pizza.Name != "Create Your Own").Count() == 4 &&
                        !cart.Delivery)
                    {
                        decimal total = cart.OrderItems.Sum(o => o.Price);

                        var discount = total - 30;

                        cart.Discount = discount;
                        cart.CurrentVoucher = voucher;
                    }
                    break;
                case "2LARGECOLL":
                    if (cart.OrderItems.Where(o => o.Pizza.Size == "large" && o.Pizza.Name != "Create Your Own").Count() == 2 &&
                        !cart.Delivery)
                    {
                        decimal total = cart.OrderItems.Sum(o => o.Price);

                        var discount = total - 25;

                        cart.Discount = discount;
                        cart.CurrentVoucher = voucher;
                    }
                    break;
                case "2MEDIUMCOLL":
                    if (cart.OrderItems.Where(o => o.Pizza.Size == "medium" && o.Pizza.Name != "Create Your Own").Count() == 2 &&
                        !cart.Delivery)
                    {
                        decimal total = cart.OrderItems.Sum(o => o.Price);

                        var discount = total - 25;

                        cart.Discount = discount;
                        cart.CurrentVoucher = voucher;
                    }
                    break;
                case "2SMALLCOLL":
                    if (cart.OrderItems.Where(o => o.Pizza.Size == "small" && o.Pizza.Name != "Create Your Own").Count() == 2 &&
                        !cart.Delivery)
                    {
                        decimal total = cart.OrderItems.Sum(o => o.Price);

                        var discount = total - 12;

                        cart.Discount = discount;
                        cart.CurrentVoucher = voucher;
                    }
                    break;
                default:
                    cart.Discount = 0;
                    cart.CurrentVoucher = "";
                    break;
            }
        }
    }
}
