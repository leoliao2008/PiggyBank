using Contract.Dtos;
using Contracts.Responses.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responses
{
    public class UserRegisterResponse:BaseResponse
    {
       public BaseUserInfoResponseData? Data { get; set; }
    }
}
