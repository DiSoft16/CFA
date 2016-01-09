using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFA.Models;

namespace CFA.Controllers
{
    public class StoreController : Controller
    {
        ComputerFirmEntities hardwareDB = new ComputerFirmEntities();

        [ChildActionOnly]
        public ActionResult MenuHardware()
        {
            var typeHardware = hardwareDB.TypeHardwares.ToList();
            return PartialView(typeHardware);
        }

        public ActionResult Browse(string typehardware)
        {
            // "Жадная" загрузка
            var typeModel = hardwareDB.TypeHardwares.Include("InfoHardwares")
                .Single(g => g.name == typehardware);

            return View(typeModel.InfoHardwares.ToList());
        }
    }
}
