 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CocktailAlmanac.Models;

namespace CocktailAlmanac.Controllers
{
    public class AllergensController : Controller
    {
        private CocktailEntities1 db = new CocktailEntities1();

        // GET: Allergens
        public ActionResult Index()
        {
            return View(db.ALLERGEN.ToList());
        }

        // GET: Allergens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALLERGEN aLLERGEN = db.ALLERGEN.Find(id);
            if (aLLERGEN == null)
            {
                return HttpNotFound();
            }
            return View(aLLERGEN);
        }

        // GET: Allergens/Create
        [Authorize(Roles = "Mod")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Allergens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod")]
        public ActionResult Create([Bind(Include = "AllergenId,ShortName,LongName,ImageURL")] ALLERGEN aLLERGEN)
        {
            if (ModelState.IsValid)
            {
                db.ALLERGEN.Add(aLLERGEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aLLERGEN);
        }

        // GET: Allergens/Edit/5
        [Authorize(Roles = "Mod")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALLERGEN aLLERGEN = db.ALLERGEN.Find(id);
            if (aLLERGEN == null)
            {
                return HttpNotFound();
            }
            return View(aLLERGEN);
        }

        // POST: Allergens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Mod")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllergenId,ShortName,LongName,ImageURL")] ALLERGEN aLLERGEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aLLERGEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aLLERGEN);
        }

        // GET: Allergens/Delete/5
        [Authorize(Roles = "Mod")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALLERGEN aLLERGEN = db.ALLERGEN.Find(id);
            if (aLLERGEN == null)
            {
                return HttpNotFound();
            }
            return View(aLLERGEN);
        }

        // POST: Allergens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mod")]
        public ActionResult DeleteConfirmed(int id)
        {
            ALLERGEN aLLERGEN = db.ALLERGEN.Find(id);
            db.ALLERGEN.Remove(aLLERGEN);
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
