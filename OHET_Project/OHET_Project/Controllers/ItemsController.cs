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
    public class ItemsController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Items
        public ActionResult Index(bool isOff, string searchString)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = isOff;

            var items = db.items.Where(x => x.Content.isOfficial == isOff).Include(i => i.Content);
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(x => x.name.Contains(searchString));
            }

            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Include(c => c.Content).Where(x => x.IDItem == id).SingleOrDefault();
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = item.Content.isOfficial;
            return View(item);
        }

        // GET: Items/Create
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Create(bool isOff)
        {
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            ViewBag.isOff = isOff;
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                var i = new Item
                {
                    IDItem = item.IDItem,
                    name = item.name,
                    conceptLvl = CountWords(item.description),
                    description = item.description,
                    cost = item.cost,
                    Content = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name),
                    IDContent = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name).IDContent
                };

                db.items.Add(i);
                db.SaveChanges();
                return RedirectToAction("Index", new { isOff = i.Content.isOfficial });
            }

            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", item.IDContent);
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Include(c => c.Content).Where(x => x.IDItem == id).SingleOrDefault();
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", item.IDContent);
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = item.Content.isOfficial;
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDItem,Name,Cost,Notes,IDContent")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", item.IDContent);
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.items.Include(c => c.Content).Where(x => x.IDItem == id).SingleOrDefault();
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = item.Content.isOfficial;
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.items.Find(id);
            var isOff = db.items.Where(x => x.IDItem == id).Include(a => a.Content).SingleOrDefault().Content.isOfficial;
            db.items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index", new { isOff = isOff });
        }

        private int CountWords(string str)
        {
            var text = str.Trim();
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            return wordCount;
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
