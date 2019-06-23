using UnityEngine;

public class BTN_Enter_PWD : MonoBehaviour
{
    private void OnClick()
    {
        var text = GGM.Caching.GameObjectCache.Find("InputEnterPWD").GetComponent<UIInput>().label.text;
        var eaes = new SimpleAES();
        if (text == eaes.Decrypt(PanelMultiJoinPWD.Password))
        {
            PhotonNetwork.JoinRoom(PanelMultiJoinPWD.roomName);
        }
        else
        {
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().PanelMultiPWD, false);
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiROOM, true);
            GGM.Caching.GameObjectCache.Find("PanelMultiROOM").GetComponent<PanelMultiJoin>().refresh();
        }
    }
}

