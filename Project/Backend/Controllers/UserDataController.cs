using Microsoft.AspNetCore.Mvc;
using Backend.Models.UserData;
using PersonalBlog.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController(DataContext _dbContext) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userData)
        {
            // 检查是否已存在相同邮箱的用户
            var existingUser = await _dbContext.Users.AnyAsync(u => u.Email == userData.Email || u.Name == userData.Name);
            if (existingUser)
            {
                return BadRequest("Email or Name is already registered.");
            }

            var newUser = new UserData
            {
                Email = userData.Email,
                Password = userData.Password,
                Name = userData.Name,
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDataDto loginData)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginData.Email && u.Password == loginData.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { Message = "Login successful", UserId = user.Id, UserName = user.Name });
        }
    }
}