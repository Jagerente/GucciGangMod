﻿using UnityEngine;

public class BTN_Create_Game : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiSet, true);
    }
}

