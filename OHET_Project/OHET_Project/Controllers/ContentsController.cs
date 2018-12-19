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
using Microsoft.AspNet.Identity.EntityFramework;
using OHET_Project.Models.ViewModel;

namespace OHET_Project.Controllers
{
    [Authorize]
    public class ContentsController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Contents
        public ActionResult Index()
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.userName = User.Identity.GetUserName().Substring(0, User.Identity.GetUserName().IndexOf('@'));
            //var contents = db.contents.Include(c => c.ApplicationUser).Include(d => d.favcons);
            //ViewBag.fav = db.favcons.ToList();

            ContentViewModel contentModel = new ContentViewModel();

            contentModel.Contents = db.contents.ToList();
            contentModel.FavCons = db.favcons.ToList();

            return View(contentModel);
        }

        // GET: Contents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }

            content.classes = db.classes.Where(Id => Id.IDContent == content.IDContent).ToList();
            content.rules = db.rules.Where(Id => Id.IDContent == content.IDContent).ToList();
            content.adventures = db.adventures.Where(Id => Id.IDContent == content.IDContent).ToList();
            content.items = db.items.Where(Id => Id.IDContent == content.IDContent).ToList();
            content.spells = db.spells.Where(Id => Id.IDContent == content.IDContent).ToList();
            content.monsters = db.monsters.Where(Id => Id.IDContent == content.IDContent).ToList();

            return View(content);
        }

        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult PublishOrUnpublish(int? id)
        {
            //ViewBag.contentPublic = true;
            //return RedirectToAction("Index");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                content.isPublic = !content.isPublic;
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", content.ApplicationUserId);
            return View(content);
        }

        [Authorize]
        public ActionResult AddFav(int? id)
        {
            if (ModelState.IsValid)
            {
                var fc = new FavCon
                {
                    ApplicationUser = db.Users.First(u => u.UserName == User.Identity.Name),
                    ApplicationUserId = db.Users.First(u => u.UserName == User.Identity.Name).Id,
                    //Content = content,
                    //IDContent = content.IDContent
                    Content = db.contents.Find(id),
                    IDContent = id
                };

                db.favcons.Add(fc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", content.ApplicationUserId);
            //return View(content);
            return View();
        }

        [Authorize]
        public ActionResult DeleteFav(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavCon favcon = db.favcons.Find(id);
            if (favcon == null)
            {
                return HttpNotFound();
            }
            db.favcons.Remove(favcon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Contents/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDContent,isOfficial,isPublic,ApplicationUserId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.contents.Add(content);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", content.ApplicationUserId);
            return View(content);
        }

        // GET: Contents/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", content.ApplicationUserId);
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDContent,isOfficial,isPublic,ApplicationUserId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", content.ApplicationUserId);
            return View(content);
        }

        // GET: Contents/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Content content = db.contents.Find(id);
            db.contents.Remove(content);
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
