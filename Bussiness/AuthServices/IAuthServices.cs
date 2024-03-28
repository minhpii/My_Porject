using Microsoft.AspNetCore.Identity;
using My_Pro.Model.DTO;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.AuthServices
{
    public interface IAuthServices
    {
        Task<IdentityResult> SignUpAsync(SignUpRequest model);
        Task<string> SignInAsync(SignInRequest model);
        Task<List<UserDTO>> GetList(string? keyword);
        Task<UserDTO> GetById(string id);
        Task<bool> ChangePassword(string oldPassword, string newPassword);
        Task<bool>SendMail(string mail);
        Task<bool> ReresetPassword(string mail, string mxt, string newPassword);
    }
}
