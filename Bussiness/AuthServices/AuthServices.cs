using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using My_Pro.Data;
using My_Pro.Model;
using My_Pro.Model.DTO;
using My_Pro.Model.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_Pro.Bussiness.AuthServices
{
    public class AuthServices : IAuthServices
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthServices(AppDbContext appDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpContextAccessor = contextAccessor;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpRequest model)
        {
            var user = new ApplicationUser
            {
                FirtName = model.FirtName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }
                await _userManager.AddToRoleAsync(user, AppRole.Customer);
            }
            return result;
        }
        public async Task<string> SignInAsync(SignInRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded)
            {
                throw new AggregateException("UserName or Password not found");
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new AggregateException("User not found");
            }

            return await GenerateJwtToken(user);
        }
        public async Task<List<UserDTO>> GetList(string? keyword)
        {
            var query = _appDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.UserName.ToLower().Contains(keyword.ToLower()));
            }

            var userDTOs = query.Select(user => MapToUserDTO(user));

            return await userDTOs.ToListAsync();
        }
        public async Task<UserDTO> GetById(string id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (user == null)
            {
                throw new AggregateException($"UserId {id} Not Found!");
            }
            var userDTO = MapToUserDTO(user);
            return userDTO;
        }
        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null)
            {
                throw new AggregateException("User not found!");
            }
            var passwordCheckResult = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!passwordCheckResult)
            {
                throw new AggregateException("Invalid old password!");
            }
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result.Succeeded;
        }
        public async Task<bool> SendMail(string mail)
        {
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null)
            {
                throw new AggregateException("Email Not Found!");
            }
            Random random = new Random();
            string mxt = random.Next(100000, 999999).ToString();
            string mailContent = "Mã xác thực Email của bạn là:" + " " + mxt;
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("minhpii181@gmail.com"));
            email.To.Add(MailboxAddress.Parse(mail));
            email.Subject = "Hệ thống xác nhận";
            email.Body = new TextPart(TextFormat.Html) { Text = mailContent };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("minhpii181@gmail.com", "vcjn tfyc wych axjx");
            smtp.Send(email);
            smtp.Disconnect(true);
            user.CodeValidation = mxt;
            _appDbContext.Users.Update(user);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> ReresetPassword(string mail, string mxt, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null)
            {
                throw new AggregateException("Email Not Found!");
            }
            if (!user.CodeValidation.Equals(mxt))
            {
                throw new AggregateException("CodeValidation Not Found!");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new AggregateException("Failed to reset password!");
            }
            return result.Succeeded;
        }
        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static UserDTO MapToUserDTO(ApplicationUser user)
        {
            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                FirtName = user.FirtName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
