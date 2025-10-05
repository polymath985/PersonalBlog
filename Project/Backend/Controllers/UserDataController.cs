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
                RegisterTime = newUser.RegisterTime,
                Avatar = newUser.Avatar,
                Bio = newUser.Bio,
                Introduction = newUser.Introduction,
                GitHub = newUser.GitHub,
                Twitter = newUser.Twitter,
                Website = newUser.Website,
                Skills = newUser.Skills
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
                RegisterTime = user.RegisterTime,
                Avatar = user.Avatar,
                Bio = user.Bio,
                Introduction = user.Introduction,
                GitHub = user.GitHub,
                Twitter = user.Twitter,
                Website = user.Website,
                Skills = user.Skills
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
                    RegisterTime = u.RegisterTime,
                    Avatar = u.Avatar,
                    Bio = u.Bio,
                    Introduction = u.Introduction,
                    GitHub = u.GitHub,
                    Twitter = u.Twitter,
                    Website = u.Website,
                    Skills = u.Skills
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetUserDetails(Guid id)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDetailDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    RegisterTime = u.RegisterTime,
                    Avatar = u.Avatar,
                    Bio = u.Bio,
                    Introduction = u.Introduction,
                    GitHub = u.GitHub,
                    Twitter = u.Twitter,
                    Website = u.Website,
                    Skills = u.Skills
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(Guid id, [FromBody] UpdateUserProfileDto profileDto)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // 更新用户资料字段(只更新提供的字段)
            if (profileDto.Avatar != null) user.Avatar = profileDto.Avatar;
            if (profileDto.Bio != null) user.Bio = profileDto.Bio;
            if (profileDto.Introduction != null) user.Introduction = profileDto.Introduction;
            if (profileDto.GitHub != null) user.GitHub = profileDto.GitHub;
            if (profileDto.Twitter != null) user.Twitter = profileDto.Twitter;
            if (profileDto.Website != null) user.Website = profileDto.Website;
            if (profileDto.Skills != null) user.Skills = profileDto.Skills;

            await _dbContext.SaveChangesAsync();

            var updatedUser = new UserDetailDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RegisterTime = user.RegisterTime,
                Avatar = user.Avatar,
                Bio = user.Bio,
                Introduction = user.Introduction,
                GitHub = user.GitHub,
                Twitter = user.Twitter,
                Website = user.Website,
                Skills = user.Skills
            };

            return Ok(updatedUser);
        }

        [HttpGet("stats/{userId}")]
        public async Task<IActionResult> GetUserStats(Guid userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.Blogs)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var stats = new
            {
                totalBlogs = user.Blogs.Count,
                totalViews = user.Blogs.Sum(b => b.Views),
                totalLikes = user.Blogs.Sum(b => b.Likes)
            };

            return Ok(stats);
        }
    }
}