using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BMFont
{
    [HideInInspector, SerializeField]
    private int mBase;

    private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();

    [HideInInspector, SerializeField]
    private int mHeight;

    [HideInInspector, SerializeField]
    private List<BMGlyph> mSaved = new List<BMGlyph>();

    [HideInInspector, SerializeField]
    private int mSize;

    [HideInInspector, SerializeField]
    private string mSpriteName;

    [HideInInspector, SerializeField]
    private int mWidth;

    public void Clear()
    {
        mDict.Clear();
        mSaved.Clear();
    }

    public BMGlyph GetGlyph(int index)
    {
        return GetGlyph(index, false);
    }

    public BMGlyph GetGlyph(int index, bool createIfMissing)
    {
        BMGlyph glyph = null;
        if (mDict.Count == 0)
        {
            var num = 0;
            var count = mSaved.Count;
            while (num < count)
            {
                var glyph2 = mSaved[num];
                mDict.Add(glyph2.index, glyph2);
                num++;
            }
        }
        if (!mDict.TryGetValue(index, out glyph) && createIfMissing)
        {
            glyph = new BMGlyph
            {
                index = index
            };
            mSaved.Add(glyph);
            mDict.Add(index, glyph);
        }
        return glyph;
    }

    public void Trim(int xMin, int yMin, int xMax, int yMax)
    {
        if (isValid)
        {
            var num = 0;
            var count = mSaved.Count;
            while (num < count)
            {
                var glyph = mSaved[num];
                glyph?.Trim(xMin, yMin, xMax, yMax);
                num++;
            }
        }
    }

    public int baseOffset
    {
        get
        {
            return mBase;
        }
        set
        {
            mBase = value;
        }
    }

    public int charSize
    {
        get
        {
            return mSize;
        }
        set
        {
            mSize = value;
        }
    }

    public int glyphCount
    {
        get
        {
            return !isValid ? 0 : mSaved.Count;
        }
    }

    public bool isValid
    {
        get
        {
            return mSaved.Count > 0;
        }
    }

    public string spriteName
    {
        get
        {
            return mSpriteName;
        }
        set
        {
            mSpriteName = value;
        }
    }

    public int texHeight
    {
        get
        {
            return mHeight;
        }
        set
        {
            mHeight = value;
        }
    }

    public int texWidth
    {
        get
        {
            return mWidth;
        }
        set
        {
            mWidth = value;
        }
    }
}