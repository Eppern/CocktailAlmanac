using CocktailAlmanac.Models;
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
        private CocktailEntities1 db = new CocktailEntities1();

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Recipes = db.RECIPE.ToList();
            model.RecipeOfTheDay = model.Recipes.OrderByDescending(r => r.Votes).FirstOrDefault();
            model.RecipeOfTheMonth = model.Recipes.OrderByDescending(r => r.Name).FirstOrDefault();
            model.BestRated = db.RECIPE.OrderByDescending(r => r.Rating).FirstOrDefault();
            return View(model);
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