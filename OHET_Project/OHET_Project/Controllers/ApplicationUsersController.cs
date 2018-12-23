using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OHET_Project.Models;
using OHET_Project.Persistence;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace OHET_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        public ActionResult Ban(ApplicationUser user)
        {
            //db.Users.SingleOrDefault(u => u.Id == id).Roles.SingleOrDefault().RoleId = "3";

            //Contact contact = dbContext.Contacts.Single(c => c.Id == 12345);
            //ContactType contactType = dbContext.ContactType.Single(c => c.Id == 3);
            //contact.ContactType = contactType;

            //db.Roles.SingleOrDefault(r => r.Users.FirstOrDefault(u => u.UserId == id).UserId == id).Id = "3";
            //db.SaveChanges();

            if (ModelState.IsValid)
            {
                // THIS LINE IS IMPORTANT
                var oldUser = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user.Id);
                var oldRoleId = oldUser.Roles.SingleOrDefault().RoleId;
                var oldRoleName = db.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

                if (oldRoleName != "Banned")
                {
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().RemoveFromRole(user.Id, oldRoleName);
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, "Banned");
                }
                db.Entry(user).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Unban(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                // THIS LINE IS IMPORTANT
                var oldUser = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user.Id);
                var oldRoleId = oldUser.Roles.SingleOrDefault().RoleId;
                var oldRoleName = db.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

                if (oldRoleName != "User")
                {
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().RemoveFromRole(user.Id, oldRoleName);
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, "User");
                }
                db.Entry(user).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public virtual ActionResult ChangeRole(ApplicationUser user, string role)
        {
            if (ModelState.IsValid)
            {
                // THIS LINE IS IMPORTANT
                var oldUser = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user.Id);
                var oldRoleId = oldUser.Roles.SingleOrDefault().RoleId;
                var oldRoleName = db.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

                if (oldRoleName != role)
                {
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().RemoveFromRole(user.Id, oldRoleName);
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, role);
                }
                db.Entry(user).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
