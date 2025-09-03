namespace Backend
{
    public class UserData
    {
        public UserData()
        {
            RegisterTime = DateTime.Now;
        }

        public UserData(string name, string account, string email)
        {
            Name = name;
            Account = account;
            Email = email;
            RegisterTime = DateTime.Now;
        }

        public UserData(string name, string account, string email, string password)
        {
            Name = name;
            Account = account;
            Email = email;
            Password = password;
            RegisterTime = DateTime.Now;
        }

        public UserData(int id, string name, string account, string email, DateTime registerTime)
        {
            Id = id;
            Name = name;
            Account = account;
            Email = email;
            RegisterTime = registerTime;
        }

        public UserData(int id, string name, string account, string email, string password, DateTime registerTime)
        {
            Id = id;
            Name = name;
            Account = account;
            Email = email;
            Password = password;
            RegisterTime = registerTime;
        }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; } = string.Empty;
        
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        public static UserData CreateSampleUser()
        {
            return new UserData("张三", "zhangsan", "zhangsan@example.com", "123456");
        }

        // 重写Equals方法，基于Id判断相等性
        public override bool Equals(object? obj)
        {
            if (obj is UserData other)
            {
                return Id == other.Id;
            }
            return false;
        }

        // 重写GetHashCode方法
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        // 重写ToString方法，便于调试和显示
        public override string ToString()
        {
            return $"UserData(Id={Id}, Name={Name}, Account={Account}, Email={Email}, RegisterTime={RegisterTime:yyyy-MM-dd HH:mm:ss})";
        }
    }
}
