//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CocktailAlmanac.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RECIPE_INGREDIENT
    {
        public double Amount { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
    
        public virtual INGREDIENT INGREDIENT { get; set; }
        public virtual RECIPE RECIPE { get; set; }
    }
}
