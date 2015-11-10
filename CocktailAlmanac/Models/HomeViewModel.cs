using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailAlmanac.Models
{
    public class HomeViewModel
    {
        public List<RECIPE> Recipes { get; set; }
        public RECIPE RecipeOfTheMonth { get; set; }
        public RECIPE RecipeOfTheDay { get; set; }
        public RECIPE BestRated { get; set; }
    }
}