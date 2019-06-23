using UnityEngine;

public class BTN_toSingleSet : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelSingleSet, true);
    }

    private void Start()
    {
    }
}

