using GGM.GUI.Pages;
using System;
using System.Collections;
using UnityEngine;

namespace GGM.Config
{
    internal class AssetsManager : MonoBehaviour
    {
        public static IEnumerator LoadFonts()
        {
            if (GUI.Styles.Fonts != null) yield break;
            var bundle = AssetBundle.CreateFromMemory(System.IO.File.ReadAllBytes(Application.dataPath + "/Resources/ggmfonts.unity3d"));
            yield return bundle;
            GUI.Styles.Fonts = new[]
            {
                (Font)bundle.assetBundle.Load("chemistry"),
                (Font)bundle.assetBundle.Load("tahoma"),
                (Font)bundle.assetBundle.Load("rabelo"),
                (Font)bundle.assetBundle.Load("bienetresocial"),
                (Font)bundle.assetBundle.Load("mandatory")
            };
            Labels.Version = $"GucciGangMod {UIMainReferences.Version}";
        }

        public static IEnumerator LoadRCAssets()
        {
            if (FengGameManagerMKII.isAssetLoaded) yield break;
            Page.GetInstance<LoadingScreen>().Enable();
            var url = Application.dataPath + "/RCAssets.unity3d";
            if (!Application.isWebPlayer)
            {
                url = "File://" + url;
            }
            while (!UnityEngine.Caching.ready)
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
            Page.GetInstance<LoadingScreen>().Disable();
        }
    }
}