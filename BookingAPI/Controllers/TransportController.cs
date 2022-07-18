using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _service;

        public TransportController(ITransportService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transport>> GetTransports() => _service.GetTransports();
        [HttpGet("id")]
        public ActionResult<Transport> GetTransportById(string id) => _service.GetTransportById(id);

        [HttpGet("type")]
        public ActionResult<IEnumerable<Transport>> getTransportbyType(string Type) => _service.getTransportbyType(Type);
       
        [HttpPost]
        public IActionResult PostTransport(Transport a)
        {
            _service.AddTransport(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteTransport(string id)
        {
            var a = _service.GetTransportById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteTransport(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateBill(string id, Transport a)
        {
            var aTmp = _service.GetTransportById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateTransport(a);
            return NoContent();
        }
    }
}
