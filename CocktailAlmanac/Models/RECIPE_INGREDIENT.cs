//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
