using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Pro.Bussiness.RoomImageServices;

namespace My_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomImageController : ControllerBase
    {
        private readonly IRoomImageServices _roomImageServices;
        public RoomImageController(IRoomImageServices roomImageServices)
        {
            _roomImageServices = roomImageServices;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddImageToRoom(int roomId, IFormFile file)
        {
            try
            {
                var res = await _roomImageServices.AddImageToRoom(roomId, file);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> RemoveImageToRoom(int id)
        {
            try
            {
                var res = await _roomImageServices.RemoveImageToRoom(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
