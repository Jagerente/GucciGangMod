using UnityEngine;

public class BTN_Server_List_PgDn : MonoBehaviour
{
    private void OnClick()
    {
        GGM.Caching.GameObjectCache.Find("PanelMultiROOM").GetComponent<PanelMultiJoin>().pageDown();
    }
}