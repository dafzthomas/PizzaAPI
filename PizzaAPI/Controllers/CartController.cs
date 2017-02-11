using System.Collections.Generic;
using System.Dynamic;
using System.Web.Http;
namespace PizzaAPI
{
	public class CartController : ApiController
	{

		[HttpGet]
		public IHttpActionResult Index()
		{
			dynamic obj = new ExpandoObject();

			obj.type = "message";
			obj.data = new List<string>();

			obj.data.Add("Hello");
			obj.data.Add("world");
			obj.data.Add("guys!");

			return Ok(obj);
		}
	}
}
