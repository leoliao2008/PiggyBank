using Contract.Dtos;
using Contracts.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos
{
    public class UserUpdateRequestDto: BaseUserRequestDto
    {
        public int Id { get; set; }

    }
}
