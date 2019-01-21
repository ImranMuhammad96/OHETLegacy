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
    public class AdventuresController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Adventures
        public ActionResult Index(bool isOff, string searchString)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = isOff;

            var adventures = db.adventures.Where(x => x.Content.isOfficial == isOff).Include(a => a.Content);
            if (!String.IsNullOrEmpty(searchString))
            {
                adventures = adventures.Where(x => x.title.Contains(searchString));
            }

            return View(adventures.ToList());
        }

        // GET: Adventures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adventure adventure = db.adventures.Include(c => c.Content).Where(x => x.IDAdventure == id).SingleOrDefault();
            if (adventure == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = adventure.Content.isOfficial;
            return View(adventure);
        }

        // GET: Adventures/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create(bool isOff)
        {
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            ViewBag.isOff = isOff;
            return View();
        }

        // POST: Adventures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adventure adventure)
        {
            if (ModelState.IsValid)
            {
                var a = new Adventure
                {
                    IDAdventure = adventure.IDAdventure,
                    title = adventure.title,
                    description = adventure.description,
                    Content = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name),
                    IDContent = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name).IDContent
                };

                db.adventures.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index", new { isOff = a.Content.isOfficial });
            }

            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", adventure.IDContent);
            return View(adventure);
        }

        // GET: Adventures/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adventure adventure = db.adventures.Include(c => c.Content).Where(x => x.IDAdventure == id).SingleOrDefault();
            if (adventure == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", adventure.IDContent);
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = adventure.Content.isOfficial;
            return View(adventure);
        }

        // POST: Adventures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAdventure,title,description,IDContent")] Adventure adventure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adventure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", adventure.IDContent);
            return View(adventure);
        }

        // GET: Adventures/Delete/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adventure adventure = db.adventures.Include(c => c.Content).Where(x => x.IDAdventure == id).SingleOrDefault();
            if (adventure == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = adventure.Content.isOfficial;
            return View(adventure);
        }

        // POST: Adventures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adventure adventure = db.adventures.Find(id);
            var isOff = db.adventures.Where(x => x.IDAdventure == id).Include(a => a.Content).SingleOrDefault().Content.isOfficial;
            db.adventures.Remove(adventure);
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
