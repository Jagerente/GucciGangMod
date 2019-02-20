//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BTN_save_snapshot : MonoBehaviour
{
    public GameObject info;
    public GameObject targetTexture;
    public GameObject[] thingsNeedToHide;

    private void OnClick()
    {
        foreach (var obj2 in thingsNeedToHide)
        {
            var transform = obj2.transform;
            transform.position += Vector3.up * 10000f;
        }
        StartCoroutine(ScreenshotEncode());
        info.GetComponent<UILabel>().text = "trying..";
    }

    [DebuggerHidden]
    private IEnumerator ScreenshotEncode()
    {
        return new ScreenshotEncodecIterator0 { fthis = this };
    }

    [CompilerGenerated]
    private sealed class ScreenshotEncodecIterator0 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal GameObject[] Ss_52;
        internal int Ss_63;
        internal BTN_save_snapshot fthis;
        internal GameObject go4;
        internal string img_name5;
        internal float r0;
        internal Texture2D texture1;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    Scurrent = new WaitForEndOfFrame();
                    SPC = 1;
                    goto Label_0308;

                case 1:
                    r0 = Screen.height / 600f;
                    texture1 = new Texture2D((int) (r0 * fthis.targetTexture.transform.localScale.x), (int) (r0 * fthis.targetTexture.transform.localScale.y), TextureFormat.RGB24, false);
                    texture1.ReadPixels(new Rect((Screen.width * 0.5f) - (texture1.width * 0.5f), ((Screen.height * 0.5f) - (texture1.height * 0.5f)) - (r0 * 0f), texture1.width, texture1.height), 0, 0);
                    texture1.Apply();
                    Scurrent = 0;
                    SPC = 2;
                    goto Label_0308;

                case 2:
                {
                    Ss_52 = fthis.thingsNeedToHide;
                    Ss_63 = 0;
                    while (Ss_63 < Ss_52.Length)
                    {
                        go4 = Ss_52[Ss_63];
                        var transform = go4.transform;
                        transform.position -= Vector3.up * 10000f;
                        Ss_63++;
                    }
                    var textArray1 = new string[] { "aottg_ss-", DateTime.Today.Month.ToString(), "_", DateTime.Today.Day.ToString(), "_", DateTime.Today.Year.ToString(), "-", DateTime.Now.Hour.ToString(), "_", DateTime.Now.Minute.ToString(), "_", DateTime.Now.Second.ToString(), ".png" };
                    img_name5 = string.Concat(textArray1);
                    var args = new object[] { img_name5, texture1.width, texture1.height, Convert.ToBase64String(texture1.EncodeToPNG()) };
                    Application.ExternalCall("SaveImg", args);
                    DestroyObject(texture1);
                    SPC = -1;
                    break;
                }
            }
            return false;
        Label_0308:
            return true;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }
}

