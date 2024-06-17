using Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responses
{
    public class UserRegisterResponse:BaseResponse
    {
        public UserResponseDto? Content { get; set; }
    }
}
