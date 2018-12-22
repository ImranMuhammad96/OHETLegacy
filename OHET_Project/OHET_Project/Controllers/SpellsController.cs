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
using Microsoft.AspNet.Identity;

namespace OHET_Project.Controllers
{
    public class SpellsController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Spells
        public ActionResult Index()
        {
            ViewBag.userId = User.Identity.GetUserId();

            var spells = db.spells.Include(s => s.Class).Include(s => s.Content);
            return View(spells.ToList());
        }

        // GET: Spells/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spell spell = db.spells.Find(id);
            if (spell == null)
            {
                return HttpNotFound();
            }
            return View(spell);
        }

        // GET: Spells/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create()
        {
            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name");
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            return View();
        }

        // POST: Spells/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Spell spell)
        {
            if (ModelState.IsValid)
            {
                var s = new Spell
                {
                    IDSpell = spell.IDSpell,
                    name = spell.name,
                    description = spell.description,
                    conceptLvl = spell.conceptLvl,
                    Content = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name),
                    IDContent = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name).IDContent,
                    Class = db.classes.First(u => u.IDClass == spell.IDClass),
                    IDClass = spell.IDClass
                };

                db.spells.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name", spell.IDClass);
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", spell.IDContent);
            return View(spell);
        }

        // GET: Spells/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spell spell = db.spells.Find(id);
            if (spell == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name", spell.IDClass);
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", spell.IDContent);
            return View(spell);
        }

        // POST: Spells/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSpell,name,description,conceptLvl,IDContent,IDClass")] Spell spell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name", spell.IDClass);
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", spell.IDContent);
            return View(spell);
        }

        // GET: Spells/Delete/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spell spell = db.spells.Find(id);
            if (spell == null)
            {
                return HttpNotFound();
            }
            return View(spell);
        }

        // POST: Spells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spell spell = db.spells.Find(id);
            db.spells.Remove(spell);
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
