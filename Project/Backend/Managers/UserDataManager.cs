using Backend.DataBase;
using Backend.Controllers;
using Backend.Models;

namespace Backend.Managers
{
    public class UserDataManager
    {
        private static readonly Lazy<UserDataManager> _instanceHolder = new Lazy<UserDataManager>(() => new UserDataManager());

        public static UserDataManager Instance => _instanceHolder.Value;

        private UserDataManager()
        {
            // 初始化数据库表
            DataBaseOperator.InitializeUserDataTable();
        }

        ///<summary>
        ///与数据库互动，获取新用户ID
        ///<summary>
        public UserData? AddUserData(UserData userData)
        {
            try
            {
                // 使用数据库操作创建用户
                int newId = DataBaseOperator.CreateUser(userData);
                if (newId > 0)
                {
                    userData.Id = newId;

                    return userData;
                }
                else
                {
                    throw new Exception("数据库创建用户失败");
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ValidateUser(SignInRequest request)
        {

            if (DataBaseOperator.ValidateUserCredentials(request.Email, request.Password))
            {
                return true;
            }
            return false;
        }

        public void RemoveUserData(UserData userData)
        {
            DataBaseOperator.DeleteUser(userData.Id);
        }

        public UserData? GetUserDataById(int id)
        {
            try
            {
                // 首先从数据库获取
                var userData = DataBaseOperator.GetUserById(id);
                if (userData != null)
                {
                    return userData;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库查询失败: {ex.Message}");
                return null;
            }
        }

        public UserData? GetUserDataByEmail(string email)
        {
            try
            {
                // 首先从数据库获取
                var userData = DataBaseOperator.GetUserByEmail(email);
                if (userData != null)
                {
                    return userData;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库查询失败: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<UserData>? GetAllUsers()
        {
            try
            {
                // 首先从数据库获取所有用户
                var usersFromDb = DataBaseOperator.GetAllUsers();
                return usersFromDb;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库查询失败: {ex.Message}");
                return null;
            }

        }

        public void UpdateUserData(UserData userData)
        {
            var existingUser = GetUserDataById(userData.Id);
            if (existingUser != null)
            {
                RemoveUserData(existingUser);
                AddUserData(userData);
            }
        }

    }
}
