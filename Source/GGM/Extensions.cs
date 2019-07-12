using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using GGM.Caching;
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
            if (hex.Length != 6) return Caching.ColorCache.White;
            var r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            var g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            var b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            return new Color32(r, g, b, a);
        }

        /// <summary>
        /// Removes HTML tags from string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Removes HEX tag colors from string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StripHEX(this string text)
        {
            var list = new[]
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
            }.ToList();
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] == '[')
                {
                    var num = i;
                    var num2 = 1;
                    var flag = false;
                    while (num + num2 < text.Length)
                    {
                        num2++;
                        if (num2 > 8)
                        {
                            break;
                        }
                        var num3 = num + num2 - 1;
                        if (text[num3] == ']')
                        {
                            if (num2 >= 3 && (num2 != 3 || text[num3 - 1] == '-'))
                            {
                                flag = true;
                            }
                            break;
                        }

                        if (!list.Contains(char.ToUpper(text[num3])) && (num3 + 1 >= text.Length || text[num3 + 1] != ']'))
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

        public static int CountWords(this string s, string s1)
        {
            return (s.Length - s.Replace(s1, "").Length) / s1.Length;
        }

        public static string ToHTML(this string str)
        {
            if (Regex.IsMatch(str, @"\[([0-9a-zA-Z]{6})\]"))
            {
                str = str.Contains("[-]") ? Regex.Replace(str, @"\[([0-9a-fA-F]{6})\]", "<color=#$1>").Replace("[-]", "</color>") : Regex.Replace(str, @"\[([0-9a-fA-F]{6})\]", "<color=#$1>");
                var c = (short)(str.CountWords("<color=") - str.CountWords("</color>"));
                for (short i = 0; i < c; i++)
                {
                    str += "</color>";
                }
            }
            return str;
        }

        public static string GetDayLight()
        {
            var dayLight = "Day";
            switch (IN_GAME_MAIN_CAMERA.dayLight)
            {
                case DayLight.Day:
                    return dayLight = "Day";
                case DayLight.Dawn:
                    return dayLight = "Dawn";
                case DayLight.Night:
                    return dayLight = "Night";
            }
            return dayLight;
        }

        public static string GetDifficulty()
        {
            var difficulty = "Training";
            switch (IN_GAME_MAIN_CAMERA.difficulty)
            {
                case 0:
                    return difficulty = "Normal";
                case 1:
                    return difficulty = "Hard";
                case 2:
                    return difficulty = "Abnormal";
            }
            return difficulty;
        }

        public static string GetLobbyName()
        {
            return Regex.Replace(PhotonNetwork.ServerAddress, "app\\-|\\.exitgamescloud\\.com|\\:\\d+", "").ToUpper();
        }

        public static string GetRoomName()
        {
            return PhotonNetwork.room.name.Split(new char[] { '`' })[0].Trim().StripHEX();
        }

        public static void DisableObject(string str)
        {
            if (GameObjectCache.Find(str))
            {
                GameObjectCache.Find(str).SetActive(false);
            }
        }

        public static void EnableObject(string str)
        {
            if (GameObjectCache.Find(str))
            {
                GameObjectCache.Find(str).SetActive(true);
            }
        }
    }
}