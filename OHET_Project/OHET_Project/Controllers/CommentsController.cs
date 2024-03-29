﻿using System;
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
using System.Web.SessionState;

namespace OHET_Project.Controllers
{
    public class CommentsController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Comments
        public ActionResult Index()
        {
            ViewBag.userId = User.Identity.GetUserId();

            var comments = db.comments.Include(c => c.ApplicationUser).Include(c => c.Post);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create(int? id)
        {
            //ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            //ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title");
            ViewBag.IDPost = id;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment, int? id)
        {
            if (ModelState.IsValid)
            {
                var c = new Comment
                {
                    Description = comment.Description,
                    Date = DateTime.Now,
                    ApplicationUser = db.Users.First(u => u.UserName == User.Identity.Name),
                    ApplicationUserId = db.Users.First(u => u.UserName == User.Identity.Name).Id,
                    Post = db.posts.First(u => u.IDPost == id),
                    IDPost = id
                };

                db.comments.Add(c);
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { id = id });
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", comment.ApplicationUserId);
            ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title", comment.IDPost);
            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", comment.ApplicationUserId);
            ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title", comment.IDPost);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { id = comment.IDPost });
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", comment.ApplicationUserId);
            ViewBag.IDPost = new SelectList(db.posts, "IDPost", "Title", comment.IDPost);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int? x = db.comments.Find(id).IDPost;
            Comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
