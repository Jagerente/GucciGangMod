//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_LEADERBOARD : MonoBehaviour
{
    public GameObject leaderboard;
    public GameObject mainMenu;

    private void OnClick()
    {
        NGUITools.SetActive(mainMenu, false);
        NGUITools.SetActive(leaderboard, true);
    }
}

