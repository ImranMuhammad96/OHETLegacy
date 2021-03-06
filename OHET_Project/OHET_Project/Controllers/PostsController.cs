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
using OHET_Project.BLL;

namespace OHET_Project.Controllers
{
    public class PostsController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Posts
        public ActionResult Index()
        {
            ViewBag.userId = User.Identity.GetUserId();
            var posts = db.posts.Include(p => p.ApplicationUser).ToList();
            foreach(var post in posts)
            {
                post.comments = db.comments.Where(Id => Id.IDPost == post.IDPost).ToList();
                post.subposts = db.subposts.Where(Id => Id.IDPost == post.IDPost).ToList();
            }
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.userId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            post.comments = db.comments.Where(Id => Id.IDPost == post.IDPost).ToList();
            post.subposts = db.subposts.Where(Id => Id.IDPost == post.IDPost).ToList();
            return View(post);
        }

        [Authorize(Roles = "Admin, Editor")]
        public ActionResult PublishOrUnpublish(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                post.isPublic = !post.isPublic;
                post.Date = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                post.image = file.ConvertToBytes();

                var p = new Post
                {
                    IDPost = post.IDPost,
                    Title = post.Title,
                    Description = post.Description,
                    Date = DateTime.Now,
                    isPublic = false,
                    image = post.image,
                    ApplicationUser = db.Users.First(u => u.UserName == User.Identity.Name),
                    ApplicationUserId = db.Users.First(u => u.UserName == User.Identity.Name).Id
                };

                db.posts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", post.ApplicationUserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", post.ApplicationUserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPost,Title,Description,Date,ApplicationUserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", post.ApplicationUserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.posts.Find(id);
            db.posts.Remove(post);
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
