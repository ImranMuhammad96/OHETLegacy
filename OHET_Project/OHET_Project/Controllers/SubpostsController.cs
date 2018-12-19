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
    public class SubpostsController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Subposts
        public ActionResult Index()
        {
            ViewBag.userId = User.Identity.GetUserId();

            var subposts = db.subposts.Include(s => s.Post);
            return View(subposts.ToList());
        }

        // GET: Subposts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subpost subpost = db.subposts.Find(id);
            if (subpost == null)
            {
                return HttpNotFound();
            }
            return View(subpost);
        }

        // GET: Subposts/Create
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Create()
        {
            ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title");
            return View();
        }

        // POST: Subposts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSubpost,Title,Description,OrderNr,IDPost")] Subpost subpost)
        {
            if (ModelState.IsValid)
            {
                db.subposts.Add(subpost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title", subpost.IDPost);
            return View(subpost);
        }

        // GET: Subposts/Edit/5
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subpost subpost = db.subposts.Find(id);
            if (subpost == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title", subpost.IDPost);
            return View(subpost);
        }

        // POST: Subposts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSubpost,Title,Description,OrderNr,IDPost")] Subpost subpost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subpost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title", subpost.IDPost);
            return View(subpost);
        }

        // GET: Subposts/Delete/5
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subpost subpost = db.subposts.Find(id);
            if (subpost == null)
            {
                return HttpNotFound();
            }
            return View(subpost);
        }

        // POST: Subposts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subpost subpost = db.subposts.Find(id);
            db.subposts.Remove(subpost);
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
