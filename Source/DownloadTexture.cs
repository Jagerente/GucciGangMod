using System;
using System.Collections;
using System.Collections.Generic;
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

    
    private IEnumerator Start()
    {
        return new Startc__Iterator7 { f__this = this };
    }

    
    private sealed class Startc__Iterator7 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal DownloadTexture f__this;
        internal UITexture ut__1;
        internal WWW www__0;

        
        public void Dispose()
        {
            PC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) PC;
            PC = -1;
            switch (num)
            {
                case 0:
                    www__0 = new WWW(f__this.url);
                    current = www__0;
                    PC = 1;
                    return true;

                case 1:
                    f__this.mTex = www__0.texture;
                    if (f__this.mTex == null)
                    {
                        goto Label_0118;
                    }
                    ut__1 = f__this.GetComponent<UITexture>();
                    if (ut__1.material != null)
                    {
                        f__this.mMat = new Material(ut__1.material);
                        break;
                    }
                    f__this.mMat = new Material(Shader.Find("Unlit/Transparent Colored"));
                    break;

                default:
                    goto Label_012A;
            }
            ut__1.material = f__this.mMat;
            f__this.mMat.mainTexture = f__this.mTex;
            ut__1.MakePixelPerfect();
        Label_0118:
            www__0.Dispose();
            PC = -1;
        Label_012A:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return current;
            }
        }
    }
}

