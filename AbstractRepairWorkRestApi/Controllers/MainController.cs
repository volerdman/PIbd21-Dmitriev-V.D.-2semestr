using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkRestApi.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AbstractRepairWorkRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IServiceMain _service;

        private readonly IExecutorService _serviceExecutor;

        public MainController(IServiceMain service, IExecutorService
serviceExecutor)
        {
            _service = service;
            _serviceExecutor = serviceExecutor;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.ListGet();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateBooking(BookingBindingModel model)
        {
            _service.CreateBooking(model);
        }

        [HttpPost]
        public void PayBooking(BookingBindingModel model)
        {
            _service.PayBooking(model);
        }

        [HttpPost]
        public void PutMaterialOnStorage(StorageMaterialBindingModel model)
        {
            _service.PutMaterialOnStorage(model);
        }

        [HttpPost]
        public void StartWork()
        {
            List<BookingViewModel> bookings = _service.GetFreeBookings();
            foreach (var order in bookings)
            {
                ExecutorViewModel impl = _serviceExecutor.GetFreeWorker();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkExecutor(_service, _serviceExecutor, impl.Id, order.Id);
            }
        }
    }
}
