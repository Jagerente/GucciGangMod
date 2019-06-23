using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GGM
{
    public static class Extensions
    {
        /// <summary>
        /// Converts Color variable to HEX-format string.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHEX(this Color color)
        {
            return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        }

        /// <summary>
        /// Converts HEX string to Color.
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color ToColor(this string hex, byte a = 255)
        {
            if (hex.Length != 6) return Caching.Colors.white;
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            return new Color32(r, g, b, a);
        }

        /// <summary>
        /// Removes HTML tags from string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string stripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Removes HEX tag colors from string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string stripHEX(this string text)
        {
            List<char> list = new char[]
            {
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                '0',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9'
            }.ToList<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '[')
                {
                    int num = i;
                    int num2 = 1;
                    bool flag = false;
                    while (num + num2 < text.Length)
                    {
                        num2++;
                        if (num2 > 8)
                        {
                            break;
                        }
                        int num3 = num + num2 - 1;
                        if (text[num3] == ']')
                        {
                            if (num2 >= 3 && (num2 != 3 || text[num3 - 1] == '-'))
                            {
                                flag = true;
                                break;
                            }
                            break;
                        }
                        else if (!list.Contains(char.ToUpper(text[num3])) && (num3 + 1 >= text.Length || text[num3 + 1] != ']'))
                        {
                            break;
                        }
                    }
                    if (flag)
                    {
                        text = text.Remove(num, num2);
                        i = 0;
                    }
                }
            }
            return string.Concat(text);
        }

    }
}
