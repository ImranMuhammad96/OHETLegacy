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
using System.IO;
using OHET_Project.BLL;

namespace OHET_Project.Controllers
{
    public class ClassesController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Classes
        public ActionResult Index(bool isOff)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = isOff;

            var classes = db.classes.Where(x => x.Content.isOfficial == isOff).Include(c => c.Content);
            return View(classes.ToList());
        }

        // GET: Classes/Details/5
        public ActionResult Details(int id)
        {
            /*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            */
            Class _class = db.classes.Include(c => c.Content).Where(x => x.IDClass == id).SingleOrDefault();
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = _class.Content.isOfficial;

            if (_class == null)
            {
                return HttpNotFound();
            }
            return View(_class);
        }

        // GET: Classes/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create(bool isOff)
        {
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            ViewBag.isOff = isOff;
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Class _class)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                _class.image = file.ConvertToBytes();

                var c = new Class
                {
                    IDClass = _class.IDClass,
                    name = _class.name,
                    description = _class.description,
                    image = _class.image,
                    Content = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name),
                    IDContent = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name).IDContent
                };

                db.classes.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", _class.IDContent);
            return View(_class);
        }

        // GET: Classes/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class _class = db.classes.Include(c => c.Content).Where(x => x.IDClass == id).SingleOrDefault();
            if (_class == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", _class.IDContent);
            ViewBag.isOff = _class.Content.isOfficial;
            return View(_class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDClass,name,description,IDContent")] Class _class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(_class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", _class.IDContent);
            return View(_class);
        }

        // GET: Classes/Delete/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class _class = db.classes.Include(c => c.Content).Where(x => x.IDClass == id).SingleOrDefault();
            if (_class == null)
            {
                return HttpNotFound();
            }
            ViewBag.isOff = _class.Content.isOfficial;
            return View(_class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class _class = db.classes.Find(id);
            db.classes.Remove(_class);
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
