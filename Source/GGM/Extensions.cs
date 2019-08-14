using GGM.Caching;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GGM
{
    public static class Extensions
    {
        public static string[] AllProps;

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
            var list = new[] { 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.ToList();
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

        public static void SendToGGMUser(this PhotonView pv, string RPCName, params object[] data)
        {
            var targets = PhotonPlayer.GetGGMUsers();
            if (targets.Length == 0)
            {
                return;
            }
            foreach (var player in targets)
            {
                pv.RPC(RPCName, player, data);
            }
        }

        public static string CheckMod(this PhotonPlayer player)
        {
            var mod = string.Empty;
            var key = string.Empty;
            var rank = string.Empty;
            string[] rankarray = {
            "bronze",
            "silver",
            "gold",
            "platin",
            "diamond",
            "master",
            "grandmaster",
            "top5",
            "legendary"
        };
            switch (key)
            {
                case "Arch":
                    return mod = "Arch Mod";

                case "ZM":
                    return mod = "ZM Mod";

                case "KM":
                    return mod = "Kirito's Mod";

                case "CearPriv":
                    return mod = "Cear's Mod";

                case "GHOST":
                    return mod = "Ghost's Mod";

                case "CyanModNew":
                case "CyanMod":
                    return mod = "Cyan Mod";

                case "NRC":
                    return mod = "NRC Mod";

                case "RPR":
                    return mod = "RP Mod";

                case "USaitama":
                    return mod = "Saitama Mod";

                case "SRC":
                    return mod = "SRC Mod";

                case "Rage":
                case "RAGE":
                    return mod = "Tactical Rage's Mod / Valkyre Mod";

                case "EXE":
                    return mod = "EXE Mod";

                case "SoSteam":
                    return mod = "Ori's Mod";

                case "Nathan":
                    return mod = "Aurora & Nathan Mod";

                case "Angry_Guest":
                    return mod = "Angry_Guest Mod";

                case "not null":
                    return mod = "EC Mod";

                case "kies":
                case "Red":
                case "Death":
                    return mod = "Death Mod / Red Skies Mod";

                case "raohsopmod":
                    return mod = "Raoh Mod";

                case "Robbie'sMod":
                    return mod = "Robbie's Mod";

                case "KageNoKishi":
                    return mod = "Kage no kishi Mod";

                case "Universe":
                case "coins":
                case "UPublica":
                case "UPublica2":
                case "[a100ff]|[ac00ff]U[b800ff]n[c300ff]e[cf00ff]~[da00ff]]|fefcff| ":
                case "string.Empty":
                    return mod = "Universe Mod";

                case "INS":
                case "INSANE":
                    return mod = "Insane Mod";

                case "BRM":
                    return mod = "BRM Mod";

                case "AlphaX":
                    return mod = "AlphaX Mod";

                case "BSM":
                    return mod = "Blossom Mod";

                case "pedoModUser":
                    return mod = "Pedo Mod";
            }
            if (player.customProperties.ContainsKey(rankarray[0]) || player.customProperties.ContainsKey(rankarray[1]) || player.customProperties.ContainsKey(rankarray[2]) || player.customProperties.ContainsKey(rankarray[3]) || player.customProperties.ContainsKey(rankarray[4]) || player.customProperties.ContainsKey(rankarray[5]) || player.customProperties.ContainsKey(rankarray[6]) || player.customProperties.ContainsKey(rankarray[7]))
            {
                if (player.customProperties.ContainsKey(rankarray[0]))
                {
                    rank = "Bronze";
                }
                if (player.customProperties.ContainsKey(rankarray[1]))
                {
                    rank = "Silver";
                }
                if (player.customProperties.ContainsKey(rankarray[2]))
                {
                    rank = "Gold";
                }
                if (player.customProperties.ContainsKey(rankarray[3]))
                {
                    rank = "Platinum";
                }
                if (player.customProperties.ContainsKey(rankarray[4]))
                {
                    rank = "Diamond";
                }
                if (player.customProperties.ContainsKey(rankarray[5]))
                {
                    rank = "Master";
                }
                if (player.customProperties.ContainsKey(rankarray[6]))
                {
                    rank = "Grandmaster";
                }
                if (player.customProperties.ContainsKey(rankarray[7]))
                {
                    rank = "Top 5";
                }
                if (player.customProperties.ContainsKey(rankarray[8]))
                {
                    rank = "Legendary";
                }
                mod = rank;
                return "RRC Mod \nRank: " + rank;
            }

            if (player.customProperties.ContainsKey(key))
            {
                return mod;
            }
            if (player.isLocal)
            {
                return "GucciGang";
            }
            if (player.CelestialDeath)
            {
                return "CelestialDeath";
            }
            if (player.CyanMod)
            {
                return "CyanMod v0.3.0.2";
            }
            if (player.DI)
            {
                return "DI";
            }
            if (player.DeadInside)
            {
                return "DeadInside";
            }
            if (player.DeadInsideVer)
            {
                return "DeadInsideVer";
            }
            if (player.DeathMod)
            {
                return "DeathMod";
            }
            if (player.GucciLab)
            {
                return "GucciGangMod v1";
            }
            if (player.GucciGangMod)
            {
                return "GucciGangMod";
            }
            if (player.RC83)
            {
                return "RC83";
            }
            if (player.RS)
            {
                return "Red Skies";
            }
            if (player.SukaMod)
            {
                return "SukaRC";
            }
            if (player.SukaModOld)
            {
                return "OldSukaRC";
            }
            if (player.Universe)
            {
                return "Universe";
            }
            if (player.VENICE)
            {
                return "Venice's Mod";
            }
            if (player.customProperties.ContainsKey("RCteam") && mod == string.Empty)
            {
                return "RC Mod";
            }
            return "Unknown";
        }

        public static string CheckProps(this PhotonPlayer player)
        {
            var result = string.Empty;
            foreach (string str in player.customProperties.Keys)
            {
                if (!AllProps.Contains(str))
                {
                    if ((string)str == string.Empty)
                    {
                        result += "string.Empty ";
                    }
                    else
                    {
                        result += str + " ";
                    }
                }
            }
            return result == string.Empty ? "No unusual properties" : result;
        }
    }
}