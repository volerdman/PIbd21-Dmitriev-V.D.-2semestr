using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractRepairWorkRestApi.Controllers
{
    public class MaterialController : ApiController
    {
        private readonly IMaterialService _service;
        public MaterialController(IMaterialService service)
        {
            _service = service;
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
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.ElementGet(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }
        [HttpPost]
        public void AddElement(MaterialBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(MaterialBindingModel model)
        {
            _service.UpdateElement(model);
        }
        [HttpPost]
        public void DelElement(MaterialBindingModel model)
        {
            _service.DeleteElement(model.Id);
        }
    }
}
