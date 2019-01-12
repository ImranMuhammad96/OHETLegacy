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
            ViewBag.userId = User.Identity.GetUserId();
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
            if (ModelState.IsValid)
            {
                var u = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user.Id);
                var uid = u.Roles.SingleOrDefault().RoleId;
                var role = db.Roles.SingleOrDefault(r => r.Id == uid).Name;

                if (role != "Banned")
                {
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().RemoveFromRole(user.Id, role);
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, "Banned");
                }
                db.Entry(user).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult ChangeToUser(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var u = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user.Id);
                var uid = u.Roles.SingleOrDefault().RoleId;
                var role = db.Roles.SingleOrDefault(r => r.Id == uid).Name;

                if (role != "User")
                {
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().RemoveFromRole(user.Id, role);
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, "User");
                }
                db.Entry(user).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult ChangeToEditor(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var u = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user.Id);
                var uid = u.Roles.SingleOrDefault().RoleId;
                var role = db.Roles.SingleOrDefault(r => r.Id == uid).Name;

                if (role != "Editor")
                {
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().RemoveFromRole(user.Id, role);
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, "Editor");
                }
                db.Entry(user).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult ChangeToAdmin(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var u = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user.Id);
                var uid = u.Roles.SingleOrDefault().RoleId;
                var role = db.Roles.SingleOrDefault(r => r.Id == uid).Name;

                if (role != "Admin")
                {
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().RemoveFromRole(user.Id, role);
                    Request.GetOwinContext().GetUserManager<ApplicationUserManager>().AddToRole(user.Id, "Admin");
                }
                db.Entry(user).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            return View(user);
        }
        /*
        public ActionResult ChangeRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            ViewBag.Roles = new SelectList(db.Roles, "Name", "Name");
            ViewBag.AppUser = applicationUser;
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        */
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
