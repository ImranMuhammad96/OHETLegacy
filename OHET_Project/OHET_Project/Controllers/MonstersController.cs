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
    public class MonstersController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Monsters
        public ActionResult Index(bool isOff, string searchString)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = isOff;

            var monsters = db.monsters.Where(x => x.Content.isOfficial == isOff).Include(m => m.Content);
            if (!String.IsNullOrEmpty(searchString))
            {
                monsters = monsters.Where(x => x.name.Contains(searchString));
            }

            return View(monsters.ToList());
        }

        // GET: Monsters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monster monster = db.monsters.Include(c => c.Content).Where(x => x.IDMonster == id).SingleOrDefault();
            if (monster == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = monster.Content.isOfficial;
            return View(monster);
        }

        // GET: Monsters/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create(bool isOff)
        {
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            ViewBag.isOff = isOff;
            return View();
        }

        // POST: Monsters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Monster monster)
        {
            if (ModelState.IsValid)
            {
                var m = new Monster
                {
                    IDMonster = monster.IDMonster,
                    name = monster.name,
                    description = monster.description,
                    conceptLvl = monster.conceptLvl,
                    Content = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name),
                    IDContent = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name).IDContent
                };

                db.monsters.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", new { isOff = m.Content.isOfficial });
            }

            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", monster.IDContent);
            return View(monster);
        }

        // GET: Monsters/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monster monster = db.monsters.Include(c => c.Content).Where(x => x.IDMonster == id).SingleOrDefault();
            if (monster == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", monster.IDContent);
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = monster.Content.isOfficial;
            return View(monster);
        }

        // POST: Monsters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Monster monster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { isOff = db.contents.Where(id => id.IDContent == monster.IDContent).SingleOrDefault().isOfficial });
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", monster.IDContent);
            return View(monster);
        }

        // GET: Monsters/Delete/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monster monster = db.monsters.Include(c => c.Content).Where(x => x.IDMonster == id).SingleOrDefault();
            if (monster == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = monster.Content.isOfficial;
            return View(monster);
        }

        // POST: Monsters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Monster monster = db.monsters.Find(id);
            var isOff = db.monsters.Where(x => x.IDMonster == id).Include(a => a.Content).SingleOrDefault().Content.isOfficial;
            db.monsters.Remove(monster);
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
