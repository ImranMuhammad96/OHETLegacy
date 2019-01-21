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
        public ActionResult Index(bool isOff, int? classID, string searchString)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = isOff;
            var spells = db.spells.Where(x => x.Content.isOfficial == isOff).Include(s => s.Class).Include(s => s.Content);
            if (classID != null)
            {
                spells = spells.Where(x => x.Class.IDClass == classID);
                ViewBag.classID = classID;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                spells = spells.Where(x => x.name.Contains(searchString));
                ViewBag.search = searchString;
            }

            return View(spells.ToList());
        }

        // GET: Spells/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spell spell = db.spells.Include(c => c.Content).Where(x => x.IDSpell == id).SingleOrDefault();
            if (spell == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = spell.Content.isOfficial;
            return View(spell);
        }

        // GET: Spells/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create(bool isOff)
        {
            //ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name");
            //ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            ViewBag.IDClass = new SelectList(db.classes.Where(s => s.isSpellcaster == true), "IDClass", "name");
            ViewBag.isOff = isOff;
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
                return RedirectToAction("Index", new { isOff = s.Content.isOfficial });
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
            Spell spell = db.spells.Include(c => c.Content).Where(x => x.IDSpell == id).SingleOrDefault();
            if (spell == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDClass = new SelectList(db.classes, "IDClass", "name", spell.IDClass);
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", spell.IDContent);
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = spell.Content.isOfficial;
            return View(spell);
        }

        // POST: Spells/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Spell spell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { isOff = db.contents.Where(id => id.IDContent == spell.IDContent).SingleOrDefault().isOfficial });
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
            Spell spell = db.spells.Include(c => c.Content).Include(cc => cc.Class).Where(x => x.IDSpell == id).SingleOrDefault();
            if (spell == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = spell.Content.isOfficial;
            return View(spell);
        }

        // POST: Spells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spell spell = db.spells.Find(id);
            var isOff = db.spells.Where(x => x.IDSpell == id).Include(a => a.Content).SingleOrDefault().Content.isOfficial;
            db.spells.Remove(spell);
            db.SaveChanges();
            return RedirectToAction("Index", new { isOff = isOff });
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
