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
    
    public partial class RECIPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RECIPE()
        {
            this.RECIPE_INGREDIENT = new HashSet<RECIPE_INGREDIENT>();
            this.RECIPE_STEP = new HashSet<RECIPE_STEP>();
        }
    
        public int RecipeId { get; set; }
        public Nullable<double> Rating { get; set; }
        public System.DateTime SubmittedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> Votes { get; set; }
        public string SubmittedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Category { get; set; }
        public string ImageURL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECIPE_INGREDIENT> RECIPE_INGREDIENT { get; set; }
        public virtual RECIPE_CATEGORY RECIPE_CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECIPE_STEP> RECIPE_STEP { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
    }
}
