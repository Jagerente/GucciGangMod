using UnityEngine;

public class OnClickLoadSomething : MonoBehaviour
{
    public string ResourceToLoad;
    public ResourceTypeOption ResourceTypeToLoad;

    public void OnClick()
    {
        switch (ResourceTypeToLoad)
        {
            case ResourceTypeOption.Scene:
                Application.LoadLevel(ResourceToLoad);
                break;

            case ResourceTypeOption.Web:
                Application.OpenURL(ResourceToLoad);
                break;
        }
    }

    public enum ResourceTypeOption : byte
    {
        Scene = 0,
        Web = 1
    }
}