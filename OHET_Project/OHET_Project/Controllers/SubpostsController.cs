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
        public ActionResult Create(int? id)
        {
            //ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title");
            ViewBag.IDPost = id;
            return View();
        }

        // POST: Subposts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subpost subpost, int? id)
        {
            if (ModelState.IsValid)
            {
                var v = 1;
                if (db.subposts.Any())
                {
                    v = db.subposts.OrderByDescending(u => u.OrderNr).FirstOrDefault().OrderNr + 1;
                }
                var s = new Subpost
                {
                    Title = subpost.Title,
                    Description = subpost.Description,
                    OrderNr = v,
                    Post = db.posts.First(u => u.IDPost == id),
                    IDPost = id
                };

                db.subposts.Add(s);
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { id = id });
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
                return RedirectToAction("Details", "Posts", new { id = subpost.IDPost });
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
            int? x = db.subposts.Find(id).IDPost;
            for (int i = db.subposts.OrderByDescending(u => u.OrderNr).FirstOrDefault().OrderNr; i>db.subposts.Find(id).OrderNr; i--)
            {
                db.subposts.Where(o => o.OrderNr == i && o.IDPost == x).SingleOrDefault().OrderNr--;
                db.Entry(db.subposts.Find(db.subposts.Where(o => o.OrderNr == i).SingleOrDefault().IDSubpost)).State = EntityState.Modified;
            }
            Subpost subpost = db.subposts.Find(id);
            db.subposts.Remove(subpost);
            db.SaveChanges();
            return RedirectToAction("Details", "Posts", new { id = x });
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
