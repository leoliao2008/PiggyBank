using Contracts.Dtos;
using Contracts.Request;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Contract.Dtos
{
    public class UserRegisterRequestDto: BaseUserRequestDto
    {
        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
