using UnityEngine;

public class BTN_START_MULTI_SERVER : MonoBehaviour
{
    private void OnClick()
    {
        var text = GGM.Caching.GameObjectCache.Find("InputServerName").GetComponent<UIInput>().label.text;
        var maxPlayers = int.Parse(GGM.Caching.GameObjectCache.Find("InputMaxPlayer").GetComponent<UIInput>().label.text);
        var num2 = int.Parse(GGM.Caching.GameObjectCache.Find("InputMaxTime").GetComponent<UIInput>().label.text);
        var selection = GGM.Caching.GameObjectCache.Find("PopupListMap").GetComponent<UIPopupList>().selection;
        var str3 = !GGM.Caching.GameObjectCache.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked ? !GGM.Caching.GameObjectCache.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked ? "normal" : "abnormal" : "hard";
        var str4 = string.Empty;
        if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day)
        {
            str4 = "day";
        }
        if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn)
        {
            str4 = "dawn";
        }
        if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Night)
        {
            str4 = "night";
        }
        var unencrypted = GGM.Caching.GameObjectCache.Find("InputStartServerPWD").GetComponent<UIInput>().label.text;
        PhotonNetwork.CreateRoom(string.Concat(text, "`", selection, "`", str3, "`", num2, "`", str4, "`", unencrypted.Length > 0 ? new SimpleAES().Encrypt(unencrypted) : "", "`", Random.Range(0, 50000)), new RoomOptions { isOpen = true, isVisible = true, maxPlayers = maxPlayers }, null);
    }
}

