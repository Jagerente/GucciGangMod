//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class PanelCredits : MonoBehaviour
{
    public GameObject label_back;
    public GameObject label_title;
    private int lang = -1;

    private void showTxt()
    {
        if (lang != Language.type)
        {
            lang = Language.type;
            label_title.GetComponent<UILabel>().text = Language.btn_credits[Language.type];
            label_back.GetComponent<UILabel>().text = Language.btn_back[Language.type];
        }
    }

    private void Update()
    {
        showTxt();
    }
}

