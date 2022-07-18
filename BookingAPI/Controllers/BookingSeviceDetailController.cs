using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSeviceDetailController : ControllerBase
    {
        private readonly IBookingSeviceDetailService _service;

        public BookingSeviceDetailController(IBookingSeviceDetailService service)
        {
            this._service = service;
        }


        [HttpGet("id")]
        public ActionResult<BookingSeviceDetail> GetBookingRoomDetailById(string id) => _service.GetBookingSeviceDetailById(id);

        [HttpGet("idbill")]
        public ActionResult<IEnumerable<BookingSeviceDetail>> GetBookingSeviceDetailByBill(string id) => _service.GetBookingSeviceDetails(id);


        [HttpPost]
        public IActionResult PortBookingSeviceDetail(BookingSeviceDetail a)
        {
            _service.AddBookingSeviceDetail(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteBookingSeviceDetail(string id)
        {
            var a = _service.GetBookingSeviceDetailById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteBookingSeviceDetail(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateBookingSeviceDetail(string id, BookingSeviceDetail a)
        {
            var aTmp = _service.GetBookingSeviceDetailById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateBookingSeviceDetail(a);
            return NoContent();
        }
    }
}
