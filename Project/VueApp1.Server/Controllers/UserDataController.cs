using Microsoft.AspNetCore.Mvc;
using VueApp1.Server.Managers;

namespace VueApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;

        public UserDataController(ILogger<UserDataController> logger)
        {
            _logger = logger;
            
            // 初始化一些示例数据（如果列表为空）
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            var manager = UserDataManager.Instance;
            
            // 只有在列表为空时才初始化示例数据
            if (manager.GetAllUsers().Any())
            {
                return; // 数据已存在，不重复添加
            }
            
            // 添加示例用户数据
            manager.AddUserData(new UserData(1, "张三", "zhangsan@example.com"));
            manager.AddUserData(new UserData(2, "李四", "lisi@example.com"));
            manager.AddUserData(new UserData(3, "王五", "wangwu@example.com"));
            manager.AddUserData(new UserData(4, "赵六", "zhaoliu@example.com"));
            manager.AddUserData(new UserData(5, "孙七", "sunqi@example.com"));
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

            if (user.Id == 0) // UserDataManager 返回默认用户表示未找到
            {
                return NotFound($"用户 ID {id} 未找到");
            }

            return Ok(user);
        }
    }
}
