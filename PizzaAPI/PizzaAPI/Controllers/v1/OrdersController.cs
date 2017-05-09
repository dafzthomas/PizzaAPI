using PizzaAPI.Models;
using PizzaAPI.Models.Helpers;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace PizzaAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private PizzaOrderContext db = new PizzaOrderContext();

        [Authorize]
        [HttpGet]
        [Route("api/v1/Orders")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult GetOrders()
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            string userId = RequestContext.Principal.Identity.Name;

            foreach (Order order in db.Orders)
            {
                if (order.UserId == userId)
                {
                    orders.Add(new OrderViewModel(order));
                }
            }
            return Ok(orders);
        }

        [Authorize]
        [HttpPost]
        [Route("api/v1/SubmitOrder")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult SubmitOrder(ClientViewModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order(clientModel.order);
            string userId = RequestContext.Principal.Identity.Name;

            CartHelper.Submit(userId, order);

            return Ok(order);
        }


    }
}
