using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class AddMaterialStorageController : Controller
    { 
        private IMaterialService ingredientService = Globals.MaterialService;
        private IStorageService storageService = Globals.StorageService;
        private IServiceMain mainService = Globals.MainService;

        // GET: AddMaterialStorage
        public ActionResult Index()
        {
            var ingredients = new SelectList(ingredientService.ListGet(), "Id", "MaterialName");
            ViewBag.Material = ingredients;

            var storages = new SelectList(storageService.GetList(), "Id", "StorageName");
            ViewBag.Storages = storages;
            return View();
        }

        [HttpPost]
        public ActionResult AddMaterialPost()
        {
            mainService.PutMaterialOnStorage(new StorageMaterialBindingModel
            {
                MaterialId = int.Parse(Request["MaterialId"]),
                StorageId = int.Parse(Request["StorageId"]),
                Count = int.Parse(Request["Count"])
            });
            return RedirectToAction("Index", "Home");
        }
    }
}