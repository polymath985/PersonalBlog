using Microsoft.AspNetCore.Mvc;
using Backend.Managers;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;

        public UserDataController(ILogger<UserDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllUsers")]
        public ActionResult<IEnumerable<UserData>> Get()
        {
            var users = UserDataManager.Instance.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserData> Get(int id)
        {
            var user = UserDataManager.Instance.GetUserDataById(id);

            if (user == null || user.Id == 0) // UserDataManager 返回默认用户表示未找到
            {
                return NotFound($"用户 ID {id} 未找到");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public ActionResult<UserData> Register([FromBody] RegisterRequest request)
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
                var existingUser = UserDataManager.Instance.GetUserDataByEmail(request.Email);

                if (existingUser != null)
                {
                    return Conflict("该邮箱已被注册");
                }

                // 创建新用户（注意：实际应用中密码需要加密存储）
                var newUser = new UserData(request.Name, request.Email, request.Email, request.Password); // 使用邮箱作为账号
                var addedUser = UserDataManager.Instance.AddUserData(newUser);

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
        public ActionResult<UserData> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                // 验证输入数据
                if (string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest("邮箱和密码都不能为空");
                }

                bool userValid = UserDataManager.Instance.ValidateUser(request);

                if (!userValid)
                {
                    return NotFound($"邮箱或者密码不正确");
                }

                var user = UserDataManager.Instance.GetUserDataByEmail(request.Email);

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
