﻿using UnityEngine;

public class BTN_QUICKMATCH : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiSet, true);
        PhotonNetwork.offlineMode = true;
    }

    private void Start()
    {
    }
}

