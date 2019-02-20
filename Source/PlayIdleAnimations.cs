//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Play Idle Animations")]
public class PlayIdleAnimations : MonoBehaviour
{
    private Animation mAnim;
    private List<AnimationClip> mBreaks = new List<AnimationClip>();
    private AnimationClip mIdle;
    private int mLastIndex;
    private float mNextBreak;

    private void Start()
    {
        mAnim = GetComponentInChildren<Animation>();
        if (mAnim == null)
        {
            Debug.LogWarning(NGUITools.GetHierarchy(gameObject) + " has no Animation component");
            Destroy(this);
        }
        else
        {
            var enumerator = mAnim.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (AnimationState) enumerator.Current;
                    if (current.clip.name == "idle")
                    {
                        current.layer = 0;
                        mIdle = current.clip;
                        mAnim.Play(mIdle.name);
                    }
                    else if (current.clip.name.StartsWith("idle"))
                    {
                        current.layer = 1;
                        mBreaks.Add(current.clip);
                    }
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
            if (mBreaks.Count == 0)
            {
                Destroy(this);
            }
        }
    }

    private void Update()
    {
        if (mNextBreak < Time.time)
        {
            if (mBreaks.Count == 1)
            {
                var clip = mBreaks[0];
                mNextBreak = (Time.time + clip.length) + UnityEngine.Random.Range(5f, 15f);
                mAnim.CrossFade(clip.name);
            }
            else
            {
                var num = UnityEngine.Random.Range(0, mBreaks.Count - 1);
                if (mLastIndex == num)
                {
                    num++;
                    if (num >= mBreaks.Count)
                    {
                        num = 0;
                    }
                }
                mLastIndex = num;
                var clip2 = mBreaks[num];
                mNextBreak = (Time.time + clip2.length) + UnityEngine.Random.Range(2f, 8f);
                mAnim.CrossFade(clip2.name);
            }
        }
    }
}

