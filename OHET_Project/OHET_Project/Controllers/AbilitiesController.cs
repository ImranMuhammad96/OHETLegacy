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
    public class AbilitiesController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Abilities
        public ActionResult Index()
        {
            var abilities = db.abilities.Include(a => a.Class);
            return View(abilities.ToList());
        }

        // GET: Abilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ability ability = db.abilities.Find(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View(ability);
        }

        // GET: Abilities/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create(int? id)
        {
            //ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name");
            ViewBag.IDClass = id;
            return View();
        }

        // POST: Abilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ability ability, int? id)
        {
            if (ModelState.IsValid)
            {
                var a = new Ability
                {
                    IDAbility = ability.IDAbility,
                    description = ability.description,
                    conceptLvl = ability.conceptLvl,
                    Class = db.classes.First(u => u.IDClass == id),
                    IDClass = id
                };

                db.abilities.Add(a);
                db.SaveChanges();
                return RedirectToAction("Details", "Classes", new { id = id });
            }

            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name", ability.IDClass);
            return View(ability);
        }

        // GET: Abilities/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ability ability = db.abilities.Find(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name", ability.IDClass);
            return View(ability);
        }

        // POST: Abilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAbility,description,conceptLvl,IDClass")] Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Classes", new { id = ability.IDClass });
            }
            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name", ability.IDClass);
            return View(ability);
        }

        // GET: Abilities/Delete/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ability ability = db.abilities.Find(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View(ability);
        }

        // POST: Abilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ability ability = db.abilities.Find(id);
            db.abilities.Remove(ability);
            db.SaveChanges();
            return RedirectToAction("Details", "Classes", new { id = id });
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
