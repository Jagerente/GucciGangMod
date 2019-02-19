using System.Text.RegularExpressions;
using UnityEngine;

namespace GGM
{
    class Extensions
    {
        #region Location Names
        public static bool Forest = false;
        public static bool City = false;
        public static bool Tutorial = false;
        public static bool Outside = false;
        public static bool CaveFight = false;
        public static bool HouseFight = false;
        public static bool Akina = false;
        public static bool Colossal = false;
        #endregion

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static GameObject Player()
        {
            var player = new GameObject();
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
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
            else return false;
        }

        static void DisableObject(string str)
        {
            if (GameObject.Find(str))
            {
                GameObject.Find(str).SetActive(false);
            }
        }

        public static bool OnMap()
        {
            if (Forest || City || CaveFight || HouseFight || Colossal || Akina || Tutorial || Outside)
                return true;
            else
                return false;
        }

        public static bool ASODamage()
        {
            if (
                (int)FengGameManagerMKII.settings[210] == 1 &&
                (string)FengGameManagerMKII.settings[211] == "100" &&
                (string)FengGameManagerMKII.settings[212] == "0" &&
                (string)FengGameManagerMKII.settings[213] == "0" &&
                (string)FengGameManagerMKII.settings[214] == "0" &&
                (string)FengGameManagerMKII.settings[215] == "0" &&
                (int)FengGameManagerMKII.settings[207] == 1 &&
                (string)FengGameManagerMKII.settings[208] == "2.5" &&
                (string)FengGameManagerMKII.settings[209] == "3" &&
                (int)FengGameManagerMKII.settings[205] == 1 &&
                (string)FengGameManagerMKII.settings[206] == "1000")
                return true;
            else
                return false;
        }
    }
}
