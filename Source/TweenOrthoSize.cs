using UnityEngine;

[AddComponentMenu("NGUI/Tween/Orthographic Size"), RequireComponent(typeof(Camera))]
public class TweenOrthoSize : UITweener
{
    public float from;
    private Camera mCam;
    public float to;

    public static TweenOrthoSize Begin(GameObject go, float duration, float to)
    {
        var size = Begin<TweenOrthoSize>(go, duration);
        size.from = size.orthoSize;
        size.to = to;
        if (duration <= 0f)
        {
            size.Sample(1f, true);
            size.enabled = false;
        }
        return size;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        cachedCamera.orthographicSize = @from * (1f - factor) + to * factor;
    }

    public Camera cachedCamera
    {
        get
        {
            if (mCam == null)
            {
                mCam = camera;
            }
            return mCam;
        }
    }

    public float orthoSize
    {
        get
        {
            return cachedCamera.orthographicSize;
        }
        set
        {
            cachedCamera.orthographicSize = value;
        }
    }
}

