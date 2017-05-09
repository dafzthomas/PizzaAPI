using PizzaAPI.Models;
using PizzaAPI.Models.Helpers;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace PizzaAPI.Controllers.v1
{
    public class ToppingsController : ApiController
    {

        private PizzaOrderContext db = new PizzaOrderContext();

        [HttpGet]
        [Route("api/v1/Toppings")]
        [ResponseType(typeof(List<Topping>))]
        public IHttpActionResult GetToppings()
        {
            List<ToppingViewModel> toppings = new List<ToppingViewModel>();
            foreach (Topping topping in db.Toppings)
            {
                toppings.Add(new ToppingViewModel(topping));
            }
            return Ok(toppings);
        }
    }
}
