using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class NGUITools
{
    private static float mGlobalVolume = 1f;
    private static Color mInvisible = new Color(0f, 0f, 0f, 0f);
    private static AudioListener mListener;
    private static bool mLoaded;

    private static void Activate(Transform t)
    {
        SetActiveSelf(t.gameObject, true);
        var index = 0;
        var childCount = t.childCount;
        while (index < childCount)
        {
            if (t.GetChild(index).gameObject.activeSelf)
            {
                return;
            }
            index++;
        }
        var num3 = 0;
        var num4 = t.childCount;
        while (num3 < num4)
        {
            Activate(t.GetChild(num3));
            num3++;
        }
    }

    public static GameObject AddChild(GameObject parent)
    {
        var obj2 = new GameObject();
        if (parent != null)
        {
            var transform = obj2.transform;
            transform.parent = parent.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            obj2.layer = parent.layer;
        }
        return obj2;
    }

    public static T AddChild<T>(GameObject parent) where T: Component
    {
        var obj2 = AddChild(parent);
        obj2.name = GetName<T>();
        return obj2.AddComponent<T>();
    }

    public static GameObject AddChild(GameObject parent, GameObject prefab)
    {
        var obj2 = Object.Instantiate(prefab) as GameObject;
        if (obj2 != null && parent != null)
        {
            var transform = obj2.transform;
            transform.parent = parent.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            obj2.layer = parent.layer;
        }
        return obj2;
    }

    public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
    {
        var sprite = atlas == null ? null : atlas.GetSprite(spriteName);
        var sprite2 = AddWidget<UISprite>(go);
        sprite2.type = sprite != null && !(sprite.inner == sprite.outer) ? UISprite.Type.Sliced : UISprite.Type.Simple;
        sprite2.atlas = atlas;
        sprite2.spriteName = spriteName;
        return sprite2;
    }

    public static T AddWidget<T>(GameObject go) where T: UIWidget
    {
        var num = CalculateNextDepth(go);
        var local = AddChild<T>(go);
        local.depth = num;
        var transform = local.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(100f, 100f, 1f);
        local.gameObject.layer = go.layer;
        return local;
    }

    public static BoxCollider AddWidgetCollider(GameObject go)
    {
        if (go == null)
        {
            return null;
        }
        var component = go.GetComponent<Collider>();
        var collider2 = component as BoxCollider;
        if (collider2 == null)
        {
            if (component != null)
            {
                if (Application.isPlaying)
                {
                    Object.Destroy(component);
                }
                else
                {
                    Object.DestroyImmediate(component);
                }
            }
            collider2 = go.AddComponent<BoxCollider>();
        }
        var num = CalculateNextDepth(go);
        var bounds = NGUIMath.CalculateRelativeWidgetBounds(go.transform);
        collider2.isTrigger = true;
        collider2.center = bounds.center + Vector3.back * (num * 0.25f);
        collider2.size = new Vector3(bounds.size.x, bounds.size.y, 0f);
        return collider2;
    }

    public static Color ApplyPMA(Color c)
    {
        if (c.a != 1f)
        {
            c.r *= c.a;
            c.g *= c.a;
            c.b *= c.a;
        }
        return c;
    }

    public static void Broadcast(string funcName)
    {
        var objArray = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var index = 0;
        var length = objArray.Length;
        while (index < length)
        {
            objArray[index].SendMessage(funcName, SendMessageOptions.DontRequireReceiver);
            index++;
        }
    }

    public static void Broadcast(string funcName, object param)
    {
        var objArray = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var index = 0;
        var length = objArray.Length;
        while (index < length)
        {
            objArray[index].SendMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
            index++;
        }
    }

    public static int CalculateNextDepth(GameObject go)
    {
        var a = -1;
        var componentsInChildren = go.GetComponentsInChildren<UIWidget>();
        var index = 0;
        var length = componentsInChildren.Length;
        while (index < length)
        {
            a = Mathf.Max(a, componentsInChildren[index].depth);
            index++;
        }
        return a + 1;
    }

    private static void Deactivate(Transform t)
    {
        SetActiveSelf(t.gameObject, false);
    }

    public static void Destroy(Object obj)
    {
        if (obj != null)
        {
            if (Application.isPlaying)
            {
                if (obj is GameObject)
                {
                    var obj2 = obj as GameObject;
                    obj2.transform.parent = null;
                }
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj);
            }
        }
    }

    public static void DestroyImmediate(Object obj)
    {
        if (obj != null)
        {
            if (Application.isEditor)
            {
                Object.DestroyImmediate(obj);
            }
            else
            {
                Object.Destroy(obj);
            }
        }
    }

    public static string EncodeColor(Color c)
    {
        var num = 0xffffff & (NGUIMath.ColorToInt(c) >> 8);
        return NGUIMath.DecimalToHex(num);
    }

    public static T[] FindActive<T>() where T: Component
    {
        return Object.FindObjectsOfType(typeof(T)) as T[];
    }

    public static Camera FindCameraForLayer(int layer)
    {
        var num = 1 << layer;
        var cameraArray = FindActive<Camera>();
        var index = 0;
        var length = cameraArray.Length;
        while (index < length)
        {
            var camera = cameraArray[index];
            if ((camera.cullingMask & num) != 0)
            {
                return camera;
            }
            index++;
        }
        return null;
    }

    public static T FindInParents<T>(GameObject go) where T: Component
    {
        if (go == null)
        {
            return null;
        }
        object component = go.GetComponent<T>();
        if (component == null)
        {
            for (var transform = go.transform.parent; transform != null && component == null; transform = transform.parent)
            {
                component = transform.gameObject.GetComponent<T>();
            }
        }
        return (T) component;
    }

    public static bool GetActive(GameObject go)
    {
        return go != null && go.activeInHierarchy;
    }

    public static string GetHierarchy(GameObject obj)
    {
        var name = obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            name = obj.name + "/" + name;
        }
        return "\"" + name + "\"";
    }

    public static string GetName<T>() where T: Component
    {
        var str = typeof(T).ToString();
        if (str.StartsWith("UI"))
        {
            return str.Substring(2);
        }
        if (str.StartsWith("UnityEngine."))
        {
            str = str.Substring(12);
        }
        return str;
    }

    public static GameObject GetRoot(GameObject go)
    {
        var transform = go.transform;
        while (true)
        {
            var parent = transform.parent;
            if (parent == null)
            {
                return transform.gameObject;
            }
            transform = parent;
        }
    }

    public static bool IsChild(Transform parent, Transform child)
    {
        if (parent != null && child != null)
        {
            while (child != null)
            {
                if (child == parent)
                {
                    return true;
                }
                child = child.parent;
            }
        }
        return false;
    }

    public static byte[] Load(string fileName)
    {
        return null;
    }

    public static void MakePixelPerfect(Transform t)
    {
        var component = t.GetComponent<UIWidget>();
        if (component != null)
        {
            component.MakePixelPerfect();
        }
        else
        {
            t.localPosition = Round(t.localPosition);
            t.localScale = Round(t.localScale);
            var index = 0;
            var childCount = t.childCount;
            while (index < childCount)
            {
                MakePixelPerfect(t.GetChild(index));
                index++;
            }
        }
    }

    public static void MarkParentAsChanged(GameObject go)
    {
        var componentsInChildren = go.GetComponentsInChildren<UIWidget>();
        var index = 0;
        var length = componentsInChildren.Length;
        while (index < length)
        {
            componentsInChildren[index].ParentHasChanged();
            index++;
        }
    }

    public static WWW OpenURL(string url)
    {
        WWW www = null;
        try
        {
            www = new WWW(url);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }
        return www;
    }

    public static WWW OpenURL(string url, WWWForm form)
    {
        if (form == null)
        {
            return OpenURL(url);
        }
        WWW www = null;
        try
        {
            www = new WWW(url, form);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception == null ? "<null>" : exception.Message);
        }
        return www;
    }

    public static Color ParseColor(string text, int offset)
    {
        var num = (NGUIMath.HexToDecimal(text[offset]) << 4) | NGUIMath.HexToDecimal(text[offset + 1]);
        var num2 = (NGUIMath.HexToDecimal(text[offset + 2]) << 4) | NGUIMath.HexToDecimal(text[offset + 3]);
        var num3 = (NGUIMath.HexToDecimal(text[offset + 4]) << 4) | NGUIMath.HexToDecimal(text[offset + 5]);
        var num4 = 0.003921569f;
        return new Color(num4 * num, num4 * num2, num4 * num3);
    }

    public static int ParseSymbol(string text, int index, List<Color> colors, bool premultiply)
    {
        var length = text.Length;
        if (index + 2 < length)
        {
            if (text[index + 1] == '-')
            {
                if (text[index + 2] == ']')
                {
                    if (colors != null && colors.Count > 1)
                    {
                        colors.RemoveAt(colors.Count - 1);
                    }
                    return 3;
                }
            }
            else if (index + 7 < length && text[index + 7] == ']')
            {
                if (colors != null)
                {
                    var c = ParseColor(text, index + 1);
                    if (EncodeColor(c) != text.Substring(index + 1, 6).ToUpper())
                    {
                        return 0;
                    }
                    var color2 = colors[colors.Count - 1];
                    c.a = color2.a;
                    if (premultiply && c.a != 1f)
                    {
                        c = Color.Lerp(mInvisible, c, c.a);
                    }
                    colors.Add(c);
                }
                return 8;
            }
        }
        return 0;
    }

    public static AudioSource PlaySound(AudioClip clip)
    {
        return PlaySound(clip, 1f, 1f);
    }

    public static AudioSource PlaySound(AudioClip clip, float volume)
    {
        return PlaySound(clip, volume, 1f);
    }

    public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
    {
        volume *= soundVolume;
        if (clip != null && volume > 0.01f)
        {
            if (mListener == null)
            {
                mListener = Object.FindObjectOfType(typeof(AudioListener)) as AudioListener;
                if (mListener == null)
                {
                    var main = Camera.main;
                    if (main == null)
                    {
                        main = Object.FindObjectOfType(typeof(Camera)) as Camera;
                    }
                    if (main != null)
                    {
                        mListener = main.gameObject.AddComponent<AudioListener>();
                    }
                }
            }
            if (mListener != null && mListener.enabled && GetActive(mListener.gameObject))
            {
                var audio = mListener.audio;
                if (audio == null)
                {
                    audio = mListener.gameObject.AddComponent<AudioSource>();
                }
                audio.pitch = pitch;
                audio.PlayOneShot(clip, volume);
                return audio;
            }
        }
        return null;
    }

    public static int RandomRange(int min, int max)
    {
        if (min == max)
        {
            return min;
        }
        return Random.Range(min, max + 1);
    }

    public static Vector3 Round(Vector3 v)
    {
        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        v.z = Mathf.Round(v.z);
        return v;
    }

    public static bool Save(string fileName, byte[] bytes)
    {
        return false;
    }

    public static void SetActive(GameObject go, bool state)
    {
        if (state)
        {
            Activate(go.transform);
        }
        else
        {
            Deactivate(go.transform);
        }
    }

    public static void SetActiveChildren(GameObject go, bool state)
    {
        var transform = go.transform;
        if (state)
        {
            var index = 0;
            var childCount = transform.childCount;
            while (index < childCount)
            {
                Activate(transform.GetChild(index));
                index++;
            }
        }
        else
        {
            var num3 = 0;
            var num4 = transform.childCount;
            while (num3 < num4)
            {
                Deactivate(transform.GetChild(num3));
                num3++;
            }
        }
    }

    public static void SetActiveSelf(GameObject go, bool state)
    {
        go.SetActive(state);
    }

    public static void SetLayer(GameObject go, int layer)
    {
        go.layer = layer;
        var transform = go.transform;
        var index = 0;
        var childCount = transform.childCount;
        while (index < childCount)
        {
            SetLayer(transform.GetChild(index).gameObject, layer);
            index++;
        }
    }

    public static string StripSymbols(string text)
    {
        if (text != null)
        {
            var index = 0;
            var length = text.Length;
            while (index < length)
            {
                var ch = text[index];
                if (ch == '[')
                {
                    var count = ParseSymbol(text, index, null, false);
                    if (count > 0)
                    {
                        text = text.Remove(index, count);
                        length = text.Length;
                        continue;
                    }
                }
                index++;
            }
        }
        return text;
    }

    public static string clipboard
    {
        get
        {
            return null;
        }
        set
        {
        }
    }

    public static bool fileAccess
    {
        get
        {
            return Application.platform != RuntimePlatform.WindowsWebPlayer && Application.platform != RuntimePlatform.OSXWebPlayer;
        }
    }

    public static float soundVolume
    {
        get
        {
            if (!mLoaded)
            {
                mLoaded = true;
                mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
            }
            return mGlobalVolume;
        }
        set
        {
            if (mGlobalVolume != value)
            {
                mLoaded = true;
                mGlobalVolume = value;
                PlayerPrefs.SetFloat("Sound", value);
            }
        }
    }
}

