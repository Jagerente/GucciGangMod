using UnityEngine;

namespace GGM.GUI.Pages
{
    internal class PagesManager : MonoBehaviour
    {
        private void Awake()
        {
            var pauseMenu = new GameObject("PauseMenu").AddComponent<PauseMenu>();
            pauseMenu.gameObject.SetActive(false);
            DontDestroyOnLoad(pauseMenu);

            var mainMenu = new GameObject("MainMenu").AddComponent<MainMenu>();
            mainMenu.gameObject.SetActive(false);
            DontDestroyOnLoad(mainMenu);
        }
    }
}