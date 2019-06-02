using UnityEngine;

[AddComponentMenu("NGUI/Tween/Field of View"), RequireComponent(typeof(Camera))]
public class TweenFOV : UITweener
{
    public float from;
    private Camera mCam;
    public float to;

    public static TweenFOV Begin(GameObject go, float duration, float to)
    {
        var nfov = Begin<TweenFOV>(go, duration);
        nfov.from = nfov.fov;
        nfov.to = to;
        if (duration <= 0f)
        {
            nfov.Sample(1f, true);
            nfov.enabled = false;
        }
        return nfov;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        cachedCamera.fieldOfView = @from * (1f - factor) + to * factor;
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

    public float fov
    {
        get
        {
            return cachedCamera.fieldOfView;
        }
        set
        {
            cachedCamera.fieldOfView = value;
        }
    }
}

