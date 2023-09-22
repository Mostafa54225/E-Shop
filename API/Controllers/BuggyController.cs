using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly AppDbContext _context;

        public BuggyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundResult()
        {
            var thing = _context.Products.Find(10233123);
            if (thing == null) return NotFound(new ApiResponse(404));
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(10233123);
            var thingToReturn = thing.ToString();
            return NotFound();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestWithId(int id)
        {
            return Ok();
        }
    }
    
}