using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Utils
{
    public static class TextUtils
    {
        public static bool IsEmpty(string? input) 
        { 
            return input == null || input.Length == 0;
        }
    }
}
