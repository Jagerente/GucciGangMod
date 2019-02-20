//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class PanelGroupManager
{
    public GameObject[] panelGroup;

    public void ActivePanel(int index)
    {
        foreach (var obj2 in panelGroup)
        {
            obj2.SetActive(false);
        }
        panelGroup[index].SetActive(true);
    }
}

