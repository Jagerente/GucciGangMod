using UnityEngine;

public class BTN_Server_List_PgUp : MonoBehaviour
{
    private void OnClick()
    {
        GGM.Caching.GameObjectCache.Find("PanelMultiROOM").GetComponent<PanelMultiJoin>().pageUp();
    }
}