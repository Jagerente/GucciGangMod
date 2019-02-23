//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using GGM;

public class UIMainReferences : MonoBehaviour
{
    public static string fengVersion = "01042015";
    private static bool isGAMEFirstLaunch = true;
    public GameObject panelCredits;
    public GameObject PanelDisconnect;
    public GameObject panelMain;
    public GameObject PanelMultiJoinPrivate;
    public GameObject PanelMultiPWD;
    public GameObject panelMultiROOM;
    public GameObject panelMultiSet;
    public GameObject panelMultiStart;
    public GameObject PanelMultiWait;
    public GameObject panelOption;
    public GameObject panelSingleSet;
    public GameObject PanelSnapShot;
    public static string version = "01042015";

    public IEnumerator request()
    {
        var url = Application.dataPath + "/RCAssets.unity3d";
        if (!Application.isWebPlayer)
        {
            url = "File://" + url;
        }
        while (!Caching.ready)
        {
            yield return null;
        }
        var version = 1;
        using (var link = WWW.LoadFromCacheOrDownload(url, version))
        {
            yield return link;
            if (link.error != null)
            {
                throw new Exception("WWW download had an error:" + link.error);
            }
            FengGameManagerMKII.RCassets = link.assetBundle;
            FengGameManagerMKII.isAssetLoaded = true;
        }
    }

    private void Start()
    {
        gameObject.AddComponent<Style>();
        NGUITools.SetActive(panelMain, true);
        GameObject.Find("VERSION").GetComponent<UILabel>().text = string.Empty;
        if (isGAMEFirstLaunch)
        {
            version = fengVersion;
            isGAMEFirstLaunch = false;
            var target = (GameObject) Instantiate(Resources.Load("InputManagerController"));
            target.name = "InputManagerController";
            DontDestroyOnLoad(target);
            FengGameManagerMKII.s = (
                "verified343," +//0
                "hair," +//1
                "character_eye," +//2
                "glass," +//3
                "character_face," +//4
                "character_head," +//5
                "character_hand," +//6
                "character_body," +//7
                "character_arm," +//8
                "character_leg," +//9
                "character_chest," +//10
                "character_cape," +//11
                "character_brand," +//12
                "character_3dmg," +//13
                "r," +//14
                "character_blade_l," +//15
                "character_3dmg_gas_r," +//16
                "character_blade_r," +//17
                "3dmg_smoke," +//18
                "HORSE," +//19
                "hair," +//20
                "body_001," +//21
                "Cube," +//22
                "Plane_031," +//23
                "mikasa_asset," +//24
                "character_cap_," +//25
                "character_gun"//26
                ).Split(',');
            StartCoroutine(request());
            GameObject MyGameObj = new GameObject();
            string URL = "File://" + Application.dataPath + "/visualAssets.unity3d";
            WWW visuals = WWW.LoadFromCacheOrDownload(URL, 72);
            AssetBundle visualBundle = visuals.assetBundle;
            //(Material)visualBundle.Load("outline");
            Settings.BlendForBLoom = (Shader)visualBundle.Load("BlendForBloom");
            Settings.BlurAndFlares = (Shader)visualBundle.Load("BlurAndFlares");
            Settings.BrightPassFilter = (Shader)visualBundle.Load("BrightPassFilter2");
            Settings.LensFlareShader = (Shader)visualBundle.Load("LensFlareCreate");
            UnityEngine.Object.DontDestroyOnLoad(MyGameObj);
            FengGameManagerMKII.loginstate = 0;

        }
        if (FengGameManagerMKII.shallRejoin[0] is bool && (bool)FengGameManagerMKII.shallRejoin[0])
        {
            PhotonNetwork.ConnectToMaster((string)FengGameManagerMKII.shallRejoin[1], 5055, FengGameManagerMKII.applicationId, version);
        }
        gameObject.AddComponent<RichPresence>();
    }

}

