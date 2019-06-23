using UnityEngine;

public class MapNameChange : MonoBehaviour
{
    private void OnSelectionChange()
    {
        var info = LevelInfo.getInfo(GetComponent<UIPopupList>().selection);
        if (info != null)
        {
            GGM.Caching.GameObjectCache.Find("LabelLevelInfo").GetComponent<UILabel>().text = info.desc;
        }
        if (!GetComponent<UIPopupList>().items.Contains("Custom"))
        {
            GetComponent<UIPopupList>().items.Add("Custom");
            var component = GetComponent<UIPopupList>();
            component.textScale *= 0.8f;
        }
        if (!GetComponent<UIPopupList>().items.Contains("Custom (No PT)"))
        {
            GetComponent<UIPopupList>().items.Add("Custom (No PT)");
        }
    }
}

