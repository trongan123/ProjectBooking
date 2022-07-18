using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeSevice _service;

        public RoomTypeController(IRoomTypeSevice service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomType>> GetBill() => _service.GetRoomTypes();

        [HttpGet("id")]
        public ActionResult<RoomType> GetRoomTypeById(string id) => _service.GetRoomTypeById(id);

        [HttpPost]
        public IActionResult PortRoomType(RoomType a)
        {
            _service.AddRoomType(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteRoomType(string id)
        {
            var a = _service.GetRoomTypeById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteRoomType(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateRoomType(string id, RoomType a)
        {
            var aTmp = _service.GetRoomTypeById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateRoomType(a);
            return NoContent();
        }
    }
}
