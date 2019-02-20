//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_Connect_To_Server_On_List : MonoBehaviour
{
    public int index;
    public string roomName;

    private void OnClick()
    {
        transform.parent.parent.GetComponent<PanelMultiJoin>().connectToIndex(index, roomName);
    }
}

