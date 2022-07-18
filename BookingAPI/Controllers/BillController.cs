using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {

        private readonly IBillService  _service;

        public BillController(IBillService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Bill>> GetBill() => _service.GetBills();

        [HttpGet("id")]
        public ActionResult<Bill> GetBillById(string id) => _service.GetBillById(id);
      
        [HttpGet("bill")]
        public ActionResult<string> GetIdCuoi(string bill) => _service.getidcuoi();

        [HttpGet("idacc")]
        public ActionResult<IEnumerable<Bill>> GetBillByAccount(string id) => _service.GetBillByAccount(id);


        [HttpPost]
        public IActionResult PortBill(Bill a)
        {
            _service.AddBill(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteBill(string id)
        {
            var a = _service.GetBillById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteBill(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateBill(string id, Bill a)
        {
            var aTmp = _service.GetBillById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateBill(a);
            return NoContent();
        }
    }
}

