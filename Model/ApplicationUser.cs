using Microsoft.AspNetCore.Identity;

namespace My_Pro.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string? CodeValidation { get; set; }
    }
}
