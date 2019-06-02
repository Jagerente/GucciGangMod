using System;
using UnityEngine;

[Serializable]
public class BMSymbol
{
    private int mAdvance;
    private int mHeight;
    private bool mIsValid;
    private int mLength;
    private int mOffsetX;
    private int mOffsetY;
    private UIAtlas.Sprite mSprite;
    private Rect mUV;
    private int mWidth;
    public string sequence;
    public string spriteName;

    public void MarkAsDirty()
    {
        mIsValid = false;
    }

    public bool Validate(UIAtlas atlas)
    {
        if (atlas == null)
        {
            return false;
        }
        if (!mIsValid)
        {
            if (string.IsNullOrEmpty(spriteName))
            {
                return false;
            }
            mSprite = atlas == null ? null : atlas.GetSprite(spriteName);
            if (mSprite != null)
            {
                var texture = atlas.texture;
                if (texture == null)
                {
                    mSprite = null;
                }
                else
                {
                    var outer = mSprite.outer;
                    mUV = outer;
                    if (atlas.coordinates == UIAtlas.Coordinates.Pixels)
                    {
                        mUV = NGUIMath.ConvertToTexCoords(mUV, texture.width, texture.height);
                    }
                    else
                    {
                        outer = NGUIMath.ConvertToPixels(outer, texture.width, texture.height, true);
                    }
                    mOffsetX = Mathf.RoundToInt(mSprite.paddingLeft * outer.width);
                    mOffsetY = Mathf.RoundToInt(mSprite.paddingTop * outer.width);
                    mWidth = Mathf.RoundToInt(outer.width);
                    mHeight = Mathf.RoundToInt(outer.height);
                    mAdvance = Mathf.RoundToInt(outer.width + (mSprite.paddingRight + mSprite.paddingLeft) * outer.width);
                    mIsValid = true;
                }
            }
        }
        return mSprite != null;
    }

    public int advance
    {
        get
        {
            return mAdvance;
        }
    }

    public int height
    {
        get
        {
            return mHeight;
        }
    }

    public int length
    {
        get
        {
            if (mLength == 0)
            {
                mLength = sequence.Length;
            }
            return mLength;
        }
    }

    public int offsetX
    {
        get
        {
            return mOffsetX;
        }
    }

    public int offsetY
    {
        get
        {
            return mOffsetY;
        }
    }

    public Rect uvRect
    {
        get
        {
            return mUV;
        }
    }

    public int width
    {
        get
        {
            return mWidth;
        }
    }
}

