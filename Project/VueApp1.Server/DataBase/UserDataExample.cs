using Server.Data.DataBase;
using VueApp1.Server;

namespace VueApp1.Server.Examples
{
    /// <summary>
    /// UserData CRUD操作使用示例
    /// </summary>
    public static class UserDataExample
    {
        /// <summary>
        /// 运行UserData CRUD操作示例
        /// </summary>
        public static void RunExample()
        {
            Console.WriteLine("=== UserData CRUD 操作示例 ===\n");

            // 1. 初始化数据表
            Console.WriteLine("1. 初始化UserData表...");
            DataBaseOperator.InitializeUserDataTable();
            Console.WriteLine("UserData表初始化完成\n");

            // 2. 创建用户 (Create)
            Console.WriteLine("2. 创建新用户...");
            var newUser1 = new UserData("张三", "zhangsan", "zhangsan@example.com");
            var newUser2 = new UserData("李四", "lisi", "lisi@example.com");
            var newUser3 = new UserData("王五", "wangwu", "wangwu@example.com");

            int userId1 = DataBaseOperator.CreateUser(newUser1);
            int userId2 = DataBaseOperator.CreateUser(newUser2);
            int userId3 = DataBaseOperator.CreateUser(newUser3);

            Console.WriteLine($"创建的用户ID: {userId1}, {userId2}, {userId3}\n");

            // 3. 读取用户 (Read)
            Console.WriteLine("3. 读取用户信息...");
            
            // 按ID查询
            DataBaseOperator.PrintUserById(userId1);
            
            // 按账号查询
            var userByAccount = DataBaseOperator.GetUserByAccount("lisi");
            if (userByAccount != null)
            {
                Console.WriteLine($"按账号查询到用户: {userByAccount}");
            }

            // 按邮箱查询
            var userByEmail = DataBaseOperator.GetUserByEmail("wangwu@example.com");
            if (userByEmail != null)
            {
                Console.WriteLine($"按邮箱查询到用户: {userByEmail}");
            }

            Console.WriteLine();

            // 4. 显示所有用户
            Console.WriteLine("4. 显示所有用户...");
            DataBaseOperator.PrintAllUsers();
            DataBaseOperator.PrintUserCount();
            Console.WriteLine();

            // 5. 更新用户 (Update)
            Console.WriteLine("5. 更新用户信息...");
            if (userByAccount != null)
            {
                userByAccount.Name = "李四（已更新）";
                userByAccount.Email = "lisi_updated@example.com";
                bool updateResult = DataBaseOperator.UpdateUser(userByAccount);
                Console.WriteLine($"更新结果: {updateResult}");
            }
            Console.WriteLine();

            // 6. 搜索用户
            Console.WriteLine("6. 搜索用户...");
            var searchResults = DataBaseOperator.SearchUsersByName("李");
            Console.WriteLine($"搜索'李'的结果 (共{searchResults.Length}个):");
            foreach (var user in searchResults)
            {
                Console.WriteLine($"  - {user}");
            }
            Console.WriteLine();

            // 7. 检查账号/邮箱是否存在
            Console.WriteLine("7. 检查账号/邮箱存在性...");
            Console.WriteLine($"账号'zhangsan'是否存在: {DataBaseOperator.IsAccountExists("zhangsan")}");
            Console.WriteLine($"账号'nonexistent'是否存在: {DataBaseOperator.IsAccountExists("nonexistent")}");
            Console.WriteLine($"邮箱'zhangsan@example.com'是否存在: {DataBaseOperator.IsEmailExists("zhangsan@example.com")}");
            Console.WriteLine();

            // 8. 删除用户 (Delete)
            Console.WriteLine("8. 删除用户...");
            bool deleteResult = DataBaseOperator.DeleteUser(userId3);
            Console.WriteLine($"删除用户结果: {deleteResult}");
            
            Console.WriteLine("删除后的用户列表:");
            DataBaseOperator.PrintAllUsers();
            Console.WriteLine();

            // 9. 最终统计
            Console.WriteLine("9. 最终统计...");
            DataBaseOperator.PrintUserCount();

            Console.WriteLine("\n=== 示例结束 ===");
        }

        /// <summary>
        /// 测试错误情况
        /// </summary>
        public static void TestErrorCases()
        {
            Console.WriteLine("\n=== 测试错误情况 ===");

            // 测试重复账号
            Console.WriteLine("1. 测试创建重复账号...");
            var duplicateUser = new UserData("重复用户", "zhangsan", "duplicate@example.com");
            int result = DataBaseOperator.CreateUser(duplicateUser);
            Console.WriteLine($"创建重复账号结果: {result} (应该为-1)");

            // 测试重复邮箱
            Console.WriteLine("2. 测试创建重复邮箱...");
            var duplicateEmailUser = new UserData("重复邮箱", "duplicate", "zhangsan@example.com");
            result = DataBaseOperator.CreateUser(duplicateEmailUser);
            Console.WriteLine($"创建重复邮箱结果: {result} (应该为-1)");

            // 测试删除不存在的用户
            Console.WriteLine("3. 测试删除不存在的用户...");
            bool deleteResult = DataBaseOperator.DeleteUser(99999);
            Console.WriteLine($"删除不存在用户结果: {deleteResult} (应该为false)");

            // 测试更新不存在的用户
            Console.WriteLine("4. 测试更新不存在的用户...");
            var nonExistentUser = new UserData(99999, "不存在", "nonexistent", "nonexistent@example.com", DateTime.Now);
            bool updateResult = DataBaseOperator.UpdateUser(nonExistentUser);
            Console.WriteLine($"更新不存在用户结果: {updateResult} (应该为false)");

            Console.WriteLine("=== 错误测试结束 ===\n");
        }
    }
}
