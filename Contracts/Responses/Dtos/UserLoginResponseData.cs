﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Responses.Dtos
{
    public class UserLoginResponseData : BaseUserInfoResponseData
    {
        public string? Token { get; set; }
    }
}
