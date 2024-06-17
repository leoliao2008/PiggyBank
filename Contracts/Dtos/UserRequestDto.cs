using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Contract.Dtos
{
    public class UserRequestDto
    {
        [Required]  
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public int Gender { get; set; } = 1;
        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
