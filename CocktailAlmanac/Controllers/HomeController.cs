using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocktailAlmanac.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Was ist Cocktail Almanac?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Eppern-Software";

            return View();
        }
    }
}