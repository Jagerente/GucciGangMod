//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Tween/Volume")]
public class TweenVolume : UITweener
{
    public float from;
    private AudioSource mSource;
    public float to = 1f;

    public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
    {
        var volume = UITweener.Begin<TweenVolume>(go, duration);
        volume.from = volume.volume;
        volume.to = targetVolume;
        if (duration <= 0f)
        {
            volume.Sample(1f, true);
            volume.enabled = false;
        }
        return volume;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        volume = (from * (1f - factor)) + (to * factor);
        mSource.enabled = mSource.volume > 0.01f;
    }

    public AudioSource audioSource
    {
        get
        {
            if (mSource == null)
            {
                mSource = audio;
                if (mSource == null)
                {
                    mSource = GetComponentInChildren<AudioSource>();
                    if (mSource == null)
                    {
                        Debug.LogError("TweenVolume needs an AudioSource to work with", this);
                        enabled = false;
                    }
                }
            }
            return mSource;
        }
    }

    public float volume
    {
        get
        {
            return audioSource.volume;
        }
        set
        {
            audioSource.volume = value;
        }
    }
}

