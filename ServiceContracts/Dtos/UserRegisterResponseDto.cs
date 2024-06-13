using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dtos
{
    public class UserRegisterResponseDto
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
