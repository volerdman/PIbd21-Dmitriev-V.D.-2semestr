using AbstractRepairServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class RepairsController : Controller
    {
        public IRepairService service = Globals.RepairService;
        // GET: Repairs
        public ActionResult Index()
        {
            return View(service.ListGet());
        }

        public ActionResult Delete(int id)
        {
            service.DeleteElement(id);
            return RedirectToAction("Index");
        }
    }
}