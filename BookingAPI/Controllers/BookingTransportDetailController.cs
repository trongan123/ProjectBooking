using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTransportDetailController : ControllerBase
    {
        private readonly IBookingTransportDetailService _service;

        public BookingTransportDetailController(IBookingTransportDetailService service)
        {
            this._service = service;
        }


        [HttpGet("id")]
        public ActionResult<BookingTransportDetail> GetBookingRoomDetailById(string id) => _service.GetBookingTransportDetailById(id);

        [HttpGet("idbill")]
        public ActionResult<IEnumerable<BookingTransportDetail>> GetBookingSeviceDetailByBill(string id) => _service.GetBookingTransportDetails(id);


        [HttpPost]
        public IActionResult PortBookingTransportDetail(BookingTransportDetail a)
        {
            _service.AddBookingTransportDetail(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteBookingTransportDetail(string id)
        {
            var a = _service.GetBookingTransportDetailById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteBookingTransportDetail(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateBookingTransportDetail(string id, BookingTransportDetail a)
        {
            var aTmp = _service.GetBookingTransportDetailById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateBookingTransportDetail(a);
            return NoContent();
        }
    }
}
