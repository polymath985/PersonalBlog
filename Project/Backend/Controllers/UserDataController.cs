using Microsoft.AspNetCore.Mvc;
using Backend.Managers;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly UserDataManager _userDataManager;

        public UserDataController(ILogger<UserDataController> logger, UserDataManager userDataManager)
        {
            _logger = logger;
            _userDataManager = userDataManager;
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserData>>> Get()
        {
            var users = await _userDataManager.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> Get(int id)
        {
            var user = await _userDataManager.GetUserDataByIdAsync(id);

            if (user == null || user.Id == 0) // UserDataManager 返回默认用户表示未找到
            {
                return NotFound($"用户 ID {id} 未找到");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserData>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // 验证输入数据
                if (string.IsNullOrWhiteSpace(request.Name) ||
                    string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest("姓名、邮箱和密码都不能为空");
                }

                // 检查邮箱是否已存在
                var existingUser = await _userDataManager.GetUserDataByEmailAsync(request.Email);

                if (existingUser != null)
                {
                    return Conflict("该邮箱已被注册");
                }

                // 创建新用户（注意：实际应用中密码需要加密存储）
                var newUser = new UserData(request.Name, request.Email, request.Email, request.Password); // 使用邮箱作为账号
                var addedUser = await _userDataManager.AddUserDataAsync(newUser);

                if (addedUser != null)
                {
                    _logger.LogInformation($"新用户注册成功: {addedUser.Email}");

                    // 返回不包含敏感信息的用户数据
                    return Ok(new
                    {
                        Id = addedUser.Id,
                        Name = addedUser.Name,
                        Email = addedUser.Email,
                        RegisterTime = addedUser.RegisterTime
                    });
                }
                else
                {
                    _logger.LogError("在数据库注册用户时出现错误");
                    return StatusCode(500, "注册过程中发生错误");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "用户注册失败");
                return StatusCode(500, "注册过程中发生错误");
            }
        }
        
        [HttpPost("signin")]
        public async Task<ActionResult<UserData>> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                // 验证输入数据
                if (string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest("邮箱和密码都不能为空");
                }

                bool userValid = await _userDataManager.ValidateUserAsync(request);

                if (!userValid)
                {
                    return NotFound($"邮箱或者密码不正确");
                }

                var user = await _userDataManager.GetUserDataByEmailAsync(request.Email);

                // 返回不包含敏感信息的用户数据
                return Ok(new
                {
                    Id = user?.Id,
                    Name = user?.Name,
                    Email = user?.Email,
                    RegisterTime = user?.RegisterTime
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "用户登录失败");
                return StatusCode(500, "登录过程中发生错误");
            }
        }
    }

    public class SignInRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // 注册请求的数据传输对象
    public class RegisterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
