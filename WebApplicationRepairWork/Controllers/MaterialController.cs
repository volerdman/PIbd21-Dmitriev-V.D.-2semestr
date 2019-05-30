using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class MaterialController : Controller
    {
        private IMaterialService service = Globals.MaterialService;
        // GET: Materials
        public ActionResult Index()
        {
            return View(service.ListGet());
        }


        // GET: Materials/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new MaterialBindingModel
            {
                MaterialName = Request["MaterialName"]
            });
            return RedirectToAction("Index");
        }


        // GET: Materials/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = service.ElementGet(id);
            var bindingModel = new MaterialBindingModel
            {
                Id = id,
                MaterialName = viewModel.MaterialName
            };
            return View(bindingModel);
        }


        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdateElement(new MaterialBindingModel
            {
                Id = int.Parse(Request["Id"]),
                MaterialName = Request["MaterialName"]
            });
            return RedirectToAction("Index");
        }


        // GET: Materials/Delete/5
        public ActionResult Delete(int id)
        {
            service.DeleteElement(id);
            return RedirectToAction("Index");
        }
    }
}