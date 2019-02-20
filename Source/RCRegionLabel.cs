//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class RCRegionLabel : MonoBehaviour
{
    public GameObject myLabel;

    private void Update()
    {
        if ((myLabel != null) && myLabel.GetComponent<UILabel>().isVisible)
        {
            myLabel.transform.LookAt(2f * myLabel.transform.position - Camera.main.transform.position);
        }
    }
}

