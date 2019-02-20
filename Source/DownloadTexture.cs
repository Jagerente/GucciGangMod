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

[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
    private Material mMat;
    private Texture2D mTex;
    public string url = "http://www.tasharen.com/misc/logo.png";

    private void OnDestroy()
    {
        if (mMat != null)
        {
            Destroy(mMat);
        }
        if (mTex != null)
        {
            Destroy(mTex);
        }
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
        return new StartcIterator7 { fthis = this };
    }

    [CompilerGenerated]
    private sealed class StartcIterator7 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal DownloadTexture fthis;
        internal UITexture ut1;
        internal WWW www0;

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
                    www0 = new WWW(fthis.url);
                    Scurrent = www0;
                    SPC = 1;
                    return true;

                case 1:
                    fthis.mTex = www0.texture;
                    if (fthis.mTex == null)
                    {
                        goto Label_0118;
                    }
                    ut1 = fthis.GetComponent<UITexture>();
                    if (ut1.material != null)
                    {
                        fthis.mMat = new Material(ut1.material);
                        break;
                    }
                    fthis.mMat = new Material(Shader.Find("Unlit/Transparent Colored"));
                    break;

                default:
                    goto Label_012A;
            }
            ut1.material = fthis.mMat;
            fthis.mMat.mainTexture = fthis.mTex;
            ut1.MakePixelPerfect();
        Label_0118:
            www0.Dispose();
            SPC = -1;
        Label_012A:
            return false;
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

