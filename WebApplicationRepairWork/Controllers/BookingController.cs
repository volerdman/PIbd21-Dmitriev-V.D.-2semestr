using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationRepairWork.Controllers
{
    public class BookingController : Controller
    {
        private IRepairService repairsService = Globals.RepairService;
        private IServiceMain repairOrderService = Globals.MainService;
        private ICustomerService customerService = Globals.CustomerService;


        // GET: Booking
        public ActionResult Index()
        {
            return View(repairOrderService.ListGet());
        }

        public ActionResult Create()
        {
            var repairs = new SelectList(repairsService.ListGet(), "Id", "RepairName");
            var customers = new SelectList(customerService.ListGet(), "Id", "CustomerFIO");
            ViewBag.Repairs = repairs;
            ViewBag.Customers = customers;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            var customerId = int.Parse(Request["CustomerId"]);
            var repairId = int.Parse(Request["RepairId"]);
            var repairCount = int.Parse(Request["Count"]);
            var totalCost = CalcSum(repairId, repairCount);

            repairOrderService.CreateBooking(new BookingBindingModel
            {
                CustomerId = customerId,
                RepairId = repairId,
                Count = repairCount,
                Sum = totalCost

            });
            return RedirectToAction("Index");
        }

        private Decimal CalcSum(int repairId, int repairCount)
        {
            RepairViewModel repair = repairsService.ElementGet(repairId);
            return repairCount * repair.Cost;
        }

        public ActionResult SetStatus(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Processing":
                        repairOrderService.TakeBookingInWork(new BookingBindingModel { Id = id });
                        break;
                    case "Ready":
                        repairOrderService.FinishBooking(new BookingBindingModel { Id = id });
                        break;
                    case "Paid":
                        repairOrderService.PayBooking(new BookingBindingModel { Id = id });
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }


            return RedirectToAction("Index");
        }
    }
}