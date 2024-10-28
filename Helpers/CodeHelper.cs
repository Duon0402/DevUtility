using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevUtility.Helpers
{
    public static class CodeHelper
    {
        public static bool IsEmpty<T>(this T value)
        {
            return value == null ;
        }
    }
}
