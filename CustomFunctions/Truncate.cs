using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkillsTest.CustomFunctions
{
    public static class Truncate
    {
        public static string TruncateLongString(this string inputString, int maxChars)
        {
            if (inputString == null || inputString.Length < maxChars)
            {
                return inputString;
            }
            var truncatedString = inputString.Substring(0, maxChars) + "...";
            return truncatedString;
        }
    }
}