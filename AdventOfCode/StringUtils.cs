using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class StringUtils
    {
        public static int AsInt(this string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return 0;
        }
    }
}