using System.ComponentModel.DataAnnotations;

namespace Backend.Models.UserData
{
    public class RegisterUserDto
    {
        [Length(1, 10)]
        public required string Name { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [MinLength(6)]
        public required string Password { get; set; }
    }
}