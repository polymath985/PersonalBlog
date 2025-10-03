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

            var UserDetailDto = new UserDetailDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                RegisterTime = newUser.RegisterTime
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok(UserDetailDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDataDto loginData)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginData.Email && u.Password == loginData.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var UserDetailDto = new UserDetailDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RegisterTime = user.RegisterTime
            };

            return Ok(UserDetailDto);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserDetails(string email)
        {
            var user = await _dbContext.Users
                .Where(u => u.Email == email)
                .Select(u => new UserDetailDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    RegisterTime = u.RegisterTime
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
    }
}