using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevUtility.Helpers
{
    public static class CodeHelper
    {
        public static bool IsEmpty(this ICollection data)
        {
            if (data != null)
            {
                return data.Count == 0;
            }

            return true;
        }

        public static bool IsNotEmpty(this ICollection data)
        {
            return !data.IsEmpty();
        }
    }
}
