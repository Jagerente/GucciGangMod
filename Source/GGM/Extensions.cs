using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace GGM
{
    public static class Extensions
    {
        #region Variables
        private static string[] _locations = { "akina", "cave", "city", "colossal", "forest", "house", "outside", "tutorial"};
        public static bool Forest = false;
        public static bool City = false;
        #endregion

        public static string stripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
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

        public static GameObject Player()
        {
            var player = new GameObject();
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || PhotonNetwork.offlineMode)
                    player = gameObject;
                else if (gameObject.GetPhotonView().isMine)
                    player = gameObject;
            }
            return player;
        }

        public static bool Protection(PhotonMessageInfo info, string reason)
        {
            if (!info.sender.isMasterClient)
            {
                if (PhotonNetwork.player.isMasterClient)
                    FengGameManagerMKII.instance.kickPlayerRC(info.sender, true, reason);
                return true;
            }
            return false;
        }

        public static void DisableObject(string str)
        {
            if (GameObject.Find(str))
            {
                GameObject.Find(str).SetActive(false);
            }
        }

        public static bool OnMap()
        {
            var boolean = false;
            foreach (var location in _locations)
                if (Application.loadedLevelName.ToLower().Contains(location))
                    boolean = true;
            return boolean;
        }

        public static bool ASODamage()
        {
            return (int)FengGameManagerMKII.settings[210] == 1 &&
                   (string)FengGameManagerMKII.settings[211] == "100" &&
                   (string)FengGameManagerMKII.settings[212] == "0" &&
                   (string)FengGameManagerMKII.settings[213] == "0" &&
                   (string)FengGameManagerMKII.settings[214] == "0" &&
                   (string)FengGameManagerMKII.settings[215] == "0" &&
                   (int)FengGameManagerMKII.settings[207] == 1 &&
                   (string)FengGameManagerMKII.settings[208] == "2.5" &&
                   (string)FengGameManagerMKII.settings[209] == "3" &&
                   (int)FengGameManagerMKII.settings[205] == 1 &&
                   (string)FengGameManagerMKII.settings[206] == "1000";
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

        public static string GetMapName()
        {
            return Application.loadedLevelName;
        }

        public static string GetRoomName()
        {
            return PhotonNetwork.room.name.Split(new char[] { '`' })[0].Trim().stripHEX();
        }
    }
}
