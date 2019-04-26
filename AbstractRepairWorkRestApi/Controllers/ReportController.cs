using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractRepairWorkRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetStorageLoad()
        {
            var list = _service.GetStorageLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetCustomerBooking(ReportBindingModel model)
        {
            var list = _service.GetCustomerBooking(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveProductPrice(ReportBindingModel model)
        {
            _service.SaveRepairPrice(model);
        }

        [HttpPost]
        public void SaveStorageLoad(ReportBindingModel model)
        {
            _service.SaveStorageLoad(model);
        }

        [HttpPost]
        public void SaveCustomerBooking(ReportBindingModel model)
        {
            _service.SaveCustomerBooking(model);
        }
    }
}
