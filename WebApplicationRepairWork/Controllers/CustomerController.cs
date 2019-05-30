using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerService service = Globals.CustomerService;

        // GET: Customer
        public ActionResult Index()
        {
            return View(service.ListGet());
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new CustomerBindingModel
            {
                CustomerFIO = Request["CustomerFIO"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = service.ElementGet(id);
            var bindingModel = new CustomerBindingModel
            {
                Id = id,
                CustomerFIO = viewModel.CustomerFIO
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdateElement(new CustomerBindingModel
            {
                Id = int.Parse(Request["Id"]),
                CustomerFIO = Request["CustomerFIO"]
            });
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            service.DeleteElement(id);
            return RedirectToAction("Index");
        }
    }
}