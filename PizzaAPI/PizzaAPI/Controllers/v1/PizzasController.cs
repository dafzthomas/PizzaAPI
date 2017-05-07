using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PizzaAPI.Models;
using PizzaAPI.Models.Helpers;
using System.Web.Http.Cors;

namespace PizzaAPI.Controllers
{
    public class PizzasController : ApiController
    {
        
        private PizzaOrderContext db = new PizzaOrderContext();
        
        [HttpGet]
        [Route("api/v1/Pizzas")]
        [ResponseType(typeof(List<Pizza>))]
        public IHttpActionResult GetPizzas()
        {
            List<PizzaViewModel> pizzas = new List<PizzaViewModel>();
            foreach (Pizza pizza in db.Pizzas)
            {
                pizzas.Add(new PizzaViewModel(pizza));
            }
            return Ok(pizzas);
        }

        [HttpGet]
        [Route("api/v1/Pizzas/{id}")]
        [ResponseType(typeof(Pizza))]
        public async Task<IHttpActionResult> GetPizza(int id)
        {
            Pizza pizza = await db.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }
        
        [HttpPost]
        [Route("api/v1/AddToCart")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult AddToCart(ClientViewModel clientModel)
        {
            Order order = new Order(clientModel.order);

            OrderItem orderItem = new OrderItem();
            orderItem.Pizza = db.Pizzas.Find(clientModel.pizzaId);
            orderItem.Price = orderItem.Pizza.Price;

            if (clientModel.extraToppings != null)
            {
                foreach(Topping topping in clientModel.extraToppings)
                {
                    Topping t = db.Toppings.Find(topping.ToppingId);
                    orderItem.ExtraToppings.Add(t);
                    orderItem.Price += t.Price;
                }
            }

            order.OrderItems.Add(orderItem);
            order.Price += orderItem.Price;

            return Ok(order);
        }

        [HttpPut]
        [Route("api/v1/RemoveFromCart")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult RemoveFromCart(ClientViewModel clientModel)
        {
            Order order = new Order(clientModel.order);
            Pizza pizza = db.Pizzas.Find(clientModel.pizzaId);


            var itemToRemove = order.OrderItems.Single(r => r.OrderItemId == clientModel.orderItem.OrderItemId);

            order.OrderItems.Remove(itemToRemove);
            order.Price -= itemToRemove.Price;
            
            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        [Route("api/v1/SubmitOrder")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult SubmitOrder(ClientViewModel clientModel)
        {
            Order order = new Order(clientModel.order);
            string userId = RequestContext.Principal.Identity.Name;

            CartHelper.Submit(userId, order);

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}