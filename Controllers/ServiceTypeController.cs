using Microsoft.AspNetCore.Mvc;
using My_Pro.Bussiness.ServiceTypeServices;
using My_Pro.Model.Request;
using Org.BouncyCastle.Ocsp;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace My_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeServices _serviceTypeServices;
        public ServiceTypeController(IServiceTypeServices serviceTypeServices)
        {
            _serviceTypeServices = serviceTypeServices;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(string? keyword)
        {
            var res = await _serviceTypeServices.GetList(keyword);
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
                var res = await _serviceTypeServices.GetById(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ServiceTypeRequest request)
        {
            var res = await _serviceTypeServices.Create(request);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, ServiceTypeRequest request)
        {
            try
            {
                var res = await _serviceTypeServices.Update(id, request);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _serviceTypeServices.Delete(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
