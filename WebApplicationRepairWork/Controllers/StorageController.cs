using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class StorageController : Controller
    {
        private IStorageService service = Globals.StorageService;

        public ActionResult List()
        {
            return View(service.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new StorageBindingModel
            {
                StorageName = Request["StorageName"]
            });
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new StorageBindingModel
            {
                Id = id,
                StorageName = viewModel.StorageName
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdateElement(new StorageBindingModel
            {
                Id = int.Parse(Request["Id"]),
                StorageName = Request["StorageName"]
            });
            return RedirectToAction("List");
        }

        public ActionResult More(int id)
        {
            var viewModel = service.GetElement(id);

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            service.DeleteElement(id);
            return RedirectToAction("List");
        }
    }
}