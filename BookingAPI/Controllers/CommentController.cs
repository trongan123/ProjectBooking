using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetComments() => _service.GetComments();

        [HttpGet("id")]
        public ActionResult<Comment> GetCommentById(string id) => _service.GetCommentById(id);

        [HttpGet("idacc")]
        public ActionResult<IEnumerable<Comment>> GetCommentsByAccount(string id) => _service.GetCommentsByAccount(id);


        [HttpPost]
        public IActionResult PortComment(Comment a)
        {
            _service.AddComment(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteComment(string id)
        {
            var a = _service.GetCommentById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteComment(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateComment(string id, Comment a)
        {
            var aTmp = _service.GetCommentById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateComment(a);
            return NoContent();
        }
    }
}
