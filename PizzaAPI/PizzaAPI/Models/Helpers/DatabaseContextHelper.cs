using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAPI.Models.Helpers
{
    class DatabaseContextHelper
    {
        public static PizzaOrderContext Db = new PizzaOrderContext();
    }
}
