using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFA.Models;
using System.Data.SqlClient;

/*
 * Database System
 * 14.12.2015
 * SEVNTU & R.I.V.
 * */

namespace CFA.Areas.Admin.Controllers
{
    public class UserProfileController : Controller
    {
        private ComputerFirmEntities db = new ComputerFirmEntities();

        //
        // GET: /UserProfile/
        [Authorize(Roles = "Admin")] // доступ для действия только администратору
        public ActionResult Index(string name = null, string email = null,
            string phone = null)
        {
            var model = (from users in db.UserProfiles join user 
                            in db.webpages_Membership on users.UserId equals user.UserId
                             select new UserProfileView { 
                             UserId=users.UserId,
                             UserName = users.UserName,
                             email = users.email,
                             phone = users.phone,
                             CreateDate = user.CreateDate
                         }).ToList();


            ViewBag.name = name;
            ViewBag.email = email;
            ViewBag.phone = phone;

            if (!String.IsNullOrEmpty(name) || !String.IsNullOrEmpty(email)
                || !String.IsNullOrEmpty(phone))
            {
                model = (from users in db.UserProfiles
                         join user in db.webpages_Membership on users.UserId equals user.UserId
                         where ((name == null || users.UserName.Contains(name))
                        && (email == null || users.email.Contains(email))
                        && (phone == null || users.phone.Contains(phone)
                        ))
                         select new UserProfileView
                         {
                             UserId = users.UserId,
                             UserName = users.UserName,
                             email = users.email,
                             phone = users.phone,
                             CreateDate = user.CreateDate
                         }).ToList();

            }

            return View(model);
        }

        //
        // GET: /UserProfile/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /UserProfile/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserProfile/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userprofile);
        }

        //
        // GET: /UserProfile/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /UserProfile/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // GET: /UserProfile/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /UserProfile/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}