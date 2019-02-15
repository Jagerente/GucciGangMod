//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using UnityEngine;
using GGP;

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
        string url = Application.dataPath + "/RCAssets.unity3d";
        if (!Application.isWebPlayer)
        {
            url = "File://" + url;
        }
        while (!Caching.ready)
        {
            yield return null;
        }
        int version = 1;
        using (WWW iteratorVariable2 = WWW.LoadFromCacheOrDownload(url, version))
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

    private void Start()
    {
        base.gameObject.AddComponent<Style>();
        NGUITools.SetActive(this.panelMain, true);
        GameObject.Find("VERSION").GetComponent<UILabel>().text = string.Empty;
        if (isGAMEFirstLaunch)
        {
            version = fengVersion;
            isGAMEFirstLaunch = false;
            GameObject target = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("InputManagerController"));
            target.name = "InputManagerController";
            UnityEngine.Object.DontDestroyOnLoad(target);
            FengGameManagerMKII.s = "verified343,hair,character_eye,glass,character_face,character_head,character_hand,character_body,character_arm,character_leg,character_chest,character_cape,character_brand,character_3dmg,r,character_blade_l,character_3dmg_gas_r,character_blade_r,3dmg_smoke,HORSE,hair,body_001,Cube,Plane_031,mikasa_asset,character_cap_,character_gun".Split(new char[] { ',' });
            StartCoroutine(request());
            FengGameManagerMKII.loginstate = 0;
        }
    }

}

