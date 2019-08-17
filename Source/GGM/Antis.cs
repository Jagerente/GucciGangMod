using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGM
{
    public static class Antis
    {
        private static List<int> antiReviveList = new List<int>();

        public static void CheckAntiRevive(HERO hero, int ID)
        {
            if (!PhotonNetwork.isMasterClient || !Config.Settings.AntiRevive)
            {
                return;
            }
            if (antiReviveList.Contains(ID))
            {
                hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "[FF0000]Anti-Revive" });
            }
        }

        public static void CheckAntiReviveAdd(int ID)
        {
            if (!PhotonNetwork.isMasterClient ||!Config.Settings.AntiRevive)
            {
                return;
            }
            if(IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                return;
            }
            if (!antiReviveList.Contains(ID))
            {
                antiReviveList.Add(ID);
            }
        }

        public static void ClearAntiRevive()
        {
            antiReviveList.Clear();
        }

        public static void OnRestart()
        {
            ClearAntiRevive();
        }


        public static void RemoveFromAntiRevive(int ID)
        {
            if (antiReviveList.Contains(ID))
            {
                antiReviveList.Remove(ID);
            }
        }
    }
}
