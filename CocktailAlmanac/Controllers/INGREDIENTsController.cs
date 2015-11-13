using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CocktailAlmanac.Models;
using Microsoft.AspNet.Identity;
using CocktailAlmanac.Helpers;

namespace CocktailAlmanac.Controllers {
    public class IngredientsController : Controller {
        private CocktailEntities1 db = new CocktailEntities1();

        // GET: INGREDIENTs
        public ActionResult Index() {
            var iNGREDIENT = db.INGREDIENT.Include(i => i.AspNetUsers).Include(i => i.AspNetUsers1).Include(i => i.MEASUREMENT_UNIT);
            return View(iNGREDIENT.ToList());
        }

        // GET: INGREDIENTs/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGREDIENT iNGREDIENT = db.INGREDIENT.Find(id);
            if (iNGREDIENT == null) {
                return HttpNotFound();
            }
            return View(iNGREDIENT);
        }

        // GET: INGREDIENTs/Create
        [Authorize(Roles = "User")]
        public ActionResult Create() {
            IngredientViewModel model = new IngredientViewModel();
            model.Allergens = db.ALLERGEN.ToList();
            model.NutritionalInfos = db.NUTRITIONAL_INFO.ToList();
            model.SelectedAllergens = new List<bool>();
            for(int i = 0; i < model.Allergens.Count(); i++) {
                model.SelectedAllergens.Add(false);
            }
            model.IngredientNutritionalInfos = new List<INGREDIENT_NUTRITIONAL_INFO>();

            for (int i = 0; i < model.NutritionalInfos.Count(); i++) {
                model.IngredientNutritionalInfos.Add(new INGREDIENT_NUTRITIONAL_INFO());
            }

            model.MeasurementUnits = new SelectList(db.MEASUREMENT_UNIT, "MeasurementUnitId", "ShortName");
            return View(model);
        }

        // POST: INGREDIENTs/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Mod")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ingredient, SelectedAllergens, Allergens, MeasurementUnitId, IngredientNutritionalInfos")] IngredientViewModel model) {

            model.Ingredient.ModifiedBy = User.Identity.GetUserId().ToString();
            model.Ingredient.SubmittedBy = model.Ingredient.ModifiedBy;
            model.Ingredient.DateSubmitted = DateTime.Now;
            model.Ingredient.DateModified = DateTime.Now;
            model.NutritionalInfos = db.NUTRITIONAL_INFO.ToList();
            model.Allergens = db.ALLERGEN.ToList();

            if (ModelState.IsValid) {
                db.INGREDIENT.Add(model.Ingredient);
                db.SaveChanges();

                int id = model.Ingredient.IngredientId;

                #region allergens
                
                for (int i = 0; i < model.Allergens.Count(); i++) {
                    INGREDIENT_ALLERGEN ia = new INGREDIENT_ALLERGEN() {
                        Present = model.SelectedAllergens[i],
                        IngredientId = id,
                        AllergenId = model.Allergens[i].AllergenId
                    };
                    db.INGREDIENT_ALLERGEN.Add(ia);
                }
                #endregion

                #region nutritional info
                
                
                for (int i = 0; i < model.NutritionalInfos.Count(); i++) {
                    model.IngredientNutritionalInfos[i].INGREDIENT = model.Ingredient;
                    model.IngredientNutritionalInfos[i].NUTRITIONAL_INFO = model.NutritionalInfos[i];
                    model.Ingredient.INGREDIENT_NUTRITIONAL_INFO.Add(model.IngredientNutritionalInfos[i]);
                }

                #endregion

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            model.Allergens = db.ALLERGEN.ToList();
            model.NutritionalInfos = db.NUTRITIONAL_INFO.ToList();
            model.SelectedAllergens = new List<bool>();
            for (int i = 0; i < model.Allergens.Count(); i++)
            {
                model.SelectedAllergens.Add(false);
            }
            model.IngredientNutritionalInfos = new List<INGREDIENT_NUTRITIONAL_INFO>();

            for (int i = 0; i < model.NutritionalInfos.Count(); i++)
            {
                model.IngredientNutritionalInfos.Add(new INGREDIENT_NUTRITIONAL_INFO());
            }

            model.MeasurementUnits = new SelectList(db.MEASUREMENT_UNIT, "MeasurementUnitId", "ShortName");
            return View(model);
        }

        // GET: INGREDIENTs/Edit/5
        [Authorize(Roles = "Mod")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGREDIENT iNGREDIENT = db.INGREDIENT.Find(id);
            if (iNGREDIENT == null) {
                return HttpNotFound();
            }
            ViewBag.IngredientId = id;
            ViewBag.MeasurementUnitId = new SelectList(db.MEASUREMENT_UNIT, "MeasurementUnitId", "ShortName", iNGREDIENT.MeasurementUnitId);
            return View(iNGREDIENT);
        }

        // POST: INGREDIENTs/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod")]
        public ActionResult Edit([Bind(Include = "IngredientId,Name,Description,AlcoholPercent,MeasurementUnitId,ImageURL")] INGREDIENT iNGREDIENT) {
            if (ModelState.IsValid) {
                INGREDIENT currentIngredient = db.INGREDIENT.Find(iNGREDIENT.IngredientId);
                currentIngredient = ControllerHelpers.UpdateEntity(currentIngredient, iNGREDIENT, ModelState.Keys.ToList());
                currentIngredient.ModifiedBy = User.Identity.GetUserId();
                currentIngredient.DateModified = DateTime.Now;
                db.Entry(currentIngredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModifiedBy = UserHelpers.GetCurrentUserId();
            ViewBag.MeasurementUnitId = new SelectList(db.MEASUREMENT_UNIT, "MeasurementUnitId", "ShortName", iNGREDIENT.MeasurementUnitId);
            return View(iNGREDIENT);
        }

        // GET: INGREDIENTs/Delete/5
        [Authorize(Roles = "Mod")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGREDIENT iNGREDIENT = db.INGREDIENT.Find(id);
            if (iNGREDIENT == null) {
                return HttpNotFound();
            }
            return View(iNGREDIENT);
        }

        // POST: INGREDIENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod")]
        public ActionResult DeleteConfirmed(int id) {
            INGREDIENT iNGREDIENT = db.INGREDIENT.Find(id);
            bool isInUse = db.RECIPE_INGREDIENT.Where(i => i.IngredientId == id).Count() > 0;
            if (!isInUse) {
                db.INGREDIENT.Remove(iNGREDIENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //TODO: Display error that they can only modifiy
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
