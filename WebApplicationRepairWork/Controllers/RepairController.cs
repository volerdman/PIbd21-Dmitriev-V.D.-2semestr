using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class RepairController : Controller
    {
        private IRepairService service = Globals.RepairService;
        private IMaterialService ingredientService = Globals.MaterialService;

        // GET: Repairs
        public ActionResult Index()
        {
            if (Session["Repair"] == null)
            {
                var repair = new RepairViewModel();
                repair.MaterialRepair = new List<MaterialRepairViewModel>();
                Session["Repair"] = repair;
            }
            return View((RepairViewModel)Session["Repair"]);
        }

        public ActionResult AddMaterial()
        {
            var ingredients = new SelectList(ingredientService.ListGet(), "Id", "MaterialName");
            ViewBag.Material = ingredients;
            return View();
        }

        [HttpPost]
        public ActionResult AddMaterialPost()
        {
            var repair = (RepairViewModel)Session["Repair"];
            var ingredient = new MaterialRepairViewModel
            {
                MaterialId = int.Parse(Request["Id"]),
                MaterialName = ingredientService.ElementGet(int.Parse(Request["Id"])).MaterialName,
                Count = int.Parse(Request["Count"])
            };
            repair.MaterialRepair.Add(ingredient);
            Session["Repair"] = repair;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateRepairPost()
        {
            var repair = (RepairViewModel)Session["Repair"];
            var materialRepair = new List<MaterialRepairBindingModel>();
            for (int i = 0; i < repair.MaterialRepair.Count; ++i)
            {
                materialRepair.Add(new MaterialRepairBindingModel
                {
                    Id = repair.MaterialRepair[i].Id,
                    RepairId = repair.MaterialRepair[i].RepairId,
                    MaterialId = repair.MaterialRepair[i].MaterialId,
                    Count = repair.MaterialRepair[i].Count
                });
            }
            service.AddElement(new RepairBindingModel
            {
                RepairName = Request["RepairName"],
                Cost = Convert.ToDecimal(Request["Cost"]),
                MaterialRepair = materialRepair
            });
            Session.Remove("Repair");
            return RedirectToAction("Index", "Repair");
        }
    }
}