namespace VueApp1.Server
{
    public class UserData
    {
        public UserData(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public static UserData CreateSampleUser()
        {
            return new UserData(1, "John Doe", "john.doe@example.com");
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
    }
}
