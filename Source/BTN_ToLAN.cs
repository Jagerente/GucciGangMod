using UnityEngine;

public class BTN_ToLAN : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);
    }
}