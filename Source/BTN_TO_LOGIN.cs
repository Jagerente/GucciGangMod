using UnityEngine;

public class BTN_TO_LOGIN : MonoBehaviour
{
    public GameObject loginPanel;

    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(loginPanel, true);
    }
}