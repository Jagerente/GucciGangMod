using System;
using System.Collections;
using System.Collections.Generic;
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

    
    private IEnumerator ScreenshotEncode()
    {
        return new ScreenshotEncodec__Iterator0 { f__this = this };
    }

    
    private sealed class ScreenshotEncodec__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal GameObject[] s_5__2;
        internal int s_6__3;
        internal BTN_save_snapshot f__this;
        internal GameObject go__4;
        internal string img_name__5;
        internal float r__0;
        internal Texture2D texture__1;

        
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
                    current = new WaitForEndOfFrame();
                    PC = 1;
                    goto Label_0308;

                case 1:
                    r__0 = Screen.height / 600f;
                    texture__1 = new Texture2D((int) (r__0 * f__this.targetTexture.transform.localScale.x), (int) (r__0 * f__this.targetTexture.transform.localScale.y), TextureFormat.RGB24, false);
                    texture__1.ReadPixels(new Rect(Screen.width * 0.5f - texture__1.width * 0.5f, Screen.height * 0.5f - texture__1.height * 0.5f - r__0 * 0f, texture__1.width, texture__1.height), 0, 0);
                    texture__1.Apply();
                    current = 0;
                    PC = 2;
                    goto Label_0308;

                case 2:
                {
                    s_5__2 = f__this.thingsNeedToHide;
                    s_6__3 = 0;
                    while (s_6__3 < s_5__2.Length)
                    {
                        go__4 = s_5__2[s_6__3];
                        var transform = go__4.transform;
                        transform.position -= Vector3.up * 10000f;
                        s_6__3++;
                    }
                    string[] textArray1 = { "aottg_ss-", DateTime.Today.Month.ToString(), "_", DateTime.Today.Day.ToString(), "_", DateTime.Today.Year.ToString(), "-", DateTime.Now.Hour.ToString(), "_", DateTime.Now.Minute.ToString(), "_", DateTime.Now.Second.ToString(), ".png" };
                    img_name__5 = string.Concat(textArray1);
                    object[] args = { img_name__5, texture__1.width, texture__1.height, Convert.ToBase64String(texture__1.EncodeToPNG()) };
                    Application.ExternalCall("SaveImg", args);
                    DestroyObject(texture__1);
                    PC = -1;
                    break;
                }
            }
            return false;
        Label_0308:
            return true;
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

