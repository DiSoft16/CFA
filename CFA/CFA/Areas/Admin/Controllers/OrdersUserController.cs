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
    public class OrdersUserController : Controller
    {
        private ComputerFirmEntities db = new ComputerFirmEntities();

        //
        // GET: /OrdersUser/

        public ActionResult Index(string brand = null, string hardware = null,
            string numb = null, string user = null, string execute = null)
        {
            var ordersusers = db.OrdersUsers.Include(o => o.InfoHardware).Include(o => o.OrdersExecute)
                .Include(o => o.UserProfile).Include(o=>o.InfoHardware.BrandHardware);

            ViewBag.brand = brand;
            ViewBag.hardware = hardware;
            ViewBag.numb = numb;
            ViewBag.user = user;
            ViewBag.execute = execute;

            ViewBag.BrandList = db.BrandHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;

            if (!String.IsNullOrEmpty(brand)  || !String.IsNullOrEmpty(hardware)
                || !String.IsNullOrEmpty(numb) || !String.IsNullOrEmpty(user) || !String.IsNullOrEmpty(execute))
            {
                int local_brand = !String.IsNullOrEmpty(brand) ? Convert.ToInt32(brand) : 0;
                int local_numb = !String.IsNullOrEmpty(numb) ? Convert.ToInt32(numb) : 0;
                int local_execute = !String.IsNullOrEmpty(execute) ? Convert.ToInt32(execute) : 0;

                ordersusers = from orders in db.OrdersUsers
                              where ((local_brand == 0 || orders.InfoHardware.BrandId==local_brand)
                              && (hardware == null || orders.InfoHardware.name.Contains(hardware))
                              && (local_numb == 0 || orders.numb == local_numb)
                              && (user == null || orders.UserProfile.UserName.Contains(user))
                              && (local_execute == 0 || orders.ExecuteId == local_execute)
                              )
                              select orders;
            }
            
            return View(ordersusers.ToList());
        }

        //
        // GET: /OrdersUser/Details/5

        public ActionResult Details(int id = 0)
        {
            OrdersUser ordersuser = db.OrdersUsers.Find(id);
            if (ordersuser == null)
            {
                return HttpNotFound();
            }
            return View(ordersuser);
        }

        //
        // GET: /OrdersUser/Create

        public ActionResult Create()
        {
            ViewBag.BrandList = db.BrandHardwares;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;
            ViewBag.UserList = db.UserProfiles;

            return View();
        }

        //
        // POST: /OrdersUser/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdersUser ordersuser)
        {
            if (ModelState.IsValid)
            {
                ordersuser.dateStart = Convert.ToDateTime(ViewBag.date + " " + DateTime.Now.ToString("HH:mm:ss tt"));
                db.OrdersUsers.Add(ordersuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandList = db.BrandHardwares;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;
            ViewBag.UserList = db.UserProfiles;

            return View(ordersuser);
        }

        //
        // GET: /OrdersUser/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OrdersUser ordersuser = db.OrdersUsers.Find(id);
            if (ordersuser == null)
            {
                return HttpNotFound();
            }

            ViewBag.BrandList = db.BrandHardwares;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;
            ViewBag.UserList = db.UserProfiles;

            return View(ordersuser);
        }


        //
        // POST: /OrdersUser/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrdersUser ordersuser)
        {
            if (ModelState.IsValid)
            {
                ordersuser.dateStart = Convert.ToDateTime(ViewBag.dateStart + " " + DateTime.Now.ToString("HH:mm:ss tt"));
                db.Entry(ordersuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandList = db.BrandHardwares;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;
            ViewBag.UserList = db.UserProfiles;

            return View(ordersuser);
        }

        //
        // GET: /OrdersUser/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OrdersUser ordersuser = db.OrdersUsers.Find(id);
            if (ordersuser == null)
            {
                return HttpNotFound();
            }
            return View(ordersuser);
        }

        //
        // POST: /OrdersUser/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdersUser ordersuser = db.OrdersUsers.Find(id);
            db.OrdersUsers.Remove(ordersuser);
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