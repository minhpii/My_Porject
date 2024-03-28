using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Pro.Bussiness.AuthServices;
using My_Pro.Model.Request;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace My_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(string? keyword)
        {
            var res = await _authServices.GetList(keyword);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var res = await _authServices.GetById(id);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                var res = await _authServices.ChangePassword(oldPassword, newPassword);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(string mail)
        {
            try
            {
                var res = await _authServices.SendMail(mail);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string mail, string mxt, string newPassword)
        {
            try
            {
                var res = await _authServices.ReresetPassword(mail, mxt, newPassword);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequest model)
        {
            try
            {
                var res = await _authServices.SignInAsync(model);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequest model)
        {
            try
            {
                var res = await _authServices.SignUpAsync(model);
                return Ok(res);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
