using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
