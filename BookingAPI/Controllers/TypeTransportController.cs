using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeTransportController : ControllerBase
    {
        private readonly ITypeTransportService _service;

        public TypeTransportController(ITypeTransportService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TypeTransport>> GetTypeTransports() => _service.GetTypeTransports();
        [HttpGet("id")]
        public ActionResult<TypeTransport> GetTypeTransportById(string id) => _service.GetTypeTransportById(id);

        [HttpPost]
        public IActionResult PostTypeTransport(TypeTransport a)
        {
            _service.AddTypeTransport(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteTransport(string id)
        {
            var a = _service.GetTypeTransportById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteTypeTransport(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateBill(string id, TypeTransport a)
        {
            var aTmp = _service.GetTypeTransportById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateTypeTransport(a);
            return NoContent();
        }
    }
}
