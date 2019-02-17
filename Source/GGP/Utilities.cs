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
                    player = gameObject;
                }
                return player;
        }
    }
}
