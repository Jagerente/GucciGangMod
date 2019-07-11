using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Root"), ExecuteInEditMode]
public class UIRoot : MonoBehaviour
{
    [HideInInspector] public bool automatic;
    public int manualHeight = 720;
    public int maximumHeight = 1536;
    public int minimumHeight = 320;
    private static List<UIRoot> mRoots = new List<UIRoot>();
    private Transform mTrans;
    public Scaling scalingStyle = Scaling.FixedSize;

    private void Awake()
    {
        mTrans = transform;
        mRoots.Add(this);
        if (automatic)
        {
            scalingStyle = Scaling.PixelPerfect;
            automatic = false;
        }
    }

    public static void Broadcast(string funcName)
    {
        var num = 0;
        var count = mRoots.Count;
        while (num < count)
        {
            var root = mRoots[num];
            if (root != null)
            {
                root.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
            }

            num++;
        }
    }

    public static void Broadcast(string funcName, object param)
    {
        if (param == null)
        {
            Debug.LogError(
                "SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
        }
        else
        {
            var num = 0;
            var count = mRoots.Count;
            while (num < count)
            {
                var root = mRoots[num];
                if (root != null)
                {
                    root.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
                }

                num++;
            }
        }
    }

    public float GetPixelSizeAdjustment(int height)
    {
        height = Mathf.Max(2, height);
        if (scalingStyle == Scaling.FixedSize)
        {
            return manualHeight / (float) height;
        }

        if (height < minimumHeight)
        {
            return minimumHeight / (float) height;
        }

        if (height > maximumHeight)
        {
            return maximumHeight / (float) height;
        }

        return 1f;
    }

    public static float GetPixelSizeAdjustment(GameObject go)
    {
        var root = NGUITools.FindInParents<UIRoot>(go);
        return root == null ? 1f : root.pixelSizeAdjustment;
    }

    private void OnDestroy()
    {
        mRoots.Remove(this);
    }

    private void Start()
    {
        var componentInChildren = GetComponentInChildren<UIOrthoCamera>();
        if (componentInChildren != null)
        {
            Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.",
                componentInChildren);
            var component = componentInChildren.gameObject.GetComponent<Camera>();
            componentInChildren.enabled = false;
            if (component != null)
            {
                component.orthographicSize = 1f;
            }
        }
    }

    private void Update()
    {
        if (mTrans != null)
        {
            float activeHeight = this.activeHeight;
            if (activeHeight > 0f)
            {
                var x = 2f / activeHeight;
                var localScale = mTrans.localScale;
                if (Mathf.Abs(localScale.x - x) > float.Epsilon || Mathf.Abs(localScale.y - x) > float.Epsilon ||
                    Mathf.Abs(localScale.z - x) > float.Epsilon)
                {
                    mTrans.localScale = new Vector3(x, x, x);
                }
            }
        }
    }

    public int activeHeight
    {
        get
        {
            var num = Mathf.Max(2, Screen.height);
            if (scalingStyle == Scaling.FixedSize)
            {
                return manualHeight;
            }

            if (num < minimumHeight)
            {
                return minimumHeight;
            }

            if (num > maximumHeight)
            {
                return maximumHeight;
            }

            return num;
        }
    }

    public static List<UIRoot> list
    {
        get { return mRoots; }
    }

    public float pixelSizeAdjustment
    {
        get { return GetPixelSizeAdjustment(Screen.height); }
    }

    public enum Scaling
    {
        PixelPerfect,
        FixedSize,
        FixedSizeOnMobiles
    }
}