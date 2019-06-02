using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Sprite Animation"), RequireComponent(typeof(UISprite)), ExecuteInEditMode]
public class UISpriteAnimation : MonoBehaviour
{
    private bool mActive = true;
    private float mDelta;
    [HideInInspector, SerializeField] private int mFPS = 30;
    private int mIndex;
    [SerializeField, HideInInspector] private bool mLoop = true;
    [HideInInspector, SerializeField] private string mPrefix = string.Empty;
    private UISprite mSprite;
    private List<string> mSpriteNames = new List<string>();

    private void RebuildSpriteList()
    {
        if (mSprite == null)
        {
            mSprite = GetComponent<UISprite>();
        }

        mSpriteNames.Clear();
        if (mSprite != null && mSprite.atlas != null)
        {
            var spriteList = mSprite.atlas.spriteList;
            var num = 0;
            var count = spriteList.Count;
            while (num < count)
            {
                var sprite = spriteList[num];
                if (string.IsNullOrEmpty(mPrefix) || sprite.name.StartsWith(mPrefix))
                {
                    mSpriteNames.Add(sprite.name);
                }

                num++;
            }

            mSpriteNames.Sort();
        }
    }

    public void Reset()
    {
        mActive = true;
        mIndex = 0;
        if (mSprite != null && mSpriteNames.Count > 0)
        {
            mSprite.spriteName = mSpriteNames[mIndex];
            mSprite.MakePixelPerfect();
        }
    }

    private void Start()
    {
        RebuildSpriteList();
    }

    private void Update()
    {
        if (mActive && mSpriteNames.Count > 1 && Application.isPlaying && mFPS > 0f)
        {
            mDelta += Time.deltaTime;
            var num = 1f / mFPS;
            if (num < mDelta)
            {
                mDelta = num <= 0f ? 0f : mDelta - num;
                if (++mIndex >= mSpriteNames.Count)
                {
                    mIndex = 0;
                    mActive = loop;
                }

                if (mActive)
                {
                    mSprite.spriteName = mSpriteNames[mIndex];
                    mSprite.MakePixelPerfect();
                }
            }
        }
    }

    public int frames
    {
        get { return mSpriteNames.Count; }
    }

    public int framesPerSecond
    {
        get { return mFPS; }
        set { mFPS = value; }
    }

    public bool isPlaying
    {
        get { return mActive; }
    }

    public bool loop
    {
        get { return mLoop; }
        set { mLoop = value; }
    }

    public string namePrefix
    {
        get { return mPrefix; }
        set
        {
            if (mPrefix != value)
            {
                mPrefix = value;
                RebuildSpriteList();
            }
        }
    }
}