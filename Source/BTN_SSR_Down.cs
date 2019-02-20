//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_SSR_Down : MonoBehaviour
{
    public GameObject panel;

    private void OnClick()
    {
        panel.GetComponent<SnapShotReview>().ShowNextIMG();
    }
}

