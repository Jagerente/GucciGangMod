using UnityEngine;

public class UIadjustment : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(Screen.width / 960, Screen.height / 600, 0f);
    }

    private void Update()
    {
        transform.localScale = new Vector3(Screen.width / 960, Screen.height / 600, 0f);
    }
}