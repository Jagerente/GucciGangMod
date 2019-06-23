using System;
using System.Collections;
using UnityEngine;

public class UIMainReferences : MonoBehaviour
{
    public static string fengVersion;
    public static UIMainReferences instance;
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

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator request(string versionShow, string versionForm)
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
        using (var iteratorVariable2 = WWW.LoadFromCacheOrDownload(url, version))
        {
            yield return iteratorVariable2;
            if (iteratorVariable2.error != null)
            {
                throw new Exception("WWW download had an error:" + iteratorVariable2.error);
            }
            FengGameManagerMKII.RCassets = iteratorVariable2.assetBundle;
            FengGameManagerMKII.isAssetLoaded = true;
            FengGameManagerMKII.instance.setBackground();
        }
    }

    private void Start()
    {
        var versionShow = "8/12/2015";
        var versionForm = "08122015";
        fengVersion = "01042015";
        NGUITools.SetActive(panelMain, true);
        if (version == null || version.StartsWith("error"))
        {
            GGM.Caching.GameObjectCache.Find("VERSION").GetComponent<UILabel>().text = "Verification failed. Please clear your cache or try another browser";
        }
        else if (version.StartsWith("outdated"))
        {
            GGM.Caching.GameObjectCache.Find("VERSION").GetComponent<UILabel>().text = "Mod is outdated. Please clear your cache or try another browser.";
        }
        else
        {
            GGM.Caching.GameObjectCache.Find("VERSION").GetComponent<UILabel>().text = "Client verified. Last updated " + versionShow + ".";
        }
        if (isGAMEFirstLaunch)
        {
            version = fengVersion;
            isGAMEFirstLaunch = false;
            var target = (GameObject) Instantiate(Resources.Load("InputManagerController"));
            target.name = "InputManagerController";
            DontDestroyOnLoad(target);
            GGM.Caching.GameObjectCache.Find("VERSION").GetComponent<UILabel>().text = "Client verified. Last updated " + versionShow + ".";
            FengGameManagerMKII.s = "verified343,hair,character_eye,glass,character_face,character_head,character_hand,character_body,character_arm,character_leg,character_chest,character_cape,character_brand,character_3dmg,r,character_blade_l,character_3dmg_gas_r,character_blade_r,3dmg_smoke,HORSE,hair,body_001,Cube,Plane_031,mikasa_asset,character_cap_,character_gun".Split(',');
            
            StartCoroutine(request(versionShow, versionForm));
            FengGameManagerMKII.loginstate = 0;
        }
    }

}

