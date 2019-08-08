using UnityEngine;

public class BTN_leave_lobby : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);
        PhotonNetwork.Disconnect();
    }
}