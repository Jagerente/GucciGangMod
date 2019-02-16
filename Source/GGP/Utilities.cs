using System;
using System.Text.RegularExpressions;

namespace GGP
{
    class Utilities
    {
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
