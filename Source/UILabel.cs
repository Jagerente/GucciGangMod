using UnityEngine;

[AddComponentMenu("NGUI/UI/Label"), ExecuteInEditMode]
public class UILabel : UIWidget
{
    [HideInInspector, SerializeField]
    private Color mEffectColor = Color.black;
    [HideInInspector, SerializeField]
    private Vector2 mEffectDistance = Vector2.one;
    [HideInInspector, SerializeField]
    private Effect mEffectStyle;
    [HideInInspector, SerializeField]
    private bool mEncoding = true;
    [HideInInspector, SerializeField]
    private UIFont mFont;
    private int mLastCount;
    private Effect mLastEffect;
    private bool mLastEncoding = true;
    private bool mLastPass;
    private Vector3 mLastScale = Vector3.one;
    private bool mLastShow;
    private string mLastText = string.Empty;
    private int mLastWidth;
    [HideInInspector, SerializeField]
    private float mLineWidth;
    [HideInInspector, SerializeField]
    private int mMaxLineCount;
    [SerializeField, HideInInspector]
    private int mMaxLineWidth;
    [HideInInspector, SerializeField]
    private bool mMultiline = true;
    [HideInInspector, SerializeField]
    private bool mPassword;
    private bool mPremultiply;
    private string mProcessedText;
    private bool mShouldBeProcessed = true;
    [HideInInspector, SerializeField]
    private bool mShowLastChar;
    [HideInInspector, SerializeField]
    private bool mShrinkToFit;
    private Vector2 mSize = Vector2.zero;
    [SerializeField, HideInInspector]
    private UIFont.SymbolStyle mSymbols = UIFont.SymbolStyle.Uncolored;
    [SerializeField, HideInInspector]
    private string mText = string.Empty;

    private void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
    {
        var mEffectColor = this.mEffectColor;
        mEffectColor.a *= alpha * mPanel.alpha;
        Color32 color2 = !font.premultipliedAlpha ? mEffectColor : NGUITools.ApplyPMA(mEffectColor);
        for (var i = start; i < end; i++)
        {
            verts.Add(verts.buffer[i]);
            uvs.Add(uvs.buffer[i]);
            cols.Add(cols.buffer[i]);
            var vector = verts.buffer[i];
            vector.x += x;
            vector.y += y;
            verts.buffer[i] = vector;
            cols.buffer[i] = color2;
        }
    }

    public override void MakePixelPerfect()
    {
        if (mFont != null)
        {
            var pixelSize = font.pixelSize;
            var localScale = cachedTransform.localScale;
            localScale.x = mFont.size * pixelSize;
            localScale.y = localScale.x;
            localScale.z = 1f;
            var localPosition = cachedTransform.localPosition;
            localPosition.x = Mathf.CeilToInt(localPosition.x / pixelSize * 4f) >> 2;
            localPosition.y = Mathf.CeilToInt(localPosition.y / pixelSize * 4f) >> 2;
            localPosition.z = Mathf.RoundToInt(localPosition.z);
            localPosition.x *= pixelSize;
            localPosition.y *= pixelSize;
            cachedTransform.localPosition = localPosition;
            cachedTransform.localScale = localScale;
            if (shrinkToFit)
            {
                ProcessText();
            }
        }
        else
        {
            base.MakePixelPerfect();
        }
    }

    public override void MarkAsChanged()
    {
        hasChanged = true;
        base.MarkAsChanged();
    }

    public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        if (mFont != null)
        {
            var pivot = this.pivot;
            var size = verts.size;
            var c = color;
            c.a *= mPanel.alpha;
            if (font.premultipliedAlpha)
            {
                c = NGUITools.ApplyPMA(c);
            }
            switch (pivot)
            {
                case Pivot.Left:
                case Pivot.TopLeft:
                case Pivot.BottomLeft:
                    mFont.Print(processedText, c, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Left, 0, mPremultiply);
                    break;

                case Pivot.Right:
                case Pivot.TopRight:
                case Pivot.BottomRight:
                    mFont.Print(processedText, c, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Right, Mathf.RoundToInt(relativeSize.x * mFont.size), mPremultiply);
                    break;

                default:
                    mFont.Print(processedText, c, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Center, Mathf.RoundToInt(relativeSize.x * mFont.size), mPremultiply);
                    break;
            }
            if (effectStyle != Effect.None)
            {
                var end = verts.size;
                var num3 = 1f / mFont.size;
                var x = num3 * mEffectDistance.x;
                var y = num3 * mEffectDistance.y;
                ApplyShadow(verts, uvs, cols, size, end, x, -y);
                if (effectStyle == Effect.Outline)
                {
                    size = end;
                    end = verts.size;
                    ApplyShadow(verts, uvs, cols, size, end, -x, y);
                    size = end;
                    end = verts.size;
                    ApplyShadow(verts, uvs, cols, size, end, x, y);
                    size = end;
                    end = verts.size;
                    ApplyShadow(verts, uvs, cols, size, end, -x, -y);
                }
            }
        }
    }

    protected override void OnStart()
    {
        if (mLineWidth > 0f)
        {
            mMaxLineWidth = Mathf.RoundToInt(mLineWidth);
            mLineWidth = 0f;
        }
        if (!mMultiline)
        {
            mMaxLineCount = 1;
            mMultiline = true;
        }
        mPremultiply = font != null && font.material != null && font.material.shader.name.Contains("Premultiplied");
    }

    private void ProcessText()
    {
        mChanged = true;
        hasChanged = false;
        mLastText = mText;
        var b = Mathf.Abs(cachedTransform.localScale.x);
        float num2 = mFont.size * mMaxLineCount;
        if (b <= 0f)
        {
            mSize.x = 1f;
            b = mFont.size;
            cachedTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
            mProcessedText = string.Empty;
            goto Label_037C;
        }
    Label_0057:
        if (mPassword)
        {
            mProcessedText = string.Empty;
            if (mShowLastChar)
            {
                var num3 = 0;
                var num4 = mText.Length - 1;
                while (num3 < num4)
                {
                    mProcessedText = mProcessedText + "*";
                    num3++;
                }
                if (mText.Length > 0)
                {
                    mProcessedText = mProcessedText + mText[mText.Length - 1];
                }
            }
            else
            {
                var num5 = 0;
                var length = mText.Length;
                while (num5 < length)
                {
                    mProcessedText = mProcessedText + "*";
                    num5++;
                }
            }
            mProcessedText = mFont.WrapText(mProcessedText, mMaxLineWidth / b, mMaxLineCount, false, UIFont.SymbolStyle.None);
        }
        else if (mMaxLineWidth > 0)
        {
            mProcessedText = mFont.WrapText(mText, mMaxLineWidth / b, !mShrinkToFit ? mMaxLineCount : 0, mEncoding, mSymbols);
        }
        else if (!mShrinkToFit && mMaxLineCount > 0)
        {
            mProcessedText = mFont.WrapText(mText, 100000f, mMaxLineCount, mEncoding, mSymbols);
        }
        else
        {
            mProcessedText = mText;
        }
        mSize = string.IsNullOrEmpty(mProcessedText) ? Vector2.one : mFont.CalculatePrintedSize(mProcessedText, mEncoding, mSymbols);
        if (mShrinkToFit)
        {
            if (mMaxLineCount > 0 && mSize.y * b > num2)
            {
                b = Mathf.Round(b - 1f);
                if (b > 1f)
                {
                    goto Label_0057;
                }
            }
            if (mMaxLineWidth > 0)
            {
                var num7 = mMaxLineWidth / b;
                var a = mSize.x * b <= num7 ? b : num7 / mSize.x * b;
                b = Mathf.Min(a, b);
            }
            b = Mathf.Round(b);
            cachedTransform.localScale = new Vector3(b, b, 1f);
        }
        mSize.x = Mathf.Max(mSize.x, b <= 0f ? 1f : lineWidth / b);
    Label_037C:
        mSize.y = Mathf.Max(mSize.y, 1f);
    }

    public Color effectColor
    {
        get
        {
            return mEffectColor;
        }
        set
        {
            if (!mEffectColor.Equals(value))
            {
                mEffectColor = value;
                if (mEffectStyle != Effect.None)
                {
                    hasChanged = true;
                }
            }
        }
    }

    public Vector2 effectDistance
    {
        get
        {
            return mEffectDistance;
        }
        set
        {
            if (mEffectDistance != value)
            {
                mEffectDistance = value;
                hasChanged = true;
            }
        }
    }

    public Effect effectStyle
    {
        get
        {
            return mEffectStyle;
        }
        set
        {
            if (mEffectStyle != value)
            {
                mEffectStyle = value;
                hasChanged = true;
            }
        }
    }

    public UIFont font
    {
        get
        {
            return mFont;
        }
        set
        {
            if (mFont != value)
            {
                mFont = value;
                material = mFont == null ? null : mFont.material;
                mChanged = true;
                hasChanged = true;
                MarkAsChanged();
            }
        }
    }

    private bool hasChanged
    {
        get
        {
            return mShouldBeProcessed || mLastText != text || mLastWidth != mMaxLineWidth || mLastEncoding != mEncoding || mLastCount != mMaxLineCount || mLastPass != mPassword || mLastShow != mShowLastChar || mLastEffect != mEffectStyle;
        }
        set
        {
            if (value)
            {
                mChanged = true;
                mShouldBeProcessed = true;
            }
            else
            {
                mShouldBeProcessed = false;
                mLastText = text;
                mLastWidth = mMaxLineWidth;
                mLastEncoding = mEncoding;
                mLastCount = mMaxLineCount;
                mLastPass = mPassword;
                mLastShow = mShowLastChar;
                mLastEffect = mEffectStyle;
            }
        }
    }

    public int lineWidth
    {
        get
        {
            return mMaxLineWidth;
        }
        set
        {
            if (mMaxLineWidth != value)
            {
                mMaxLineWidth = value;
                hasChanged = true;
                if (shrinkToFit)
                {
                    MakePixelPerfect();
                }
            }
        }
    }

    public override Material material
    {
        get
        {
            var material = base.material;
            if (material == null)
            {
                material = mFont == null ? null : mFont.material;
                this.material = material;
            }
            return material;
        }
    }

    public int maxLineCount
    {
        get
        {
            return mMaxLineCount;
        }
        set
        {
            if (mMaxLineCount != value)
            {
                mMaxLineCount = Mathf.Max(value, 0);
                hasChanged = true;
                if (value == 1)
                {
                    mPassword = false;
                }
            }
        }
    }

    public bool multiLine
    {
        get
        {
            return mMaxLineCount != 1;
        }
        set
        {
            if (mMaxLineCount != 1 != value)
            {
                mMaxLineCount = !value ? 1 : 0;
                hasChanged = true;
                if (value)
                {
                    mPassword = false;
                }
            }
        }
    }

    public bool password
    {
        get
        {
            return mPassword;
        }
        set
        {
            if (mPassword != value)
            {
                if (value)
                {
                    mMaxLineCount = 1;
                    mEncoding = false;
                }
                mPassword = value;
                hasChanged = true;
            }
        }
    }

    public string processedText
    {
        get
        {
            if (mLastScale != cachedTransform.localScale)
            {
                mLastScale = cachedTransform.localScale;
                mShouldBeProcessed = true;
            }
            if (hasChanged)
            {
                ProcessText();
            }
            return mProcessedText;
        }
    }

    public override Vector2 relativeSize
    {
        get
        {
            if (mFont == null)
            {
                return Vector3.one;
            }
            if (hasChanged)
            {
                ProcessText();
            }
            return mSize;
        }
    }

    public bool showLastPasswordChar
    {
        get
        {
            return mShowLastChar;
        }
        set
        {
            if (mShowLastChar != value)
            {
                mShowLastChar = value;
                hasChanged = true;
            }
        }
    }

    public bool shrinkToFit
    {
        get
        {
            return mShrinkToFit;
        }
        set
        {
            if (mShrinkToFit != value)
            {
                mShrinkToFit = value;
                hasChanged = true;
            }
        }
    }

    public bool supportEncoding
    {
        get
        {
            return mEncoding;
        }
        set
        {
            if (mEncoding != value)
            {
                mEncoding = value;
                hasChanged = true;
                if (value)
                {
                    mPassword = false;
                }
            }
        }
    }

    public UIFont.SymbolStyle symbolStyle
    {
        get
        {
            return mSymbols;
        }
        set
        {
            if (mSymbols != value)
            {
                mSymbols = value;
                hasChanged = true;
            }
        }
    }

    public string text
    {
        get
        {
            return mText;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(mText))
                {
                    mText = string.Empty;
                }
                hasChanged = true;
            }
            else if (mText != value)
            {
                mText = value;
                hasChanged = true;
                if (shrinkToFit)
                {
                    MakePixelPerfect();
                }
            }
        }
    }

    public enum Effect
    {
        None,
        Shadow,
        Outline
    }
}

