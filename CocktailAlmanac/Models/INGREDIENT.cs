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
    using System.ComponentModel.DataAnnotations;

    public partial class INGREDIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INGREDIENT()
        {
            this.INGREDIENT_ALLERGEN = new HashSet<INGREDIENT_ALLERGEN>();
            this.INGREDIENT_NUTRITIONAL_INFO = new HashSet<INGREDIENT_NUTRITIONAL_INFO>();
            this.RECIPE_INGREDIENT = new HashSet<RECIPE_INGREDIENT>();
        }
    
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateSubmitted { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string SubmittedBy { get; set; }
        public string ModifiedBy { get; set; }
        public double AlcoholPercent { get; set; }

        [Display(Name = "Default Unit Of Measurement")]
        public int MeasurementUnitId { get; set; }

        [Display(Name = "Image")]
        public string ImageURL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INGREDIENT_ALLERGEN> INGREDIENT_ALLERGEN { get; set; }
        public virtual MEASUREMENT_UNIT MEASUREMENT_UNIT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INGREDIENT_NUTRITIONAL_INFO> INGREDIENT_NUTRITIONAL_INFO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECIPE_INGREDIENT> RECIPE_INGREDIENT { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
    }
}
