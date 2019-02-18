using System.Text.RegularExpressions;
using UnityEngine;

namespace GGP
{
    class Utilities
    {
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static GameObject Player()
        {
            var player = new GameObject();
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER)
                    player = gameObject;
                else if (gameObject.GetPhotonView().isMine)
                    player = gameObject;
            }
            return player;
        }
    }
}
