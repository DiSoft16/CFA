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
    public class InfoHardwareController : Controller
    {
        private ComputerFirmEntities db = new ComputerFirmEntities();

        //
        // GET: /InfoHardware/

        public ActionResult Index(string category = null, string brand = null, string name = null,
            string available = null, string order = null, string cost = null)
        {
            // Eagerloading 
            var infohardwares = db.InfoHardwares.Include(i => i.TypeHardware).Include(i=>i.BrandHardware);
            
            ViewBag.category = category;
            ViewBag.brand = brand;
            ViewBag.name = name;
            ViewBag.available = available;
            ViewBag.order = order;
            ViewBag.cost = cost;

            ViewBag.CategoryList = db.TypeHardwares;
            ViewBag.BrandList = db.BrandHardwares;

            if (!String.IsNullOrEmpty(category) || !String.IsNullOrEmpty(brand) || !String.IsNullOrEmpty(name)
                || !String.IsNullOrEmpty(available) || !String.IsNullOrEmpty(order)
                || !String.IsNullOrEmpty(cost))
            {
                // присваиваем числовые значения для поиска
                int local_category = !String.IsNullOrEmpty(category) ? Convert.ToInt32(category) : 0;
                int local_brand = !String.IsNullOrEmpty(brand) ? Convert.ToInt32(brand) : 0;
                int local_available = !String.IsNullOrEmpty(available) ? Convert.ToInt32(available) : 0;
                int local_order = !String.IsNullOrEmpty(order) ? Convert.ToInt32(order) : 0;
                int local_cost = !String.IsNullOrEmpty(cost) ? Convert.ToInt32(cost) : 0;

                infohardwares = from info in db.InfoHardwares
                                where ((local_category == 0 || info.TypeHardware.Id == local_category)
                               && (local_brand == 0 || info.BrandHardware.Id == local_brand)
                               && (name == null || info.name.Contains(name))
                               && (local_available == 0 || info.numbAvailable == local_available)
                               && (local_order == 0 || info.numbOrder == local_order)
                               && (local_cost == 0 || info.cost == local_cost)
                              )
                                select info;
            }
            return View(infohardwares.ToList());
        }

        //
        // GET: /InfoHardware/Details/5

        public ActionResult Details(int id = 0)
        {
            InfoHardware infohardware = db.InfoHardwares.Find(id);
            if (infohardware == null)
            {
                return HttpNotFound();
            }
            return View(infohardware);
        }

        //
        // GET: /InfoHardware/Create

        public ActionResult Create()
        {
            ViewBag.TypeList = db.TypeHardwares;
            ViewBag.BrandList = db.BrandHardwares;
            return View();
        }

        //
        // POST: /InfoHardware/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InfoHardware infohardware, HttpPostedFileBase upload)
        {
            ViewBag.TypeList = db.TypeHardwares;
            ViewBag.BrandList = db.BrandHardwares;

            if (ModelState.IsValid)
            {

                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Uploads/ImageHardware/" + fileName));
                    // в модель
                    infohardware.fname = upload.FileName;
                }
            
                db.InfoHardwares.Add(infohardware);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(infohardware);
        }

        //
        // GET: /InfoHardware/Edit/5

        public ActionResult Edit(int id = 0)
        {
            InfoHardware infohardware = db.InfoHardwares.Find(id);
            ViewBag.TypeList = db.TypeHardwares;
            ViewBag.BrandList = db.BrandHardwares;

            if (infohardware == null)
            {
                return HttpNotFound();
            }

            return View(infohardware);
        }

        //
        // POST: /InfoHardware/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InfoHardware infohardware, HttpPostedFileBase upload)
        {
            ViewBag.TypeList = db.TypeHardwares;
            ViewBag.BrandList = db.BrandHardwares;

            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Uploads/ImageHardware/" + fileName));
                // в модель
                infohardware.fname = upload.FileName;
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(infohardware).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(infohardware);
        }

        //
        // GET: /InfoHardware/Delete/5

        public ActionResult Delete(int id = 0)
        {
            InfoHardware infohardware = db.InfoHardwares.Find(id);
            if (infohardware == null)
            {
                return HttpNotFound();
            }
            return View(infohardware);
        }

        //
        // POST: /InfoHardware/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InfoHardware infohardware = db.InfoHardwares.Find(id);
            db.InfoHardwares.Remove(infohardware);
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