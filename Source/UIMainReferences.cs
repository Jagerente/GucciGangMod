using System.Collections;
using GGM;
using GGM.Config;
using GGM.GUI.Pages;
using UnityEngine;

public class UIMainReferences : MonoBehaviour
{
    public static UIMainReferences instance;
    private static bool isFirstLaunch = true;
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
    public const string PublicKey = "01042015";
    public const string Version = "v5.7.10";

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator OnOpen()
    {
        yield return StartCoroutine(AssetsManager.LoadRCAssets());
        yield return StartCoroutine(AssetsManager.LoadFonts());
    }

    private void Start()
    {
        gameObject.AddComponent<GGM.GUI.Styles>();
        gameObject.AddComponent<PagesManager>();
        StartCoroutine(OnOpen());
        if (isFirstLaunch)
        {
            isFirstLaunch = false;
            ServerKey = PublicKey;
            var target = (GameObject) Instantiate(Resources.Load("InputManagerController"));
            target.name = "InputManagerController";
            var richPresence = gameObject.AddComponent<GGM.Discord.RichPresence>();
            DontDestroyOnLoad(richPresence);
            DontDestroyOnLoad(target);
            FengGameManagerMKII.s = "verified343,hair,character_eye,glass,character_face,character_head,character_hand,character_body,character_arm,character_leg,character_chest,character_cape,character_brand,character_3dmg,r,character_blade_l,character_3dmg_gas_r,character_blade_r,3dmg_smoke,HORSE,hair,body_001,Cube,Plane_031,mikasa_asset,character_cap_,character_gun".Split(',');
            FengGameManagerMKII.loginstate = 0;
        }
        else
        {

            Labels.Version = $"GucciGangMod {Version}";
        }
        NGUITools.SetActive(panelMain, true);
    }
}