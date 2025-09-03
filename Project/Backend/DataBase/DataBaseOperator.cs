using Backend.Models;
using System.Data.SQLite;

namespace Backend.DataBase
{
    public static class DataBaseOperator
    {
        #region UserData CRUD Operations

        /// <summary>
        /// 初始化UserData表（清空现有数据并重新创建）
        /// </summary>
        public static void InitializeUserDataTable()
        {
            try
            {
                // 删除现有表（如果存在）
                string dropTableSql = "DROP TABLE IF EXISTS UserData";
                SQLiteHelper.ExecSQL(dropTableSql);
                Console.WriteLine("已删除现有UserData表");

                // 重新创建包含Password字段的表
                string createTableSql = @"
                    CREATE TABLE UserData (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Account TEXT NOT NULL UNIQUE,
                        Email TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL DEFAULT '',
                        RegisterTime TEXT NOT NULL
                    )";

                SQLiteHelper.ExecSQL(createTableSql);
                Console.WriteLine("已创建新的UserData表（包含Password字段）");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"初始化UserData表时出错：{ex.Message}");
                throw; // 重新抛出异常，让调用者知道初始化失败
            }
        }

        /// <summary>
        /// 创建用户 - Create
        /// </summary>
        /// <param name="userData">用户数据对象</param>
        /// <returns>新创建的用户ID，失败返回-1</returns>
        public static int CreateUser(UserData userData)
        {
            try
            {
                // 检查账号和邮箱是否已存在
                if (IsAccountExists(userData.Account))
                {
                    throw new Exception($"账号 {userData.Account} 已存在");
                }
                if (IsEmailExists(userData.Email))
                {
                    throw new Exception($"邮箱 {userData.Email} 已存在");
                }

                string insertSql = @"
                    INSERT INTO UserData (Name, Account, Email, Password, RegisterTime) 
                    VALUES (@Name, @Account, @Email, @Password, @RegisterTime);
                    SELECT last_insert_rowid();";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Name", userData.Name),
                    new SQLiteParameter("@Account", userData.Account),
                    new SQLiteParameter("@Email", userData.Email),
                    new SQLiteParameter("@Password", userData.Password ?? string.Empty),
                    new SQLiteParameter("@RegisterTime", userData.RegisterTime.ToString("yyyy-MM-dd HH:mm:ss"))
                };

                object result = SQLiteHelper.ExecuteScalar(insertSql, parameters);
                if (result != null)
                {
                    int newId = Convert.ToInt32(result);
                    userData.Id = newId;
                    Console.WriteLine($"用户创建成功：{userData}");
                    return newId;
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建用户失败：{ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// 根据ID获取用户 - Read
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户数据对象，未找到返回null</returns>
        public static UserData? GetUserById(int userId)
        {
            string selectSql = "SELECT * FROM UserData WHERE Id = @Id";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Id", userId)
            };

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(selectSql, parameters))
            {
                if (reader.Read())
                {
                    return new UserData(
                        Convert.ToInt32(reader["Id"]),
                        reader["Name"].ToString() ?? string.Empty,
                        reader["Account"].ToString() ?? string.Empty,
                        reader["Email"].ToString() ?? string.Empty,
                        reader["Password"].ToString() ?? string.Empty,
                        DateTime.Parse(reader["RegisterTime"].ToString() ?? DateTime.Now.ToString())
                    );
                }
            }
            return null;
        }

        /// <summary>
        /// 根据账号获取用户
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns>用户数据对象，未找到返回null</returns>
        public static UserData? GetUserByAccount(string account)
        {
            string selectSql = "SELECT * FROM UserData WHERE Account = @Account";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Account", account)
            };

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(selectSql, parameters))
            {
                if (reader.Read())
                {
                    return new UserData(
                        Convert.ToInt32(reader["Id"]),
                        reader["Name"].ToString() ?? string.Empty,
                        reader["Account"].ToString() ?? string.Empty,
                        reader["Email"].ToString() ?? string.Empty,
                        reader["Password"].ToString() ?? string.Empty,
                        DateTime.Parse(reader["RegisterTime"].ToString() ?? DateTime.Now.ToString())
                    );
                }
            }
            return null;
        }

        /// <summary>
        /// 根据邮箱获取用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns>用户数据对象，未找到返回null</returns>
        public static UserData? GetUserByEmail(string email)
        {
            string selectSql = "SELECT * FROM UserData WHERE Email = @Email";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Email", email)
            };

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(selectSql, parameters))
            {
                if (reader.Read())
                {
                    return new UserData(
                        Convert.ToInt32(reader["Id"]),
                        reader["Name"].ToString() ?? string.Empty,
                        reader["Account"].ToString() ?? string.Empty,
                        reader["Email"].ToString() ?? string.Empty,
                        reader["Password"].ToString() ?? string.Empty,
                        DateTime.Parse(reader["RegisterTime"].ToString() ?? DateTime.Now.ToString())
                    );
                }
            }
            return null;
        }

        /// <summary>
        /// 根据邮箱获取用户密码
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns>用户密码，未找到返回null</returns>
        public static string? GetPasswordByEmail(string email)
        {
            string selectSql = "SELECT Password FROM UserData WHERE Email = @Email";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Email", email)
            };

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(selectSql, parameters))
            {
                if (reader.Read())
                {
                    return reader["Password"].ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// 验证用户邮箱和密码
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="password">用户密码</param>
        /// <returns>验证成功返回true，失败返回false</returns>
        public static bool ValidateUserCredentials(string email, string password)
        {
            try
            {
                string selectSql = "SELECT COUNT(*) FROM UserData WHERE Email = @Email AND Password = @Password";
                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Email", email),
                    new SQLiteParameter("@Password", password)
                };

                object result = SQLiteHelper.ExecuteScalar(selectSql, parameters);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"验证用户凭据失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取所有用户 - Read All
        /// </summary>
        /// <returns>所有用户数据数组</returns>
        public static UserData[] GetAllUsers()
        {
            string selectSql = "SELECT * FROM UserData ORDER BY RegisterTime DESC";
            List<UserData> users = new List<UserData>();

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(selectSql))
            {
                while (reader.Read())
                {
                    users.Add(new UserData(
                        Convert.ToInt32(reader["Id"]),
                        reader["Name"].ToString() ?? string.Empty,
                        reader["Account"].ToString() ?? string.Empty,
                        reader["Email"].ToString() ?? string.Empty,
                        reader["Password"].ToString() ?? string.Empty,
                        DateTime.Parse(reader["RegisterTime"].ToString() ?? DateTime.Now.ToString())
                    ));
                }
            }

            return users.ToArray();
        }

        /// <summary>
        /// 更新用户信息 - Update
        /// </summary>
        /// <param name="userData">要更新的用户数据</param>
        /// <returns>更新成功返回true，失败返回false</returns>
        public static bool UpdateUser(UserData userData)
        {
            try
            {
                // 检查用户是否存在
                if (GetUserById(userData.Id) == null)
                {
                    Console.WriteLine($"用户ID {userData.Id} 不存在");
                    return false;
                }

                // 检查账号和邮箱是否被其他用户使用
                var existingUserByAccount = GetUserByAccount(userData.Account);
                if (existingUserByAccount != null && existingUserByAccount.Id != userData.Id)
                {
                    Console.WriteLine($"账号 {userData.Account} 已被其他用户使用");
                    return false;
                }

                var existingUserByEmail = GetUserByEmail(userData.Email);
                if (existingUserByEmail != null && existingUserByEmail.Id != userData.Id)
                {
                    Console.WriteLine($"邮箱 {userData.Email} 已被其他用户使用");
                    return false;
                }

                string updateSql = @"
                    UPDATE UserData 
                    SET Name = @Name, Account = @Account, Email = @Email, Password = @Password 
                    WHERE Id = @Id";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Id", userData.Id),
                    new SQLiteParameter("@Name", userData.Name),
                    new SQLiteParameter("@Account", userData.Account),
                    new SQLiteParameter("@Email", userData.Email),
                    new SQLiteParameter("@Password", userData.Password ?? string.Empty)
                };

                int rowsAffected = SQLiteHelper.ExecuteNonQuery(updateSql, parameters);
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"用户更新成功：{userData}");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新用户失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 删除用户 - Delete
        /// </summary>
        /// <param name="userId">要删除的用户ID</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public static bool DeleteUser(int userId)
        {
            try
            {
                // 检查用户是否存在
                var user = GetUserById(userId);
                if (user == null)
                {
                    Console.WriteLine($"用户ID {userId} 不存在");
                    return false;
                }

                string deleteSql = "DELETE FROM UserData WHERE Id = @Id";
                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Id", userId)
                };

                int rowsAffected = SQLiteHelper.ExecuteNonQuery(deleteSql, parameters);
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"用户删除成功：{user.Name} (ID: {userId})");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除用户失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 删除所有用户
        /// </summary>
        /// <returns>删除的用户数量</returns>
        public static int DeleteAllUsers()
        {
            try
            {
                string deleteSql = "DELETE FROM UserData";
                int rowsAffected = SQLiteHelper.ExecuteNonQuery(deleteSql);
                Console.WriteLine($"已删除所有用户，共 {rowsAffected} 个用户");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除所有用户失败：{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 获取用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public static int GetUserCount()
        {
            string countSql = "SELECT COUNT(*) FROM UserData";
            object result = SQLiteHelper.ExecuteScalar(countSql);
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }

        /// <summary>
        /// 检查账号是否存在
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool IsAccountExists(string account)
        {
            string checkSql = "SELECT COUNT(*) FROM UserData WHERE Account = @Account";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Account", account)
            };

            object result = SQLiteHelper.ExecuteScalar(checkSql, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// 检查邮箱是否存在
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool IsEmailExists(string email)
        {
            string checkSql = "SELECT COUNT(*) FROM UserData WHERE Email = @Email";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Email", email)
            };

            object result = SQLiteHelper.ExecuteScalar(checkSql, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// 按姓名搜索用户
        /// </summary>
        /// <param name="name">姓名关键字</param>
        /// <returns>匹配的用户数组</returns>
        public static UserData[] SearchUsersByName(string name)
        {
            string searchSql = "SELECT * FROM UserData WHERE Name LIKE @Name ORDER BY RegisterTime DESC";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Name", $"%{name}%")
            };

            List<UserData> users = new List<UserData>();
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteReader(searchSql, parameters))
            {
                while (reader.Read())
                {
                    users.Add(new UserData(
                        Convert.ToInt32(reader["Id"]),
                        reader["Name"].ToString() ?? string.Empty,
                        reader["Account"].ToString() ?? string.Empty,
                        reader["Email"].ToString() ?? string.Empty,
                        DateTime.Parse(reader["RegisterTime"].ToString() ?? DateTime.Now.ToString())
                    ));
                }
            }

            return users.ToArray();
        }

        /// <summary>
        /// 打印用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        public static void PrintUserById(int userId)
        {
            var user = GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine($"未找到ID为 {userId} 的用户！");
                return;
            }

            Console.WriteLine("\n---------------------- 用户详情 ----------------------");
            Console.WriteLine($"用户ID：{user.Id}");
            Console.WriteLine($"姓名：{user.Name}");
            Console.WriteLine($"账号：{user.Account}");
            Console.WriteLine($"邮箱：{user.Email}");
            Console.WriteLine($"注册时间：{user.RegisterTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("----------------------------------------------------");
        }

        /// <summary>
        /// 打印所有用户信息
        /// </summary>
        public static void PrintAllUsers()
        {
            var users = GetAllUsers();
            if (users.Length == 0)
            {
                Console.WriteLine("\n数据库中没有用户信息！");
                return;
            }

            Console.WriteLine($"\n=================== 用户列表 (共{users.Length}个用户) ===================");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id,-3} | 姓名: {user.Name,-10} | 账号: {user.Account,-15} | 邮箱: {user.Email,-25} | 注册时间: {user.RegisterTime:yyyy-MM-dd HH:mm:ss}");
            }
            Console.WriteLine("================================================================");
        }

        /// <summary>
        /// 打印用户统计信息
        /// </summary>
        public static void PrintUserCount()
        {
            int count = GetUserCount();
            Console.WriteLine($"\n当前用户总数：{count}");
        }

        #endregion
    }
}