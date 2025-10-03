namespace Backend.Models.UserData
{
    public class UserDetailDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}