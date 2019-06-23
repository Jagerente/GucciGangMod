using UnityEngine;

public class BTN_SetDefault : MonoBehaviour
{
    private void OnClick()
    {
        GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().setToDefault();
        GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().showKeyMap();
    }
}

