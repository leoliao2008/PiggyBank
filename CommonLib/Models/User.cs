namespace CommonLib.Models
{
    public class User
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Id { get; set; }
        public string? Token { get; set; }
        public string? AvatarUrl { get; set; }
        public int Gender { get; set; } = 1;
    }
}
