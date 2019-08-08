using System.Collections.Generic;
using System.Text;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Font"), ExecuteInEditMode]
public class UIFont : MonoBehaviour
{
    [HideInInspector, SerializeField]
    private UIAtlas mAtlas;

    private static CharacterInfo mChar;
    private List<Color> mColors = new List<Color>();

    [HideInInspector, SerializeField]
    private Font mDynamicFont;

    [HideInInspector, SerializeField]
    private float mDynamicFontOffset;

    [SerializeField, HideInInspector]
    private int mDynamicFontSize = 16;

    [SerializeField, HideInInspector]
    private FontStyle mDynamicFontStyle;

    [SerializeField, HideInInspector]
    private BMFont mFont = new BMFont();

    [SerializeField, HideInInspector]
    private Material mMat;

    [SerializeField, HideInInspector]
    private float mPixelSize = 1f;

    private int mPMA = -1;

    [HideInInspector, SerializeField]
    private UIFont mReplacement;

    [HideInInspector, SerializeField]
    private int mSpacingX;

    [SerializeField, HideInInspector]
    private int mSpacingY;

    private UIAtlas.Sprite mSprite;
    private bool mSpriteSet;

    [SerializeField, HideInInspector]
    private List<BMSymbol> mSymbols = new List<BMSymbol>();

    [HideInInspector, SerializeField]
    private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

    public void AddSymbol(string sequence, string spriteName)
    {
        GetSymbol(sequence, true).spriteName = spriteName;
        MarkAsDirty();
    }

    private void Align(BetterList<Vector3> verts, int indexOffset, Alignment alignment, int x, int lineWidth)
    {
        if (alignment != Alignment.Left)
        {
            var size = this.size;
            if (size > 0)
            {
                var num2 = 0f;
                if (alignment == Alignment.Right)
                {
                    num2 = Mathf.RoundToInt(lineWidth - x);
                    if (num2 < 0f)
                    {
                        num2 = 0f;
                    }
                    num2 /= this.size;
                }
                else
                {
                    num2 = Mathf.RoundToInt((lineWidth - x) * 0.5f);
                    if (num2 < 0f)
                    {
                        num2 = 0f;
                    }
                    num2 /= this.size;
                    if ((lineWidth & 1) == 1)
                    {
                        num2 += 0.5f / size;
                    }
                }
                for (var i = indexOffset; i < verts.size; i++)
                {
                    var vector = verts.buffer[i];
                    vector.x += num2;
                    verts.buffer[i] = vector;
                }
            }
        }
    }

    public Vector2 CalculatePrintedSize(string text, bool encoding, SymbolStyle symbolStyle)
    {
        if (mReplacement != null)
        {
            return mReplacement.CalculatePrintedSize(text, encoding, symbolStyle);
        }
        var zero = Vector2.zero;
        var isDynamic = this.isDynamic;
        if (isDynamic || mFont != null && mFont.isValid && !string.IsNullOrEmpty(text))
        {
            if (encoding)
            {
                text = NGUITools.StripSymbols(text);
            }
            if (isDynamic)
            {
                mDynamicFont.textureRebuildCallback = OnFontChanged;
                mDynamicFont.RequestCharactersInTexture(text, mDynamicFontSize, mDynamicFontStyle);
                mDynamicFont.textureRebuildCallback = null;
            }
            var length = text.Length;
            var num2 = 0;
            var num3 = 0;
            var num4 = 0;
            var previousChar = 0;
            var size = this.size;
            var num7 = size + mSpacingY;
            var flag2 = encoding && symbolStyle != SymbolStyle.None && hasSymbols;
            for (var i = 0; i < length; i++)
            {
                var index = text[i];
                if (index == '\n')
                {
                    if (num3 > num2)
                    {
                        num2 = num3;
                    }
                    num3 = 0;
                    num4 += num7;
                    previousChar = 0;
                }
                else if (index < ' ')
                {
                    previousChar = 0;
                }
                else if (!isDynamic)
                {
                    var symbol = !flag2 ? null : MatchSymbol(text, i, length);
                    if (symbol == null)
                    {
                        var glyph = mFont.GetGlyph(index);
                        if (glyph != null)
                        {
                            num3 += mSpacingX + (previousChar == 0 ? glyph.advance : glyph.advance + glyph.GetKerning(previousChar));
                            previousChar = index;
                        }
                    }
                    else
                    {
                        num3 += mSpacingX + symbol.width;
                        i += symbol.length - 1;
                        previousChar = 0;
                    }
                }
                else if (mDynamicFont.GetCharacterInfo(index, out mChar, mDynamicFontSize, mDynamicFontStyle))
                {
                    num3 += mSpacingX + (int)mChar.width;
                }
            }
            var num9 = size <= 0 ? 1f : 1f / size;
            zero.x = num9 * (num3 <= num2 ? num2 : num3);
            zero.y = num9 * (num4 + num7);
        }
        return zero;
    }

    public static bool CheckIfRelated(UIFont a, UIFont b)
    {
        if (a == null || b == null)
        {
            return false;
        }
        return a.isDynamic && b.isDynamic && a.dynamicFont.fontNames[0] == b.dynamicFont.fontNames[0] || a == b || a.References(b) || b.References(a);
    }

    private static void EndLine(ref StringBuilder s)
    {
        var num = s.Length - 1;
        if (num > 0 && s[num] == ' ')
        {
            s[num] = '\n';
        }
        else
        {
            s.Append('\n');
        }
    }

    public string GetEndOfLineThatFits(string text, float maxWidth, bool encoding, SymbolStyle symbolStyle)
    {
        if (mReplacement != null)
        {
            return mReplacement.GetEndOfLineThatFits(text, maxWidth, encoding, symbolStyle);
        }
        var num = Mathf.RoundToInt(maxWidth * size);
        if (num < 1)
        {
            return text;
        }
        var length = text.Length;
        var num3 = num;
        BMGlyph glyph = null;
        var offset = length;
        var flag = encoding && symbolStyle != SymbolStyle.None && hasSymbols;
        var isDynamic = this.isDynamic;
        if (isDynamic)
        {
            mDynamicFont.textureRebuildCallback = OnFontChanged;
            mDynamicFont.RequestCharactersInTexture(text, mDynamicFontSize, mDynamicFontStyle);
            mDynamicFont.textureRebuildCallback = null;
        }
        while (offset > 0 && num3 > 0)
        {
            var index = text[--offset];
            var symbol = !flag ? null : MatchSymbol(text, offset, length);
            var mSpacingX = this.mSpacingX;
            if (!isDynamic)
            {
                if (symbol != null)
                {
                    mSpacingX += symbol.advance;
                    goto Label_017F;
                }
                var glyph2 = mFont.GetGlyph(index);
                if (glyph2 != null)
                {
                    mSpacingX += glyph2.advance + (glyph != null ? glyph.GetKerning(index) : 0);
                    glyph = glyph2;
                    goto Label_017F;
                }
                glyph = null;
                continue;
            }
            if (mDynamicFont.GetCharacterInfo(index, out mChar, mDynamicFontSize, mDynamicFontStyle))
            {
                mSpacingX += (int)mChar.width;
            }
            Label_017F:
            num3 -= mSpacingX;
        }
        if (num3 < 0)
        {
            offset++;
        }
        return text.Substring(offset, length - offset);
    }

    private BMSymbol GetSymbol(string sequence, bool createIfMissing)
    {
        var num = 0;
        var count = mSymbols.Count;
        while (num < count)
        {
            var symbol = mSymbols[num];
            if (symbol.sequence == sequence)
            {
                return symbol;
            }
            num++;
        }
        if (createIfMissing)
        {
            var item = new BMSymbol
            {
                sequence = sequence
            };
            mSymbols.Add(item);
            return item;
        }
        return null;
    }

    public void MarkAsDirty()
    {
        if (mReplacement != null)
        {
            mReplacement.MarkAsDirty();
        }
        RecalculateDynamicOffset();
        mSprite = null;
        var labelArray = NGUITools.FindActive<UILabel>();
        var index = 0;
        var length = labelArray.Length;
        while (index < length)
        {
            var label = labelArray[index];
            if (label.enabled && NGUITools.GetActive(label.gameObject) && CheckIfRelated(this, label.font))
            {
                var font = label.font;
                label.font = null;
                label.font = font;
            }
            index++;
        }
        var num3 = 0;
        var count = mSymbols.Count;
        while (num3 < count)
        {
            symbols[num3].MarkAsDirty();
            num3++;
        }
    }

    private BMSymbol MatchSymbol(string text, int offset, int textLength)
    {
        var count = mSymbols.Count;
        if (count != 0)
        {
            textLength -= offset;
            for (var i = 0; i < count; i++)
            {
                var symbol = mSymbols[i];
                var length = symbol.length;
                if (length != 0 && textLength >= length)
                {
                    var flag = true;
                    for (var j = 0; j < length; j++)
                    {
                        if (text[offset + j] != symbol.sequence[j])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag && symbol.Validate(atlas))
                    {
                        return symbol;
                    }
                }
            }
        }
        return null;
    }

    private void OnFontChanged()
    {
        MarkAsDirty();
    }

    public void Print(string text, Color32 color, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, bool encoding, SymbolStyle symbolStyle, Alignment alignment, int lineWidth, bool premultiply)
    {
        if (mReplacement != null)
        {
            mReplacement.Print(text, color, verts, uvs, cols, encoding, symbolStyle, alignment, lineWidth, premultiply);
        }
        else if (text != null)
        {
            if (!isValid)
            {
                Debug.LogError("Attempting to print using an invalid font!");
            }
            else
            {
                var isDynamic = this.isDynamic;
                if (isDynamic)
                {
                    mDynamicFont.textureRebuildCallback = OnFontChanged;
                    mDynamicFont.RequestCharactersInTexture(text, mDynamicFontSize, mDynamicFontStyle);
                    mDynamicFont.textureRebuildCallback = null;
                }
                mColors.Clear();
                mColors.Add(color);
                var size = this.size;
                var vector = size <= 0 ? Vector2.one : new Vector2(1f / size, 1f / size);
                var indexOffset = verts.size;
                var num3 = 0;
                var x = 0;
                var num5 = 0;
                var previousChar = 0;
                var num7 = size + mSpacingY;
                var zero = Vector3.zero;
                var vector3 = Vector3.zero;
                var vector4 = Vector2.zero;
                var vector5 = Vector2.zero;
                var num8 = this.uvRect.width / mFont.texWidth;
                var num9 = mUVRect.height / mFont.texHeight;
                var length = text.Length;
                var flag2 = encoding && symbolStyle != SymbolStyle.None && hasSymbols && sprite != null;
                for (var i = 0; i < length; i++)
                {
                    var index = text[i];
                    if (index == '\n')
                    {
                        if (x > num3)
                        {
                            num3 = x;
                        }
                        if (alignment != Alignment.Left)
                        {
                            Align(verts, indexOffset, alignment, x, lineWidth);
                            indexOffset = verts.size;
                        }
                        x = 0;
                        num5 += num7;
                        previousChar = 0;
                        continue;
                    }
                    if (index < ' ')
                    {
                        previousChar = 0;
                        continue;
                    }
                    if (encoding && index == '[')
                    {
                        var num12 = NGUITools.ParseSymbol(text, i, mColors, premultiply);
                        if (num12 > 0)
                        {
                            color = mColors[mColors.Count - 1];
                            i += num12 - 1;
                            continue;
                        }
                    }
                    if (!isDynamic)
                    {
                        var symbol = !flag2 ? null : MatchSymbol(text, i, length);
                        if (symbol == null)
                        {
                            var glyph = mFont.GetGlyph(index);
                            if (glyph == null)
                            {
                                continue;
                            }
                            if (previousChar != 0)
                            {
                                x += glyph.GetKerning(previousChar);
                            }
                            if (index == ' ')
                            {
                                x += mSpacingX + glyph.advance;
                                previousChar = index;
                                continue;
                            }
                            zero.x = vector.x * (x + glyph.offsetX);
                            zero.y = -vector.y * (num5 + glyph.offsetY);
                            vector3.x = zero.x + vector.x * glyph.width;
                            vector3.y = zero.y - vector.y * glyph.height;
                            vector4.x = mUVRect.xMin + num8 * glyph.x;
                            vector4.y = mUVRect.yMax - num9 * glyph.y;
                            vector5.x = vector4.x + num8 * glyph.width;
                            vector5.y = vector4.y - num9 * glyph.height;
                            x += mSpacingX + glyph.advance;
                            previousChar = index;
                            if (glyph.channel == 0 || glyph.channel == 15)
                            {
                                for (var j = 0; j < 4; j++)
                                {
                                    cols.Add(color);
                                }
                            }
                            else
                            {
                                Color item = color;
                                item = item * 0.49f;
                                switch (glyph.channel)
                                {
                                    case 1:
                                        item.b += 0.51f;
                                        break;

                                    case 2:
                                        item.g += 0.51f;
                                        break;

                                    case 4:
                                        item.r += 0.51f;
                                        break;

                                    case 8:
                                        item.a += 0.51f;
                                        break;
                                }
                                for (var k = 0; k < 4; k++)
                                {
                                    cols.Add(item);
                                }
                            }
                        }
                        else
                        {
                            zero.x = vector.x * (x + symbol.offsetX);
                            zero.y = -vector.y * (num5 + symbol.offsetY);
                            vector3.x = zero.x + vector.x * symbol.width;
                            vector3.y = zero.y - vector.y * symbol.height;
                            var uvRect = symbol.uvRect;
                            vector4.x = uvRect.xMin;
                            vector4.y = uvRect.yMax;
                            vector5.x = uvRect.xMax;
                            vector5.y = uvRect.yMin;
                            x += mSpacingX + symbol.advance;
                            i += symbol.length - 1;
                            previousChar = 0;
                            if (symbolStyle == SymbolStyle.Colored)
                            {
                                for (var m = 0; m < 4; m++)
                                {
                                    cols.Add(color);
                                }
                            }
                            else
                            {
                                Color32 white = Color.white;
                                white.a = color.a;
                                for (var n = 0; n < 4; n++)
                                {
                                    cols.Add(white);
                                }
                            }
                        }
                        verts.Add(new Vector3(vector3.x, zero.y));
                        verts.Add(new Vector3(vector3.x, vector3.y));
                        verts.Add(new Vector3(zero.x, vector3.y));
                        verts.Add(new Vector3(zero.x, zero.y));
                        uvs.Add(new Vector2(vector5.x, vector4.y));
                        uvs.Add(new Vector2(vector5.x, vector5.y));
                        uvs.Add(new Vector2(vector4.x, vector5.y));
                        uvs.Add(new Vector2(vector4.x, vector4.y));
                        continue;
                    }
                    if (mDynamicFont.GetCharacterInfo(index, out mChar, mDynamicFontSize, mDynamicFontStyle))
                    {
                        zero.x = vector.x * (x + mChar.vert.xMin);
                        zero.y = -vector.y * (num5 - mChar.vert.yMax + mDynamicFontOffset);
                        vector3.x = zero.x + vector.x * mChar.vert.width;
                        vector3.y = zero.y - vector.y * mChar.vert.height;
                        vector4.x = mChar.uv.xMin;
                        vector4.y = mChar.uv.yMin;
                        vector5.x = mChar.uv.xMax;
                        vector5.y = mChar.uv.yMax;
                        x += mSpacingX + (int)mChar.width;
                        for (var num18 = 0; num18 < 4; num18++)
                        {
                            cols.Add(color);
                        }
                        if (mChar.flipped)
                        {
                            uvs.Add(new Vector2(vector4.x, vector5.y));
                            uvs.Add(new Vector2(vector4.x, vector4.y));
                            uvs.Add(new Vector2(vector5.x, vector4.y));
                            uvs.Add(new Vector2(vector5.x, vector5.y));
                        }
                        else
                        {
                            uvs.Add(new Vector2(vector5.x, vector4.y));
                            uvs.Add(new Vector2(vector4.x, vector4.y));
                            uvs.Add(new Vector2(vector4.x, vector5.y));
                            uvs.Add(new Vector2(vector5.x, vector5.y));
                        }
                        verts.Add(new Vector3(vector3.x, zero.y));
                        verts.Add(new Vector3(zero.x, zero.y));
                        verts.Add(new Vector3(zero.x, vector3.y));
                        verts.Add(new Vector3(vector3.x, vector3.y));
                    }
                }
                if (alignment != Alignment.Left && indexOffset < verts.size)
                {
                    Align(verts, indexOffset, alignment, x, lineWidth);
                    indexOffset = verts.size;
                }
            }
        }
    }

    public bool RecalculateDynamicOffset()
    {
        if (mDynamicFont != null)
        {
            CharacterInfo info;
            mDynamicFont.RequestCharactersInTexture("j", mDynamicFontSize, mDynamicFontStyle);
            mDynamicFont.GetCharacterInfo('j', out info, mDynamicFontSize, mDynamicFontStyle);
            var objB = mDynamicFontSize + info.vert.yMax;
            if (!Equals(mDynamicFontOffset, objB))
            {
                mDynamicFontOffset = objB;
                return true;
            }
        }
        return false;
    }

    private bool References(UIFont font)
    {
        if (font == null)
        {
            return false;
        }
        return font == this || mReplacement != null && mReplacement.References(font);
    }

    public void RemoveSymbol(string sequence)
    {
        var item = GetSymbol(sequence, false);
        if (item != null)
        {
            symbols.Remove(item);
        }
        MarkAsDirty();
    }

    public void RenameSymbol(string before, string after)
    {
        var symbol = GetSymbol(before, false);
        if (symbol != null)
        {
            symbol.sequence = after;
        }
        MarkAsDirty();
    }

    private void Trim()
    {
        var texture = mAtlas.texture;
        if (texture != null && mSprite != null)
        {
            var rect = NGUIMath.ConvertToPixels(mUVRect, this.texture.width, this.texture.height, true);
            var rect2 = mAtlas.coordinates != UIAtlas.Coordinates.TexCoords ? mSprite.outer : NGUIMath.ConvertToPixels(mSprite.outer, texture.width, texture.height, true);
            var xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
            var yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
            var xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
            var yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
            mFont.Trim(xMin, yMin, xMax, yMax);
        }
    }

    public bool UsesSprite(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            if (s.Equals(spriteName))
            {
                return true;
            }
            var num = 0;
            var count = symbols.Count;
            while (num < count)
            {
                var symbol = symbols[num];
                if (s.Equals(symbol.spriteName))
                {
                    return true;
                }
                num++;
            }
        }
        return false;
    }

    public string WrapText(string text, float maxWidth, int maxLineCount)
    {
        return WrapText(text, maxWidth, maxLineCount, false, SymbolStyle.None);
    }

    public string WrapText(string text, float maxWidth, int maxLineCount, bool encoding)
    {
        return WrapText(text, maxWidth, maxLineCount, encoding, SymbolStyle.None);
    }

    public string WrapText(string text, float maxWidth, int maxLineCount, bool encoding, SymbolStyle symbolStyle)
    {
        if (mReplacement != null)
        {
            return mReplacement.WrapText(text, maxWidth, maxLineCount, encoding, symbolStyle);
        }
        var num = Mathf.RoundToInt(maxWidth * size);
        if (num < 1)
        {
            return text;
        }
        var s = new StringBuilder();
        var length = text.Length;
        var num3 = num;
        var previousChar = 0;
        var startIndex = 0;
        var offset = 0;
        var flag = true;
        var flag2 = maxLineCount != 1;
        var num7 = 1;
        var flag3 = encoding && symbolStyle != SymbolStyle.None && hasSymbols;
        var isDynamic = this.isDynamic;
        if (isDynamic)
        {
            mDynamicFont.textureRebuildCallback = OnFontChanged;
            mDynamicFont.RequestCharactersInTexture(text, mDynamicFontSize, mDynamicFontStyle);
            mDynamicFont.textureRebuildCallback = null;
        }
        while (offset < length)
        {
            var ch = text[offset];
            if (ch == '\n')
            {
                if (!flag2 || num7 == maxLineCount)
                {
                    break;
                }
                num3 = num;
                if (startIndex < offset)
                {
                    s.Append(text.Substring(startIndex, offset - startIndex + 1));
                }
                else
                {
                    s.Append(ch);
                }
                flag = true;
                num7++;
                startIndex = offset + 1;
                previousChar = 0;
                goto Label_03E7;
            }
            if (ch == ' ' && previousChar != 32 && startIndex < offset)
            {
                s.Append(text.Substring(startIndex, offset - startIndex + 1));
                flag = false;
                startIndex = offset + 1;
                previousChar = ch;
            }
            if (encoding && ch == '[' && offset + 2 < length)
            {
                if (text[offset + 1] == '-' && text[offset + 2] == ']')
                {
                    offset += 2;
                    goto Label_03E7;
                }
                if (offset + 7 < length && text[offset + 7] == ']' && NGUITools.EncodeColor(NGUITools.ParseColor(text, offset + 1)) == text.Substring(offset + 1, 6).ToUpper())
                {
                    offset += 7;
                    goto Label_03E7;
                }
            }
            var symbol = !flag3 ? null : MatchSymbol(text, offset, length);
            var mSpacingX = this.mSpacingX;
            if (!isDynamic)
            {
                if (symbol != null)
                {
                    mSpacingX += symbol.advance;
                }
                else
                {
                    var glyph = symbol != null ? null : mFont.GetGlyph(ch);
                    if (glyph == null)
                    {
                        goto Label_03E7;
                    }
                    mSpacingX += previousChar == 0 ? glyph.advance : glyph.advance + glyph.GetKerning(previousChar);
                }
            }
            else if (mDynamicFont.GetCharacterInfo(ch, out mChar, mDynamicFontSize, mDynamicFontStyle))
            {
                mSpacingX += Mathf.RoundToInt(mChar.width);
            }
            num3 -= mSpacingX;
            if (num3 < 0)
            {
                if (flag || !flag2 || num7 == maxLineCount)
                {
                    s.Append(text.Substring(startIndex, Mathf.Max(0, offset - startIndex)));
                    if (!flag2 || num7 == maxLineCount)
                    {
                        startIndex = offset;
                        break;
                    }
                    EndLine(ref s);
                    flag = true;
                    num7++;
                    if (ch == ' ')
                    {
                        startIndex = offset + 1;
                        num3 = num;
                    }
                    else
                    {
                        startIndex = offset;
                        num3 = num - mSpacingX;
                    }
                    previousChar = 0;
                    goto Label_03C8;
                }
                while (startIndex < length && text[startIndex] == ' ')
                {
                    startIndex++;
                }
                flag = true;
                num3 = num;
                offset = startIndex - 1;
                previousChar = 0;
                if (!flag2 || num7 == maxLineCount)
                {
                    break;
                }
                num7++;
                EndLine(ref s);
                goto Label_03E7;
            }
            previousChar = ch;
            Label_03C8:
            if (!isDynamic && symbol != null)
            {
                offset += symbol.length - 1;
                previousChar = 0;
            }
            Label_03E7:
            offset++;
        }
        if (startIndex < offset)
        {
            s.Append(text.Substring(startIndex, offset - startIndex));
        }
        return s.ToString();
    }

    public UIAtlas atlas
    {
        get
        {
            return mReplacement == null ? mAtlas : mReplacement.atlas;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.atlas = value;
            }
            else if (mAtlas != value)
            {
                if (value == null)
                {
                    if (mAtlas != null)
                    {
                        mMat = mAtlas.spriteMaterial;
                    }
                    if (sprite != null)
                    {
                        mUVRect = uvRect;
                    }
                }
                mPMA = -1;
                mAtlas = value;
                MarkAsDirty();
            }
        }
    }

    public BMFont bmFont
    {
        get
        {
            return mReplacement == null ? mFont : mReplacement.bmFont;
        }
    }

    public Font dynamicFont
    {
        get
        {
            return mReplacement == null ? mDynamicFont : mReplacement.dynamicFont;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.dynamicFont = value;
            }
            else if (mDynamicFont != value)
            {
                if (mDynamicFont != null)
                {
                    material = null;
                }
                mDynamicFont = value;
                MarkAsDirty();
            }
        }
    }

    public int dynamicFontSize
    {
        get
        {
            return mReplacement == null ? mDynamicFontSize : mReplacement.dynamicFontSize;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.dynamicFontSize = value;
            }
            else
            {
                value = Mathf.Clamp(value, 4, 128);
                if (mDynamicFontSize != value)
                {
                    mDynamicFontSize = value;
                    MarkAsDirty();
                }
            }
        }
    }

    public FontStyle dynamicFontStyle
    {
        get
        {
            return mReplacement == null ? mDynamicFontStyle : mReplacement.dynamicFontStyle;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.dynamicFontStyle = value;
            }
            else if (mDynamicFontStyle != value)
            {
                mDynamicFontStyle = value;
                MarkAsDirty();
            }
        }
    }

    private Texture dynamicTexture
    {
        get
        {
            if (mReplacement != null)
            {
                return mReplacement.dynamicTexture;
            }
            if (isDynamic)
            {
                return mDynamicFont.material.mainTexture;
            }
            return null;
        }
    }

    public bool hasSymbols
    {
        get
        {
            return mReplacement == null ? mSymbols.Count != 0 : mReplacement.hasSymbols;
        }
    }

    public int horizontalSpacing
    {
        get
        {
            return mReplacement == null ? mSpacingX : mReplacement.horizontalSpacing;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.horizontalSpacing = value;
            }
            else if (mSpacingX != value)
            {
                mSpacingX = value;
                MarkAsDirty();
            }
        }
    }

    public bool isDynamic
    {
        get
        {
            return mDynamicFont != null;
        }
    }

    public bool isValid
    {
        get
        {
            return mDynamicFont != null || mFont.isValid;
        }
    }

    public Material material
    {
        get
        {
            if (mReplacement != null)
            {
                return mReplacement.material;
            }
            if (mAtlas != null)
            {
                return mAtlas.spriteMaterial;
            }
            if (mMat != null)
            {
                if (mDynamicFont != null && mMat != mDynamicFont.material)
                {
                    mMat.mainTexture = mDynamicFont.material.mainTexture;
                }
                return mMat;
            }
            if (mDynamicFont != null)
            {
                return mDynamicFont.material;
            }
            return null;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.material = value;
            }
            else if (mMat != value)
            {
                mPMA = -1;
                mMat = value;
                MarkAsDirty();
            }
        }
    }

    public float pixelSize
    {
        get
        {
            if (mReplacement != null)
            {
                return mReplacement.pixelSize;
            }
            if (mAtlas != null)
            {
                return mAtlas.pixelSize;
            }
            return mPixelSize;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.pixelSize = value;
            }
            else if (mAtlas != null)
            {
                mAtlas.pixelSize = value;
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
            if (mAtlas != null)
            {
                return mAtlas.premultipliedAlpha;
            }
            if (mPMA == -1)
            {
                var material = this.material;
                mPMA = material == null || material.shader == null || !material.shader.name.Contains("Premultiplied") ? 0 : 1;
            }
            return mPMA == 1;
        }
    }

    public UIFont replacement
    {
        get
        {
            return mReplacement;
        }
        set
        {
            var font = value;
            if (font == this)
            {
                font = null;
            }
            if (mReplacement != font)
            {
                if (font != null && font.replacement == this)
                {
                    font.replacement = null;
                }
                if (mReplacement != null)
                {
                    MarkAsDirty();
                }
                mReplacement = font;
                MarkAsDirty();
            }
        }
    }

    public int size
    {
        get
        {
            return mReplacement == null ? !isDynamic ? mFont.charSize : mDynamicFontSize : mReplacement.size;
        }
    }

    public UIAtlas.Sprite sprite
    {
        get
        {
            if (mReplacement != null)
            {
                return mReplacement.sprite;
            }
            if (!mSpriteSet)
            {
                mSprite = null;
            }
            if (mSprite == null)
            {
                if (mAtlas != null && !string.IsNullOrEmpty(mFont.spriteName))
                {
                    mSprite = mAtlas.GetSprite(mFont.spriteName);
                    if (mSprite == null)
                    {
                        mSprite = mAtlas.GetSprite(name);
                    }
                    mSpriteSet = true;
                    if (mSprite == null)
                    {
                        mFont.spriteName = null;
                    }
                }
                var num = 0;
                var count = mSymbols.Count;
                while (num < count)
                {
                    symbols[num].MarkAsDirty();
                    num++;
                }
            }
            return mSprite;
        }
    }

    public string spriteName
    {
        get
        {
            return mReplacement == null ? mFont.spriteName : mReplacement.spriteName;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.spriteName = value;
            }
            else if (mFont.spriteName != value)
            {
                mFont.spriteName = value;
                MarkAsDirty();
            }
        }
    }

    public List<BMSymbol> symbols
    {
        get
        {
            return mReplacement == null ? mSymbols : mReplacement.symbols;
        }
    }

    public int texHeight
    {
        get
        {
            return mReplacement == null ? mFont == null ? 1 : mFont.texHeight : mReplacement.texHeight;
        }
    }

    public Texture2D texture
    {
        get
        {
            if (mReplacement != null)
            {
                return mReplacement.texture;
            }
            var material = this.material;
            return material == null ? null : material.mainTexture as Texture2D;
        }
    }

    public int texWidth
    {
        get
        {
            return mReplacement == null ? mFont == null ? 1 : mFont.texWidth : mReplacement.texWidth;
        }
    }

    public Rect uvRect
    {
        get
        {
            if (mReplacement != null)
            {
                return mReplacement.uvRect;
            }
            if (mAtlas != null && mSprite == null && sprite != null)
            {
                var texture = mAtlas.texture;
                if (texture != null)
                {
                    this.mUVRect = mSprite.outer;
                    if (mAtlas.coordinates == UIAtlas.Coordinates.Pixels)
                    {
                        mUVRect = NGUIMath.ConvertToTexCoords(mUVRect, texture.width, texture.height);
                    }
                    if (mSprite.hasPadding)
                    {
                        var mUVRect = this.mUVRect;
                        this.mUVRect.xMin = mUVRect.xMin - mSprite.paddingLeft * mUVRect.width;
                        this.mUVRect.yMin = mUVRect.yMin - mSprite.paddingBottom * mUVRect.height;
                        this.mUVRect.xMax = mUVRect.xMax + mSprite.paddingRight * mUVRect.width;
                        this.mUVRect.yMax = mUVRect.yMax + mSprite.paddingTop * mUVRect.height;
                    }
                    if (mSprite.hasPadding)
                    {
                        Trim();
                    }
                }
            }
            return this.mUVRect;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.uvRect = value;
            }
            else if (sprite == null && mUVRect != value)
            {
                mUVRect = value;
                MarkAsDirty();
            }
        }
    }

    public int verticalSpacing
    {
        get
        {
            return mReplacement == null ? mSpacingY : mReplacement.verticalSpacing;
        }
        set
        {
            if (mReplacement != null)
            {
                mReplacement.verticalSpacing = value;
            }
            else if (mSpacingY != value)
            {
                mSpacingY = value;
                MarkAsDirty();
            }
        }
    }

    public enum Alignment
    {
        Left,
        Center,
        Right
    }

    public enum SymbolStyle
    {
        None,
        Uncolored,
        Colored
    }
}