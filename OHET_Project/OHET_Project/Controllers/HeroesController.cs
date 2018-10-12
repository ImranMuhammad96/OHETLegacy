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
            //var heroes = db.heroes.Include(h => h.Concept).Include(h => h.Content);
            var heroes = db.heroes.Include(h => h.Content);
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
            //ViewBag.IDConcept = new SelectList(db.concepts, "IDConcept", "description");
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            return View();
        }

        // GET:
        public ActionResult _createClassChoice()
        {
            List<Class> classes = db.classes.ToList();

            return PartialView("_createClassChoice", classes);
        }

        // ?POST:
        public ActionResult _createClassChoiceResult(string classID)
        {
            ViewBag.classID = classID;

            return View("Create");
        }

        // GET
        public ActionResult _createPersonalData(string classID)
        {
            ViewBag.classID = classID;

            return PartialView();
        }

        // ?POST:
        public ActionResult _createPersonalDataResult(string classID, string heroName)
        {
            ViewBag.classID = classID;
            ViewBag.heroName = heroName;

            return View("Create");
        }

        // GET
        public ActionResult _createConceptData(string classID, string heroName)
        {
            ViewBag.classID = classID;
            ViewBag.heroName = heroName;

            return PartialView();
        }

        // ?POST:
        public ActionResult _createConceptDataResult(string classID, string heroName, string conceptDescription)
        {
            ViewBag.classID = classID;
            ViewBag.heroName = heroName;
            ViewBag.conceptDescription = conceptDescription;

            int heroID = SaveNewHero(heroName);
            SaveNewConcept(conceptDescription);

            return RedirectToAction("Details", "Heroes", new { id = heroID });
        }

        public int SaveNewHero(string _heroName)
        {
            Hero hero = new Hero
            {
                name = _heroName,
                gold = 500,
                exp = 0
            };

            db.heroes.Add(hero);
            db.SaveChanges();

            return hero.IDHero;
        }

        public void SaveNewConcept(string _conceptDescription)
        {
            /*
            Concept concept = new Concept
            {
                level = 7,
                description = _conceptDescription
            };

            db.concepts.Add(concept);
            db.SaveChanges();
            */
            //return concept.IDConcept;
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

            //ViewBag.IDConcept = new SelectList(db.concepts, "IDConcept", "description", hero.IDConcept);
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
