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
    
    public partial class INGREDIENT_ALLERGEN
    {
        public bool Present { get; set; }
        public int IngredientId { get; set; }
        public int AllergenId { get; set; }
    
        public virtual ALLERGEN ALLERGEN { get; set; }
        public virtual INGREDIENT INGREDIENT { get; set; }
    }
}
