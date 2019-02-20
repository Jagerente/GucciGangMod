//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_LEADERBOARD_QUIT : MonoBehaviour
{
    public GameObject leaderboard;
    public GameObject mainMenu;

    private void OnClick()
    {
        NGUITools.SetActive(mainMenu, true);
        NGUITools.SetActive(leaderboard, false);
    }
}

