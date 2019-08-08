using GGM.Caching;
using UnityEngine;
using static FengGameManagerMKII;

namespace GGM.GUI.Pages
{
    public class Pause : MonoBehaviour
    {
        private void OnGUI()
        {
            float num7, num8;
            if (Time.timeScale <= 0.1f)
            {
                num7 = Screen.width / 2f;
                num8 = Screen.height / 2f;
                UnityEngine.GUI.backgroundColor = new Color(0.08f, 0.3f, 0.4f, 1f);
                UnityEngine.GUI.DrawTexture(new Rect(num7 - 98f, num8 - 48f, 196f, 96f), ColorCache.Blue);
                UnityEngine.GUI.Box(new Rect(num7 - 100f, num8 - 50f, 200f, 100f), string.Empty);
                if (FGM.pauseWaitTime <= 3f)
                {
                    UnityEngine.GUI.Label(new Rect(num7 - 43f, num8 - 15f, 200f, 22f), "Unpausing in:");
                    UnityEngine.GUI.Label(new Rect(num7 - 8f, num8 + 5f, 200f, 22f), FGM.pauseWaitTime.ToString("F1"));
                }
                else
                {
                    UnityEngine.GUI.Label(new Rect(num7 - 43f, num8 - 10f, 200f, 22f), "Game Paused.");
                }
            }
            else if (!logicLoaded || !customLevelLoaded)
            {
                num7 = Screen.width / 2f;
                num8 = Screen.height / 2f;
                UnityEngine.GUI.backgroundColor = new Color(0.08f, 0.3f, 0.4f, 1f);
                UnityEngine.GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), ColorCache.Black);
                UnityEngine.GUI.DrawTexture(new Rect(num7 - 98f, num8 - 48f, 196f, 146f), ColorCache.Blue);
                UnityEngine.GUI.Box(new Rect(num7 - 100f, num8 - 50f, 200f, 150f), string.Empty);
                var length = RCextensions
                    .returnStringFromObject(
                        PhotonNetwork.player.customProperties[PhotonPlayerProperty.currentLevel]).Length;
                var num50 = RCextensions
                    .returnStringFromObject(
                        PhotonNetwork.masterClient.customProperties[PhotonPlayerProperty.currentLevel]).Length;
                UnityEngine.GUI.Label(new Rect(num7 - 60f, num8 - 30f, 200f, 22f),
                    "Loading Level (" + length + "/" + num50 + ")");
                FGM.retryTime += Time.deltaTime;
                Screen.lockCursor = false;
                Screen.showCursor = true;
                if (UnityEngine.GUI.Button(new Rect(num7 - 20f, num8 + 50f, 40f, 30f), "Quit"))
                {
                    PhotonNetwork.Disconnect();
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                    GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameStart =
                        false;
                    GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                    FGM.DestroyAllExistingCloths();
                    Destroy(GameObjectCache.Find("MultiplayerManager"));
                    Application.LoadLevel("menu");
                }
            }
        }
    }
}