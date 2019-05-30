using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Materials()
        {
            return RedirectToAction("Index", "Material");
        }
    }
}
