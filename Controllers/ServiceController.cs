using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Pro.Bussiness.ServiceServices;
using My_Pro.Model.Request;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace My_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceServices _serviceServices;
        public ServiceController(IServiceServices serviceServices)
        {
            _serviceServices = serviceServices;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(string? keyword)
        {
            var res = await _serviceServices.GetList(keyword);
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
                var res = await _serviceServices.GetById(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            try
            {
                var res = await _serviceServices.Create(request);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, ServiceRequest request)
        {
            try
            {
                var res = await _serviceServices.Update(id, request);
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
                var res = await _serviceServices.Delete(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
