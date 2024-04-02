using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Pro.Bussiness.RoomImageServices;
using My_Pro.Model.Request;
using static My_Pro.Data.Enum;

namespace My_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomService;
        public RoomController(IRoomServices roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(string? keyword)
        {
            var res = await _roomService.GetList(keyword);
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
                var res = await _roomService.GetById(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(RoomRequest request)
        {
            try
            {
                var res = await _roomService.Create(request);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id,RoomRequest request)
        {
            try
            {
                var res = await _roomService.Update(id,request);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRoomStatus")]
        public async Task<IActionResult> UpdateRoomStatus(int id, RoomStatus status)
        {
            try
            {
                var res = await _roomService.UpdateRoomStatus(id, status);
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
                var res = await _roomService.Delete(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
