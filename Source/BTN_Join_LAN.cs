using UnityEngine;

public class BTN_Join_LAN : MonoBehaviour
{
    private void OnClick()
    {
        int num;
        var text = transform.parent.Find("InputIP").GetComponent<UIInput>().text;
        var s = transform.parent.Find("InputPort").GetComponent<UIInput>().text;
        var str3 = transform.parent.Find("InputAuthPass").GetComponent<UIInput>().text;
        PhotonNetwork.Disconnect();
        if (int.TryParse(s, out num) && PhotonNetwork.ConnectToMaster(text, num, FengGameManagerMKII.applicationId, UIMainReferences.version))
        {
            PlayerPrefs.SetString("lastIP", text);
            PlayerPrefs.SetString("lastPort", s);
            PlayerPrefs.SetString("lastAuthPass", str3);
            FengGameManagerMKII.OnPrivateServer = true;
            FengGameManagerMKII.PrivateServerAuthPass = str3;
        }
    }
}

