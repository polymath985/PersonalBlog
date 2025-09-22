using Backend.Controllers;
using Backend.Models;
using PersonalBlog.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Backend.Managers
{
    public class UserDataManager
    {
        private readonly BlogDbContext _context;

        public UserDataManager(BlogDbContext context)
        {
            _context = context;
        }

        ///<summary>
        ///与数据库互动，获取新用户ID
        ///<summary>
        public async Task<UserData?> AddUserDataAsync(UserData userData)
        {
            try
            {
                // 检查邮箱是否已存在
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userData.Email);
                if (existingUser != null)
                {
                    throw new Exception("邮箱地址已被使用");
                }

                _context.Users.Add(userData);
                await _context.SaveChangesAsync();
                return userData;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ValidateUserAsync(SignInRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            return user != null && user.Password == request.Password;
        }

        public async Task RemoveUserDataAsync(UserData userData)
        {
            _context.Users.Remove(userData);
            await _context.SaveChangesAsync();
        }

        public async Task<UserData?> GetUserDataByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库查询失败: {ex.Message}");
                return null;
            }
        }

        public async Task<UserData?> GetUserDataByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库查询失败: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<UserData>?> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库查询失败: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateUserDataAsync(UserData userData)
        {
            var existingUser = await GetUserDataByIdAsync(userData.Id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(userData);
                await _context.SaveChangesAsync();
            }
        }

    }
}
