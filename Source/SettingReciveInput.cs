using UnityEngine;

public class SettingReciveInput : MonoBehaviour
{
    public int id;

    private void OnClick()
    {
        GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().startListening(id);
        transform.Find("Label").gameObject.GetComponent<UILabel>().text = "*wait for input";
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}