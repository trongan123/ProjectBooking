using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices() => _service.GetServices();

        [HttpGet("id")]
        public ActionResult<Service> GetServiceById(string id) => _service.GetServiceById(id);

     
        [HttpPost]
        public IActionResult PortService(Service a)
        {
            _service.AddService(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteService(string id)
        {
            var a = _service.GetServiceById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteService(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateService(string id, Service a)
        {
            var aTmp = _service.GetServiceById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateService(a);
            return NoContent();
        }
    }
}
