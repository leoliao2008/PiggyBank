namespace Contracts.Responses.Dtos
{
    public class BaseUserInfoResponseData
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public int Gender { get; set; } = 1;
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public int PairedGroupId { get; set; }
        public string? Role { get; set; }
    }
}
