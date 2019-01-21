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
    public class RulesController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Rules
        public ActionResult Index(bool isOff, string searchString)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = isOff;

            var rules = db.rules.Where(x => x.Content.isOfficial == isOff).Include(r => r.Content);
            if (!String.IsNullOrEmpty(searchString))
            {
                rules = rules.Where(x => x.title.Contains(searchString));
            }

            return View(rules.ToList());
        }

        // GET: Rules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.models.Rule rule = db.rules.Include(c => c.Content).Where(x => x.IDRule == id).SingleOrDefault();
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = rule.Content.isOfficial;
            return View(rule);
        }

        // GET: Rules/Create
        [Authorize]
        public ActionResult Create(bool isOff)
        {
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");
            ViewBag.isOff = isOff;
            return View();
        }

        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.models.Rule rule)
        {
            if (ModelState.IsValid)
            {
                var r = new Models.models.Rule
                {
                    IDRule = rule.IDRule,
                    title = rule.title,
                    description = rule.description,
                    Content = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name),
                    IDContent = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name).IDContent
                };

                db.rules.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index", new { isOff = r.Content.isOfficial });
            }

            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", rule.IDContent);
            return View(rule);
        }

        // GET: Rules/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.models.Rule rule = db.rules.Include(c => c.Content).Where(x => x.IDRule == id).SingleOrDefault();
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", rule.IDContent);
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = rule.Content.isOfficial;
            return View(rule);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDRule,title,description,IDContent")] Models.models.Rule rule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", rule.IDContent);
            return View(rule);
        }

        // GET: Rules/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.models.Rule rule = db.rules.Include(c => c.Content).Where(x => x.IDRule == id).SingleOrDefault();
            if (rule == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.isOff = rule.Content.isOfficial;
            return View(rule);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.models.Rule rule = db.rules.Find(id);
            var isOff = db.rules.Where(x => x.IDRule == id).Include(a => a.Content).SingleOrDefault().Content.isOfficial;
            db.rules.Remove(rule);
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
