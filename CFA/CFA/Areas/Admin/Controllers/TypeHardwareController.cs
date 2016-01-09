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
    public class TypeHardwareController : Controller
    {
        private ComputerFirmEntities db = new ComputerFirmEntities();

        //
        // GET: /TypeHardware/

        public ActionResult Index(string search = null)
        {
            var model = db.TypeHardwares.ToList();
            ViewBag.search = search;

            model = db.TypeHardwares.Where(x => search == null 
                || x.name.Contains(search)).ToList();

            return View(model);
        }

        //
        // GET: /TypeHardware/Details/5

        public ActionResult Details(int id = 0)
        {
            TypeHardware typehardware = db.TypeHardwares.Find(id);
            if (typehardware == null)
            {
                return HttpNotFound();
            }
            return View(typehardware);
        }

        //
        // GET: /TypeHardware/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TypeHardware/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypeHardware typehardware)
        {
            if (ModelState.IsValid)
            {
                db.TypeHardwares.Add(typehardware);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typehardware);
        }

        //
        // GET: /TypeHardware/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TypeHardware typehardware = db.TypeHardwares.Find(id);
            if (typehardware == null)
            {
                return HttpNotFound();
            }
            return View(typehardware);
        }

        //
        // POST: /TypeHardware/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TypeHardware typehardware)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typehardware).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typehardware);
        }

        //
        // GET: /TypeHardware/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TypeHardware typehardware = db.TypeHardwares.Find(id);
            if (typehardware == null)
            {
                return HttpNotFound();
            }
            return View(typehardware);
        }

        //
        // POST: /TypeHardware/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeHardware typehardware = db.TypeHardwares.Find(id);
            db.TypeHardwares.Remove(typehardware);
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