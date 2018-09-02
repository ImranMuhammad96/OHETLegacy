using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OHET_Project.Models.models;
using OHET_Project.Persistence;

namespace OHET_Project.Controllers
{
    public class HeroesController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Heroes
        public ActionResult Index()
        {
            var heroes = db.heroes.Include(h => h.Concept).Include(h => h.Content);
            return View(heroes.ToList());
        }

        // GET: Heroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.heroes.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        // GET: Heroes/Create
        public ActionResult Create()
        {
            ViewBag.IDConcept = new SelectList(db.concepts, "IDConcept", "description");
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            return View();
        }

        public ActionResult _createClassChoice()
        {
            List<Class> classes = db.classes.ToList();

            return PartialView("_createClassChoice", classes);
        }

        // POST: Heroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHero,name,totalDR,gold,exp,IDConcept,IDContent")] Hero hero)
        {
            if (ModelState.IsValid)
            {
                db.heroes.Add(hero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDConcept = new SelectList(db.concepts, "IDConcept", "description", hero.IDConcept);
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", hero.IDContent);
            return View(hero);
        }

        // GET: Heroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.heroes.Find(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hero hero = db.heroes.Find(id);
            db.heroes.Remove(hero);
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
