using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Pro.Bussiness.RoomImageServices;
using My_Pro.Bussiness.RoomTypeServices;
using My_Pro.Model.Request;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace My_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeServices _roomTypeServices;
        public RoomTypeController(IRoomTypeServices roomTypeServices)
        {
            _roomTypeServices = roomTypeServices;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(string? keyword)
        {
            var res = await _roomTypeServices.GetList(keyword);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _roomTypeServices.GetById(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(RoomTypeRequest request)
        {
            var res = await _roomTypeServices.Create(request);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, RoomTypeRequest request)
        {
            try
            {
                var res = await _roomTypeServices.Update(id, request);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _roomTypeServices.Delete(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
