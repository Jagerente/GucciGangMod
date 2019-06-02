using UnityEngine;

public class BTN_TO_REGISTER : MonoBehaviour
{
    public GameObject registerPanel;

    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(registerPanel, true);
    }
}