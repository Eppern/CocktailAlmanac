using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CocktailAlmanac.Models {
    public class CocktailViewModel {
        public RECIPE Recipe { get; set; }
        public List<int?> SelectedIngredients { get; set; }

        [Display(Prompt = "Recipe Step")]
        public List<string> RecipeSteps { get; set; }
        public SelectList Ingredients { get; set; }
        public SelectList Categories { get; set; }
        public List<ALLERGEN> Allergens { get; set; }
        public List<NUTRITIONAL_INFO> NutritionalInfo { get; set; }
    }
}
