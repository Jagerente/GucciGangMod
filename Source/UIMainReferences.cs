using System;
using System.Collections;
using UnityEngine;

public class UIMainReferences : MonoBehaviour
{
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
    public static string ServerKey;
    public static string PublicKey = "01042015";
    public const string Version = "GucciGangMod v5.6.24";

    private void Awake()
    {
        instance = this;
    }

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
        using (var iteratorVariable2 = WWW.LoadFromCacheOrDownload(url, version))
        {
            yield return iteratorVariable2;
            if (iteratorVariable2.error != null)
            {
                throw new Exception("WWW download had an error:" + iteratorVariable2.error);
            }
            FengGameManagerMKII.RCassets = iteratorVariable2.assetBundle;
            FengGameManagerMKII.isAssetLoaded = true;
        }
    }

    private IEnumerator OnOpen()
    {
        yield return StartCoroutine(request());
        yield return StartCoroutine(GGM.Labels.LoadFonts());
        GGM.Labels.Version = Version;
    }

    private void Start()
    {
        gameObject.AddComponent<GGM.GUI.Styles>();

        NGUITools.SetActive(panelMain, true);
        GGM.Labels.Version = Version;
        if (isGAMEFirstLaunch)
        {
            isGAMEFirstLaunch = false;
            ServerKey = PublicKey;
            var target = (GameObject) Instantiate(Resources.Load("InputManagerController"));
            target.name = "InputManagerController";
            DontDestroyOnLoad(target);
            FengGameManagerMKII.s = "verified343,hair,character_eye,glass,character_face,character_head,character_hand,character_body,character_arm,character_leg,character_chest,character_cape,character_brand,character_3dmg,r,character_blade_l,character_3dmg_gas_r,character_blade_r,3dmg_smoke,HORSE,hair,body_001,Cube,Plane_031,mikasa_asset,character_cap_,character_gun".Split(',');
            StartCoroutine(OnOpen());
            FengGameManagerMKII.loginstate = 0;
        }
    }

}

