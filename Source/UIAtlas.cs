using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Atlas")]
public class UIAtlas : MonoBehaviour
{
    [SerializeField, HideInInspector] private Material material;
    [HideInInspector, SerializeField] private Coordinates mCoordinates;
    [HideInInspector, SerializeField] private float mPixelSize = 1f;
    private int mPMA = -1;
    [HideInInspector, SerializeField] private UIAtlas mReplacement;
    [SerializeField, HideInInspector] private List<Sprite> sprites = new List<Sprite>();

    public static bool CheckIfRelated(UIAtlas a, UIAtlas b)
    {
        if (a == null || b == null)
        {
            return false;
        }

        return a == b || a.References(b) || b.References(a);
    }

    private static int CompareString(string a, string b)
    {
        return a.CompareTo(b);
    }

    public BetterList<string> GetListOfSprites()
    {
        if (mReplacement != null)
        {
            return mReplacement.GetListOfSprites();
        }

        var list = new BetterList<string>();
        var num = 0;
        var count = sprites.Count;
        while (num < count)
        {
            var sprite = sprites[num];
            if (sprite != null && !string.IsNullOrEmpty(sprite.name))
            {
                list.Add(sprite.name);
            }

            num++;
        }

        return list;
    }

    public BetterList<string> GetListOfSprites(string match)
    {
        if (mReplacement != null)
        {
            return mReplacement.GetListOfSprites(match);
        }

        if (string.IsNullOrEmpty(match))
        {
            return GetListOfSprites();
        }

        var list = new BetterList<string>();
        var num = 0;
        var count = sprites.Count;
        while (num < count)
        {
            var sprite = sprites[num];
            if (sprite != null && !string.IsNullOrEmpty(sprite.name) &&
                string.Equals(match, sprite.name, StringComparison.OrdinalIgnoreCase))
            {
                list.Add(sprite.name);
                return list;
            }

            num++;
        }

        char[] separator = {' '};
        var strArray = match.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < strArray.Length; i++)
        {
            strArray[i] = strArray[i].ToLower();
        }

        var num4 = 0;
        var num5 = sprites.Count;
        while (num4 < num5)
        {
            var sprite2 = sprites[num4];
            if (sprite2 != null && !string.IsNullOrEmpty(sprite2.name))
            {
                var str = sprite2.name.ToLower();
                var num6 = 0;
                for (var j = 0; j < strArray.Length; j++)
                {
                    if (str.Contains(strArray[j]))
                    {
                        num6++;
                    }
                }

                if (num6 == strArray.Length)
                {
                    list.Add(sprite2.name);
                }
            }

            num4++;
        }

        return list;
    }

    public Sprite GetSprite(string name)
    {
        if (mReplacement != null)
        {
            return mReplacement.GetSprite(name);
        }

        if (!string.IsNullOrEmpty(name))
        {
            var num = 0;
            var count = sprites.Count;
            while (num < count)
            {
                var sprite = sprites[num];
                if (!string.IsNullOrEmpty(sprite.name) && name == sprite.name)
                {
                    return sprite;
                }

                num++;
            }
        }

        return null;
    }

    public void MarkAsDirty()
    {
        if (mReplacement != null)
        {
            mReplacement.MarkAsDirty();
        }

        var spriteArray = NGUITools.FindActive<UISprite>();
        var index = 0;
        var length = spriteArray.Length;
        while (index < length)
        {
            var sprite = spriteArray[index];
            if (CheckIfRelated(this, sprite.atlas))
            {
                var atlas = sprite.atlas;
                sprite.atlas = null;
                sprite.atlas = atlas;
            }

            index++;
        }

        var fontArray = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
        var num3 = 0;
        var num4 = fontArray.Length;
        while (num3 < num4)
        {
            var font = fontArray[num3];
            if (CheckIfRelated(this, font.atlas))
            {
                var atlas2 = font.atlas;
                font.atlas = null;
                font.atlas = atlas2;
            }

            num3++;
        }

        var labelArray = NGUITools.FindActive<UILabel>();
        var num5 = 0;
        var num6 = labelArray.Length;
        while (num5 < num6)
        {
            var label = labelArray[num5];
            if (label.font != null && CheckIfRelated(this, label.font.atlas))
            {
                var font2 = label.font;
                label.font = null;
                label.font = font2;
            }

            num5++;
        }
    }

    private bool References(UIAtlas atlas)
    {
        if (atlas == null)
        {
            return false;
        }

        return atlas == this || mReplacement != null && mReplacement.References(atlas);
    }

    public Coordinates coordinates
    {
        get { return mReplacement == null ? mCoordinates : mReplacement.coordinates; }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.coordinates = value;
            }
            else if (mCoordinates != value)
            {
                if (material == null || material.mainTexture == null)
                {
                    Debug.LogError("Can't switch coordinates until the atlas material has a valid texture");
                }
                else
                {
                    mCoordinates = value;
                    var mainTexture = material.mainTexture;
                    var num = 0;
                    var count = sprites.Count;
                    while (num < count)
                    {
                        var sprite = sprites[num];
                        if (mCoordinates == Coordinates.TexCoords)
                        {
                            sprite.outer =
                                NGUIMath.ConvertToTexCoords(sprite.outer, mainTexture.width, mainTexture.height);
                            sprite.inner =
                                NGUIMath.ConvertToTexCoords(sprite.inner, mainTexture.width, mainTexture.height);
                        }
                        else
                        {
                            sprite.outer = NGUIMath.ConvertToPixels(sprite.outer, mainTexture.width, mainTexture.height,
                                true);
                            sprite.inner = NGUIMath.ConvertToPixels(sprite.inner, mainTexture.width, mainTexture.height,
                                true);
                        }

                        num++;
                    }
                }
            }
        }
    }

    public float pixelSize
    {
        get { return mReplacement == null ? mPixelSize : mReplacement.pixelSize; }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.pixelSize = value;
            }
            else
            {
                var num = Mathf.Clamp(value, 0.25f, 4f);
                if (mPixelSize != num)
                {
                    mPixelSize = num;
                    MarkAsDirty();
                }
            }
        }
    }

    public bool premultipliedAlpha
    {
        get
        {
            if (mReplacement != null)
            {
                return mReplacement.premultipliedAlpha;
            }

            if (mPMA == -1)
            {
                var spriteMaterial = this.spriteMaterial;
                mPMA = spriteMaterial == null || spriteMaterial.shader == null ||
                       !spriteMaterial.shader.name.Contains("Premultiplied")
                    ? 0
                    : 1;
            }

            return mPMA == 1;
        }
    }

    public UIAtlas replacement
    {
        get { return mReplacement; }
        set
        {
            var atlas = value;
            if (atlas == this)
            {
                atlas = null;
            }

            if (mReplacement != atlas)
            {
                if (atlas != null && atlas.replacement == this)
                {
                    atlas.replacement = null;
                }

                if (mReplacement != null)
                {
                    MarkAsDirty();
                }

                mReplacement = atlas;
                MarkAsDirty();
            }
        }
    }

    public List<Sprite> spriteList
    {
        get { return mReplacement == null ? sprites : mReplacement.spriteList; }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.spriteList = value;
            }
            else
            {
                sprites = value;
            }
        }
    }

    public Material spriteMaterial
    {
        get { return mReplacement == null ? material : mReplacement.spriteMaterial; }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.spriteMaterial = value;
            }
            else if (material == null)
            {
                mPMA = 0;
                material = value;
            }
            else
            {
                MarkAsDirty();
                mPMA = -1;
                material = value;
                MarkAsDirty();
            }
        }
    }

    public Texture texture
    {
        get { return mReplacement == null ? material == null ? null : material.mainTexture : mReplacement.texture; }
    }

    public enum Coordinates
    {
        Pixels,
        TexCoords
    }

    [Serializable]
    public class Sprite
    {
        public Rect inner = new Rect(0f, 0f, 1f, 1f);
        public string name = "Unity Bug";
        public Rect outer = new Rect(0f, 0f, 1f, 1f);
        public float paddingBottom;
        public float paddingLeft;
        public float paddingRight;
        public float paddingTop;
        public bool rotated;

        public bool hasPadding
        {
            get { return paddingLeft != 0f || paddingRight != 0f || paddingTop != 0f || paddingBottom != 0f; }
        }
    }
}