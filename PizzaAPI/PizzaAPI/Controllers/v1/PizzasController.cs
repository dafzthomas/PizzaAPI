using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using PizzaAPI.Models;
using PizzaAPI.Models.Helpers;

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
        
        
    }
}