using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            cart = new Order();
        }

        public static void Add(int pizzaId, List<int> extraToppings, Order order)
        {
            

            OrderItem orderItem = new OrderItem();
            orderItem.Pizza = db.Pizzas.Find(pizzaId);
            orderItem.Price = orderItem.Pizza.Price;

            if (extraToppings != null)
            {
                foreach (int toppingId in extraToppings)
                {
                    Topping topping = db.Toppings.Find(toppingId);
                    orderItem.ExtraToppings.Add(topping);
                    orderItem.Price += topping.Price;
                }
            }

            order.OrderItems.Add(orderItem);
            order.Price += orderItem.Price;

            if (order.CurrentVoucher.Length > 0)
            {
                AddDealToCart(order.CurrentVoucher, order);
            }
        }

        public static void Remove(int orderItemId, Order order)
        {
            string voucher = order.CurrentVoucher;

            var itemToRemove = order.OrderItems.Single(r => r.OrderItemId == orderItemId);

            order.OrderItems.Remove(itemToRemove);
            order.Price -= itemToRemove.Price;

            CartHelper.AddDealToCart(voucher, order);
        }

        public static void Submit(string userId, Order cart)
        {
            cart.UserId = userId;
            cart.Price = 0;

            foreach (var item in cart.OrderItems)
            {
                cart.Price += item.Price;
            }

            //db.Entry(cart).State = EntityState.Detached;

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
                    } else
                    {
                        cart.Discount = 0;
                        cart.CurrentVoucher = "";
                        break;
                    }

                    break;
                case "3FOR2THUR":
                    if (cart.OrderItems.Where(o => o.Pizza.Size == "medium").Count() == 3)
                    {
                        decimal lowestCost = cart.OrderItems.Min(o => o.Price);

                        cart.Discount = lowestCost;
                        cart.CurrentVoucher = voucher;
                    } else
                    {
                        cart.Discount = 0;
                        cart.CurrentVoucher = "";
                        break;
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
                    } else
                    {
                        cart.Discount = 0;
                        cart.CurrentVoucher = "";
                        break;
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
                    } else
                    {
                        cart.Discount = 0;
                        cart.CurrentVoucher = "";
                        break;
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
                    } else
                    {
                        cart.Discount = 0;
                        cart.CurrentVoucher = "";
                        break;
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
                    } else
                    {
                        cart.Discount = 0;
                        cart.CurrentVoucher = "";
                        break;
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
