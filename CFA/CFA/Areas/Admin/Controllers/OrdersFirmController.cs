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
    public class OrdersFirmController : Controller
    {
        private ComputerFirmEntities db = new ComputerFirmEntities();

        //
        // GET: /OrdersFirm/

        public ActionResult Index(string numb = null, string hardware = null,
            string firm = null, string execute = null)
        {
            var ordersfirms = db.OrdersFirms.Include(o => o.InfoFirm).Include(o => o.InfoHardware).Include(o => o.OrdersExecute);

            ViewBag.numb = numb;
            ViewBag.hardware = hardware;
            ViewBag.firm = firm;
            ViewBag.execute = execute;
            ViewBag.ExecuteList = db.OrdersExecutes;

            /*model = // m.ToList();

                      db.UserProfiles.Where(x => (x.UserName.Contains(name) || name == null)

                              && (x.email.Contains(email) || email == null)

                              && (x.phone.Contains(phone) || phone == null))
                      .ToList();*/

            if (!String.IsNullOrEmpty(numb) || !String.IsNullOrEmpty(hardware)
                || !String.IsNullOrEmpty(firm) || !String.IsNullOrEmpty(execute))
            {
                int local_numb = !String.IsNullOrEmpty(numb) ? Convert.ToInt32(numb) : 0;
                int local_execute = !String.IsNullOrEmpty(execute) ? Convert.ToInt32(execute) : 0;

                // var local_date = Convert.ToDateTime(date+" "+DateTime.Now.ToString("HH:mm:ss tt"));
                ordersfirms = from orders in db.OrdersFirms
                              where ((local_numb == 0 || orders.numb == local_numb)
                              && (hardware == null || orders.InfoHardware.name.Contains(hardware))
                              && (firm == null || orders.InfoFirm.name.Contains(firm))
                              && (local_execute == 0 || orders.executeId == local_execute)
                              )
                              select orders;
            }

            return View(ordersfirms.ToList());
        }

        //
        // GET: /OrdersFirm/Details/5

        public ActionResult Details(int id = 0)
        {
            OrdersFirm ordersfirm = db.OrdersFirms.Find(id);
            if (ordersfirm == null)
            {
                return HttpNotFound();
            }
            return View(ordersfirm);
        }

        //
        // GET: /OrdersFirm/Create

        public ActionResult Create()
        {
            ViewBag.FirmList = db.InfoFirms;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;

            return View();
        }

        //
        // POST: /OrdersFirm/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdersFirm ordersfirm)
        {
            if (ModelState.IsValid)
            {
                ordersfirm.dateStart = Convert.ToDateTime(ViewBag.dateStart + " " + DateTime.Now.ToString("HH:mm:ss tt"));
                db.OrdersFirms.Add(ordersfirm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FirmList = db.InfoFirms;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;

            return View(ordersfirm);
        }

        //
        // GET: /OrdersFirm/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OrdersFirm ordersfirm = db.OrdersFirms.Find(id);

            if (ordersfirm == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirmList = db.InfoFirms;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;

            return View(ordersfirm);
        }

        //
        // POST: /OrdersFirm/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrdersFirm ordersfirm)
        {
            if (ModelState.IsValid)
            {
                ordersfirm.dateStart = Convert.ToDateTime(ViewBag.dateStart + " " + DateTime.Now.ToString("HH:mm:ss tt"));
                db.Entry(ordersfirm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FirmList = db.InfoFirms;
            ViewBag.HardwareList = db.InfoHardwares;
            ViewBag.ExecuteList = db.OrdersExecutes;

            return View(ordersfirm);
        }

        //
        // GET: /OrdersFirm/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OrdersFirm ordersfirm = db.OrdersFirms.Find(id);
            if (ordersfirm == null)
            {
                return HttpNotFound();
            }
            return View(ordersfirm);
        }

        //
        // POST: /OrdersFirm/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdersFirm ordersfirm = db.OrdersFirms.Find(id);
            db.OrdersFirms.Remove(ordersfirm);
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