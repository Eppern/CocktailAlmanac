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
    public class INGREDIENTsController : Controller {
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
        [Authorize(Roles = "Mod")]
        public ActionResult Create() {

            ViewBag.MeasurementUnitId = new SelectList(db.MEASUREMENT_UNIT, "MeasurementUnitId", "ShortName");
            return View();
        }

        // POST: INGREDIENTs/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Mod")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredientId,Name,Description,DateSubmitted,DateModified,SubmittedBy,ModifiedBy,AlcoholPercent,MeasurementUnitId,ImageURL")] INGREDIENT iNGREDIENT) {

            iNGREDIENT.ModifiedBy = User.Identity.GetUserId().ToString();
            iNGREDIENT.SubmittedBy = iNGREDIENT.ModifiedBy;
            iNGREDIENT.DateSubmitted = DateTime.Now;
            iNGREDIENT.DateModified = DateTime.Now;
            if (ModelState.IsValid) {
                db.INGREDIENT.Add(iNGREDIENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModifiedBy = UserHelpers.GetCurrentUserId();
            ViewBag.SubmittedBy = ViewBag.ModifiedBy;
            ViewBag.DateSubmitted = DateTime.Now;
            ViewBag.DateModified = DateTime.Now;
            ViewBag.MeasurementUnitId = new SelectList(db.MEASUREMENT_UNIT, "MeasurementUnitId", "ShortName", iNGREDIENT.MeasurementUnitId);
            return View(iNGREDIENT);
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
