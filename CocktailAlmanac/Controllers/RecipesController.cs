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

namespace CocktailAlmanac.Controllers
{
    public class RecipesController : Controller
    {
        private CocktailEntities1 db = new CocktailEntities1();

        // GET: Recipes
        public ActionResult Index()
        {
            var rECIPE = db.RECIPE.Include(r => r.RECIPE_CATEGORY).Include(r => r.AspNetUsers).Include(r => r.AspNetUsers1);
            return View(rECIPE.ToList());
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECIPE rECIPE = db.RECIPE.Find(id);
            if (rECIPE == null)
            {
                return HttpNotFound();
            }
            return View(rECIPE);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            CocktailViewModel model = new CocktailViewModel();
            model.SelectedIngredients = new List<int?>();
            model.RecipeSteps = new List<string>() { null, null, null, null, null, };
            model.Ingredients = new SelectList(db.INGREDIENT, "IngredientId", "Name");
            model.Categories = new SelectList(db.RECIPE_CATEGORY, "Recipe_CategoryId", "Name");
            return View(model);
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Recipe,SelectedIngredients,RecipeSteps,Allergens,NutritionalInfo,ViewBag")] CocktailViewModel model)
        {
            if (ModelState.IsValid)
            {
                //recipe
                model.Recipe.SubmittedBy = User.Identity.GetUserId();
                model.Recipe.ModifiedBy = model.Recipe.SubmittedBy;
                model.Recipe.SubmittedDate = DateTime.Now;
                model.Recipe.ModifiedDate = model.Recipe.SubmittedDate;
                db.RECIPE.Add(model.Recipe);
                db.SaveChanges();

                var recipeId = model.Recipe.RecipeId;

                //recipe step
                int stepCount = 1;
                foreach(string step in model.RecipeSteps.Where(s => s != null).Where(s => s != "")) {
                    RECIPE_STEP recStep = new RECIPE_STEP() {
                        Description = step.ToString(),
                        RecipeId = recipeId,
                        StepNr = stepCount
                    };
                    stepCount++;
                    db.RECIPE_STEP.Add(recStep);
                }


                //recipe ingredients
                foreach (int? ing in model.SelectedIngredients.Where(i => i != null)) {
                    RECIPE_INGREDIENT recIng = new RECIPE_INGREDIENT() {
                        Amount = 1,
                        IngredientId = (int)ing,
                        RecipeId = recipeId
                    };
                    db.RECIPE_INGREDIENT.Add(recIng);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.RECIPE_CATEGORY, "Recipe_CategoryId", "Name", model.Recipe.Category);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "FirstName", model.Recipe.ModifiedBy);
            ViewBag.SubmittedBy = new SelectList(db.AspNetUsers, "Id", "FirstName", model.Recipe.SubmittedBy);
            return View(model);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECIPE rECIPE = db.RECIPE.Find(id);
            if (rECIPE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.RECIPE_CATEGORY, "Recipe_CategoryId", "Name", rECIPE.Category);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "FirstName", rECIPE.ModifiedBy);
            ViewBag.SubmittedBy = new SelectList(db.AspNetUsers, "Id", "FirstName", rECIPE.SubmittedBy);
            return View(rECIPE);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,Rating,SubmittedDate,ModifiedDate,Votes,SubmittedBy,ModifiedBy,Name,Description,Category,ImageURL")] RECIPE rECIPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rECIPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.RECIPE_CATEGORY, "Recipe_CategoryId", "Name", rECIPE.Category);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "FirstName", rECIPE.ModifiedBy);
            ViewBag.SubmittedBy = new SelectList(db.AspNetUsers, "Id", "FirstName", rECIPE.SubmittedBy);
            return View(rECIPE);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECIPE rECIPE = db.RECIPE.Find(id);
            if (rECIPE == null)
            {
                return HttpNotFound();
            }
            return View(rECIPE);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RECIPE rECIPE = db.RECIPE.Find(id);
            db.RECIPE.Remove(rECIPE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
