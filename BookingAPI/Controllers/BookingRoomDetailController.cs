using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRoomDetailController : ControllerBase
    {
        private readonly IBookingRoomDetailService _service;

        public BookingRoomDetailController(IBookingRoomDetailService service)
        {
            this._service = service;
        }


        [HttpGet("id")]
        public ActionResult<BookingRoomDetail> GetBookingRoomDetailById(string id) => _service.GetBookingRoomDetailById(id);

        [HttpGet("idbill")]
        public ActionResult<IEnumerable<BookingRoomDetail>> GetBookingRoomDetailByBill(string id) => _service.GetBookingRoomDetails(id);


        [HttpPost]
        public IActionResult PortBookingRoomDetail(BookingRoomDetail a)
        {
            _service.AddBookingRoomDetail(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteBookingRoomDetail(string id)
        {
            var a = _service.GetBookingRoomDetailById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteBookingRoomDetail(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateBookingRoomDetail(string id, BookingRoomDetail a)
        {
            var aTmp = _service.GetBookingRoomDetailById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateBookingRoomDetail(a);
            return NoContent();
        }
    }
}
