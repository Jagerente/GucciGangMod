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
            foreach (AnimationState animationState in mAnim)
            {
                if (animationState.clip.name == "idle")
                {
                    animationState.layer = 0;
                    mIdle = animationState.clip;
                    mAnim.Play(mIdle.name);
                }
                else if (animationState.clip.name.StartsWith("idle"))
                {
                    animationState.layer = 1;
                    mBreaks.Add(animationState.clip);
                }
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
                mNextBreak = Time.time + clip.length + Random.Range(5f, 15f);
                mAnim.CrossFade(clip.name);
            }
            else
            {
                var num = Random.Range(0, mBreaks.Count - 1);
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
                mNextBreak = Time.time + clip2.length + Random.Range(2f, 8f);
                mAnim.CrossFade(clip2.name);
            }
        }
    }
}