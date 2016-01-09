using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFA.Models;

/*
 * Database System
 * 14.12.2015
 * SEVNTU & R.I.V.
 * */

namespace CFA.Areas.Admin.Controllers
{
    public class InfoFirmController : Controller
    {
        private ComputerFirmEntities db = new ComputerFirmEntities();

        //
        // GET: /InfoFirm/

        public ActionResult Index(string name = null, string email = null, string phone = null)
        {
            var model = db.InfoFirms.ToList();

            ViewBag.name = name;
            ViewBag.email = email;
            ViewBag.phone = phone;

            if (!String.IsNullOrEmpty(name) || !String.IsNullOrEmpty(email)
                || !String.IsNullOrEmpty(phone))
            {
                model = (from providers in db.InfoFirms
                        where ((name == null || providers.name.Contains(name))
                              && (email == null || providers.email.Contains(email))
                              && (phone == null || providers.phone.Contains(phone)))
                        select providers).ToList();
            }

            return View(model);
        }

        //
        // GET: /InfoFirm/Details/5

        public ActionResult Details(int id = 0)
        {
            InfoFirm infofirm = db.InfoFirms.Find(id);
            if (infofirm == null)
            {
                return HttpNotFound();
            }
            return View(infofirm);
        }

        //
        // GET: /InfoFirm/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /InfoFirm/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InfoFirm infofirm)
        {
            if (ModelState.IsValid)
            {
                db.InfoFirms.Add(infofirm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(infofirm);
        }

        //
        // GET: /InfoFirm/Edit/5

        public ActionResult Edit(int id = 0)
        {
            InfoFirm infofirm = db.InfoFirms.Find(id);
            if (infofirm == null)
            {
                return HttpNotFound();
            }
            return View(infofirm);
        }

        //
        // POST: /InfoFirm/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InfoFirm infofirm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infofirm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(infofirm);
        }

        //
        // GET: /InfoFirm/Delete/5

        public ActionResult Delete(int id = 0)
        {
            InfoFirm infofirm = db.InfoFirms.Find(id);
            if (infofirm == null)
            {
                return HttpNotFound();
            }
            return View(infofirm);
        }

        //
        // POST: /InfoFirm/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InfoFirm infofirm = db.InfoFirms.Find(id);
            db.InfoFirms.Remove(infofirm);
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