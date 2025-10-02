namespace Backend.Models.UserData
{
    public class LoginDataDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}