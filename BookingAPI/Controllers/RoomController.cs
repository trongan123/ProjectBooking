using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRooms() => _service.GetRooms();

        [HttpGet("type")]
        public ActionResult<IEnumerable<Room>> getRoombyType(string Type) => _service.getRoombyType(Type);

        [HttpGet("id")]
        public ActionResult<Room> GetBillById(string id) => _service.GetRoomById(id);

        [HttpGet("num")]
        public ActionResult<Room> SearchRoomByNumberRoom(string num) => _service.SearchRoomByNumberRoom(num);

        [HttpGet("ST")]
        public ActionResult<IEnumerable<Room>> GetRoomByST(int st) => _service.GetRoomByST(st);


        [HttpPost]
        public IActionResult PortRoom(Room a)
        {
            _service.AddRoom(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteRoom(string id)
        {
            var a = _service.GetRoomById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteRoom(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateRoom(string id, Room a)
        {
            var aTmp = _service.GetRoomById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateRoom(a);
            return NoContent();
        }


    }
}
