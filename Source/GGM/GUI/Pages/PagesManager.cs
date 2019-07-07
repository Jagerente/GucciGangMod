using UnityEngine;

namespace GGM.GUI.Pages
{
    internal class PagesManager : MonoBehaviour
    {
        private void Awake()
        {
            PauseMenu pauseMenu = new GameObject("PauseMenu").AddComponent<PauseMenu>();
            pauseMenu.gameObject.SetActive(false);
            DontDestroyOnLoad(pauseMenu);

            MainMenu mainMenu = new GameObject("MainMenu").AddComponent<MainMenu>();
            mainMenu.gameObject.SetActive(false);
            DontDestroyOnLoad(mainMenu);
        }
    }
}