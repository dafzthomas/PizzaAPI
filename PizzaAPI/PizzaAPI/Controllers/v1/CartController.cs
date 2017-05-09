using PizzaAPI.Models;
using PizzaAPI.Models.Helpers;
using System.Web.Http;
using System.Web.Http.Description;

namespace PizzaAPI.Controllers.v1
{
    public class CartController : ApiController
    {

        private PizzaOrderContext db = new PizzaOrderContext();

        [HttpPost]
        [Route("api/v1/AddToCart")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult AddToCart(ClientViewModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order(clientModel.order);
            order.CurrentVoucher = clientModel.voucherCode != null ? clientModel.voucherCode : "";

            CartHelper.Add(clientModel.pizzaId, clientModel.extraToppings, order);

            return Ok(order);
        }

        [HttpDelete]
        [Route("api/v1/RemoveFromCart")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult RemoveFromCart(ClientViewModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order(clientModel.order);
            int orderItemId = clientModel.orderItem.OrderItemId;

            CartHelper.Remove(orderItemId, order);

            return Ok(order);
        }

        [HttpDelete]
        [Route("api/v1/ResetCart")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult ResetCart(ClientViewModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order();

            return Ok(order);
        }

        [HttpPut]
        [Route("api/v1/AddCoupon")]
        [Route("api/v1/ApplyDelivery")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult ValidateCart(ClientViewModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order(clientModel.order);
            string voucher = clientModel.voucherCode;

            CartHelper.AddDealToCart(voucher, order);

            return Ok(order);
        }
    }
}
