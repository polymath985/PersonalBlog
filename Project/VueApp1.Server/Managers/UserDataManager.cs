namespace VueApp1.Server.Managers
{
    public class UserDataManager
    {
        private static readonly Lazy<UserDataManager> _instanceHolder = new Lazy<UserDataManager>(() => new UserDataManager());

        public static UserDataManager Instance => _instanceHolder.Value;

        private UserDataManager() { }

        private List<UserData> _userDataList = new();

        public void AddUserData(UserData userData)
        {
            if (!_userDataList.Contains(userData))
            {
                _userDataList.Add(userData);
            }
        }

        public void RemoveUserData(UserData userData)
        {
            _userDataList.Remove(userData);
        }

        public UserData GetUserDataById(int id)
        {
            return _userDataList.FirstOrDefault(u => u.Id == id) ?? new UserData(0, "Unknown", "unknown", "unknown@example.com", DateTime.Now);
        }

        public IEnumerable<UserData> GetAllUsers()
        {
            return _userDataList.ToList();
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
