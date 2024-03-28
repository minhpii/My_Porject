namespace My_Pro.Model.Request
{
    public class SignUpRequest
    {
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
    public class SignInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
