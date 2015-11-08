using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CocktailAlmanac.Models {
    public class IngredientViewModel {
        public INGREDIENT Ingredient { get; set; }
        public List<bool> SelectedAllergens { get; set; }
        public SelectList MeasurementUnits { get; set; }
        public List<ALLERGEN> Allergens { get; set; }
        public List<NUTRITIONAL_INFO> NutritionalInfos { get; set; }
        public List<INGREDIENT_NUTRITIONAL_INFO> IngredientNutritionalInfos { get; set; }
    }
}
