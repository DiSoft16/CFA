using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using CFA.Filters;
using CFA.Models;

/*
 * Database System
 * 04.11.2015
 * SEVNTU & R.I.V.
 * https://www.macaw.nl/weblog/2013/5/setting-up-a-solution-with-mvc4-and-twitter-bootstrap
 * http://www.asp.net/mvc/overview/older-versions-1/movie-database/create-a-movie-database-application-in-15-minutes-with-asp-net-mvc-cs
 * http://andrey.moveax.ru/post/mvc4-allowanonymous-attribute
 * http://www.asp.net/mvc/overview/getting-started/database-first-development/creating-the-web-application
 * http://cybarlab.com/crud-operation-in-asp-net-mvc-using-entity-framework - setting controller
 * http://www.advancesharp.com/blog/1145/mvc-dropdownlistfor-fill-on-selection-change-of-another-dropdown - DropDownListFor setup
 * https://dzone.com/articles/tip-day-default-user-roles - Setup user roles
 * http://www.dotnetfunda.com/articles/show/3139/webgrid-in-aspnet-mvc - webgrid
 * http://www.asp.net/mvc/overview/older-versions/hands-on-labs/aspnet-mvc-4-models-and-data-access#AppendixC - main page
 * http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/reading-related-data-with-the-entity-framework-in-an-asp-net-mvc-application - on Store result search
 */

namespace CFA.Controllers
{
    // защита страниц для незарегистрированных пользователей
    [Authorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
