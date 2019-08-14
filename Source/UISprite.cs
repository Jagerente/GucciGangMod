using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("NGUI/UI/Sprite")]
public class UISprite : UIWidget
{
    [SerializeField, HideInInspector] private UIAtlas mAtlas;
    [SerializeField, HideInInspector] private float mFillAmount = 1f;
    [HideInInspector, SerializeField] private bool mFillCenter = true;
    [SerializeField, HideInInspector] private FillDirection mFillDirection = FillDirection.Radial360;
    protected Rect mInner;
    protected Rect mInnerUV;
    [SerializeField, HideInInspector] private bool mInvert;
    protected Rect mOuter;
    protected Rect mOuterUV;
    protected Vector3 mScale = Vector3.one;
    protected UIAtlas.Sprite mSprite;
    [SerializeField, HideInInspector] private string mSpriteName;
    private bool mSpriteSet;
    [HideInInspector, SerializeField] private Type mType;

    protected bool AdjustRadial(Vector2[] xy, Vector2[] uv, float fill, bool invert)
    {
        if (fill < 0.001f)
        {
            return false;
        }

        if (invert || fill <= 0.999f)
        {
            var f = Mathf.Clamp01(fill);
            if (!invert)
            {
                f = 1f - f;
            }

            f *= 1.570796f;
            var t = Mathf.Sin(f);
            var num3 = Mathf.Cos(f);
            if (t > num3)
            {
                num3 *= 1f / t;
                t = 1f;
                if (!invert)
                {
                    xy[0].y = Mathf.Lerp(xy[2].y, xy[0].y, num3);
                    xy[3].y = xy[0].y;
                    uv[0].y = Mathf.Lerp(uv[2].y, uv[0].y, num3);
                    uv[3].y = uv[0].y;
                }
            }
            else if (num3 > t)
            {
                t *= 1f / num3;
                num3 = 1f;
                if (invert)
                {
                    xy[0].x = Mathf.Lerp(xy[2].x, xy[0].x, t);
                    xy[1].x = xy[0].x;
                    uv[0].x = Mathf.Lerp(uv[2].x, uv[0].x, t);
                    uv[1].x = uv[0].x;
                }
            }
            else
            {
                t = 1f;
                num3 = 1f;
            }

            if (invert)
            {
                xy[1].y = Mathf.Lerp(xy[2].y, xy[0].y, num3);
                uv[1].y = Mathf.Lerp(uv[2].y, uv[0].y, num3);
            }
            else
            {
                xy[3].x = Mathf.Lerp(xy[2].x, xy[0].x, t);
                uv[3].x = Mathf.Lerp(uv[2].x, uv[0].x, t);
            }
        }

        return true;
    }

    protected void FilledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        var x = 0f;
        var y = 0f;
        var num3 = 1f;
        var num4 = -1f;
        var xMin = mOuterUV.xMin;
        var yMin = mOuterUV.yMin;
        var xMax = mOuterUV.xMax;
        var yMax = mOuterUV.yMax;
        if (mFillDirection == FillDirection.Horizontal || mFillDirection == FillDirection.Vertical)
        {
            var num9 = (xMax - xMin) * mFillAmount;
            var num10 = (yMax - yMin) * mFillAmount;
            if (fillDirection == FillDirection.Horizontal)
            {
                if (mInvert)
                {
                    x = 1f - mFillAmount;
                    xMin = xMax - num9;
                }
                else
                {
                    num3 *= mFillAmount;
                    xMax = xMin + num9;
                }
            }
            else if (fillDirection == FillDirection.Vertical)
            {
                if (mInvert)
                {
                    num4 *= mFillAmount;
                    yMin = yMax - num10;
                }
                else
                {
                    y = -(1f - mFillAmount);
                    yMax = yMin + num10;
                }
            }
        }

        var xy = new Vector2[4];
        var uv = new Vector2[4];
        xy[0] = new Vector2(num3, y);
        xy[1] = new Vector2(num3, num4);
        xy[2] = new Vector2(x, num4);
        xy[3] = new Vector2(x, y);
        uv[0] = new Vector2(xMax, yMax);
        uv[1] = new Vector2(xMax, yMin);
        uv[2] = new Vector2(xMin, yMin);
        uv[3] = new Vector2(xMin, yMax);
        var c = color;
        c.a *= mPanel.alpha;
        Color32 item = !atlas.premultipliedAlpha ? c : NGUITools.ApplyPMA(c);
        if (fillDirection == FillDirection.Radial90)
        {
            if (!AdjustRadial(xy, uv, mFillAmount, mInvert))
            {
                return;
            }
        }
        else
        {
            if (fillDirection == FillDirection.Radial180)
            {
                var v = new Vector2[4];
                var vectorArray4 = new Vector2[4];
                for (var j = 0; j < 2; j++)
                {
                    float num12;
                    float num13;
                    v[0] = new Vector2(0f, 0f);
                    v[1] = new Vector2(0f, 1f);
                    v[2] = new Vector2(1f, 1f);
                    v[3] = new Vector2(1f, 0f);
                    vectorArray4[0] = new Vector2(0f, 0f);
                    vectorArray4[1] = new Vector2(0f, 1f);
                    vectorArray4[2] = new Vector2(1f, 1f);
                    vectorArray4[3] = new Vector2(1f, 0f);
                    if (mInvert)
                    {
                        if (j > 0)
                        {
                            Rotate(v, j);
                            Rotate(vectorArray4, j);
                        }
                    }
                    else if (j < 1)
                    {
                        Rotate(v, 1 - j);
                        Rotate(vectorArray4, 1 - j);
                    }

                    if (j == 1)
                    {
                        num12 = !mInvert ? 1f : 0.5f;
                        num13 = !mInvert ? 0.5f : 1f;
                    }
                    else
                    {
                        num12 = !mInvert ? 0.5f : 1f;
                        num13 = !mInvert ? 1f : 0.5f;
                    }

                    v[1].y = Mathf.Lerp(num12, num13, v[1].y);
                    v[2].y = Mathf.Lerp(num12, num13, v[2].y);
                    vectorArray4[1].y = Mathf.Lerp(num12, num13, vectorArray4[1].y);
                    vectorArray4[2].y = Mathf.Lerp(num12, num13, vectorArray4[2].y);
                    var fill = mFillAmount * 2f - j;
                    var flag = j % 2 == 1;
                    if (AdjustRadial(v, vectorArray4, fill, !flag))
                    {
                        if (mInvert)
                        {
                            flag = !flag;
                        }

                        if (flag)
                        {
                            for (var k = 0; k < 4; k++)
                            {
                                num12 = Mathf.Lerp(xy[0].x, xy[2].x, v[k].x);
                                num13 = Mathf.Lerp(xy[0].y, xy[2].y, v[k].y);
                                var num16 = Mathf.Lerp(uv[0].x, uv[2].x, vectorArray4[k].x);
                                var num17 = Mathf.Lerp(uv[0].y, uv[2].y, vectorArray4[k].y);
                                verts.Add(new Vector3(num12, num13, 0f));
                                uvs.Add(new Vector2(num16, num17));
                                cols.Add(item);
                            }
                        }
                        else
                        {
                            for (var m = 3; m > -1; m--)
                            {
                                num12 = Mathf.Lerp(xy[0].x, xy[2].x, v[m].x);
                                num13 = Mathf.Lerp(xy[0].y, xy[2].y, v[m].y);
                                var num19 = Mathf.Lerp(uv[0].x, uv[2].x, vectorArray4[m].x);
                                var num20 = Mathf.Lerp(uv[0].y, uv[2].y, vectorArray4[m].y);
                                verts.Add(new Vector3(num12, num13, 0f));
                                uvs.Add(new Vector2(num19, num20));
                                cols.Add(item);
                            }
                        }
                    }
                }

                return;
            }

            if (fillDirection == FillDirection.Radial360)
            {
                float[] numArray = {0.5f, 1f, 0f, 0.5f, 0.5f, 1f, 0.5f, 1f, 0f, 0.5f, 0.5f, 1f, 0f, 0.5f, 0f, 0.5f};
                var vectorArray5 = new Vector2[4];
                var vectorArray6 = new Vector2[4];
                for (var n = 0; n < 4; n++)
                {
                    vectorArray5[0] = new Vector2(0f, 0f);
                    vectorArray5[1] = new Vector2(0f, 1f);
                    vectorArray5[2] = new Vector2(1f, 1f);
                    vectorArray5[3] = new Vector2(1f, 0f);
                    vectorArray6[0] = new Vector2(0f, 0f);
                    vectorArray6[1] = new Vector2(0f, 1f);
                    vectorArray6[2] = new Vector2(1f, 1f);
                    vectorArray6[3] = new Vector2(1f, 0f);
                    if (mInvert)
                    {
                        if (n > 0)
                        {
                            Rotate(vectorArray5, n);
                            Rotate(vectorArray6, n);
                        }
                    }
                    else if (n < 3)
                    {
                        Rotate(vectorArray5, 3 - n);
                        Rotate(vectorArray6, 3 - n);
                    }

                    for (var num22 = 0; num22 < 4; num22++)
                    {
                        var index = !mInvert ? n * 4 : (3 - n) * 4;
                        var from = numArray[index];
                        var to = numArray[index + 1];
                        var num26 = numArray[index + 2];
                        var num27 = numArray[index + 3];
                        vectorArray5[num22].x = Mathf.Lerp(from, to, vectorArray5[num22].x);
                        vectorArray5[num22].y = Mathf.Lerp(num26, num27, vectorArray5[num22].y);
                        vectorArray6[num22].x = Mathf.Lerp(from, to, vectorArray6[num22].x);
                        vectorArray6[num22].y = Mathf.Lerp(num26, num27, vectorArray6[num22].y);
                    }

                    var num28 = mFillAmount * 4f - n;
                    var flag2 = n % 2 == 1;
                    if (AdjustRadial(vectorArray5, vectorArray6, num28, !flag2))
                    {
                        if (mInvert)
                        {
                            flag2 = !flag2;
                        }

                        if (flag2)
                        {
                            for (var num29 = 0; num29 < 4; num29++)
                            {
                                var num30 = Mathf.Lerp(xy[0].x, xy[2].x, vectorArray5[num29].x);
                                var num31 = Mathf.Lerp(xy[0].y, xy[2].y, vectorArray5[num29].y);
                                var num32 = Mathf.Lerp(uv[0].x, uv[2].x, vectorArray6[num29].x);
                                var num33 = Mathf.Lerp(uv[0].y, uv[2].y, vectorArray6[num29].y);
                                verts.Add(new Vector3(num30, num31, 0f));
                                uvs.Add(new Vector2(num32, num33));
                                cols.Add(item);
                            }
                        }
                        else
                        {
                            for (var num34 = 3; num34 > -1; num34--)
                            {
                                var num35 = Mathf.Lerp(xy[0].x, xy[2].x, vectorArray5[num34].x);
                                var num36 = Mathf.Lerp(xy[0].y, xy[2].y, vectorArray5[num34].y);
                                var num37 = Mathf.Lerp(uv[0].x, uv[2].x, vectorArray6[num34].x);
                                var num38 = Mathf.Lerp(uv[0].y, uv[2].y, vectorArray6[num34].y);
                                verts.Add(new Vector3(num35, num36, 0f));
                                uvs.Add(new Vector2(num37, num38));
                                cols.Add(item);
                            }
                        }
                    }
                }

                return;
            }
        }

        for (var i = 0; i < 4; i++)
        {
            verts.Add(xy[i]);
            uvs.Add(uv[i]);
            cols.Add(item);
        }
    }

    public UIAtlas.Sprite GetAtlasSprite()
    {
        if (!mSpriteSet)
        {
            mSprite = null;
        }

        if (mSprite == null && mAtlas != null)
        {
            if (!string.IsNullOrEmpty(mSpriteName))
            {
                var sp = mAtlas.GetSprite(mSpriteName);
                if (sp == null)
                {
                    return null;
                }

                SetAtlasSprite(sp);
            }

            if (mSprite == null && mAtlas.spriteList.Count > 0)
            {
                var sprite2 = mAtlas.spriteList[0];
                if (sprite2 == null)
                {
                    return null;
                }

                SetAtlasSprite(sprite2);
                if (mSprite == null)
                {
                    Debug.LogError(mAtlas.name + " seems to have a null sprite!");
                    return null;
                }

                mSpriteName = mSprite.name;
            }

            if (mSprite != null)
            {
                material = mAtlas.spriteMaterial;
                UpdateUVs(true);
            }
        }

        return mSprite;
    }

    public override void MakePixelPerfect()
    {
        if (isValid)
        {
            UpdateUVs(false);
            switch (type)
            {
                case Type.Sliced:
                {
                    var localPosition = cachedTransform.localPosition;
                    localPosition.x = Mathf.RoundToInt(localPosition.x);
                    localPosition.y = Mathf.RoundToInt(localPosition.y);
                    localPosition.z = Mathf.RoundToInt(localPosition.z);
                    cachedTransform.localPosition = localPosition;
                    var localScale = cachedTransform.localScale;
                    localScale.x = Mathf.RoundToInt(localScale.x * 0.5f) << 1;
                    localScale.y = Mathf.RoundToInt(localScale.y * 0.5f) << 1;
                    localScale.z = 1f;
                    cachedTransform.localScale = localScale;
                    break;
                }

                case Type.Tiled:
                {
                    var vector3 = cachedTransform.localPosition;
                    vector3.x = Mathf.RoundToInt(vector3.x);
                    vector3.y = Mathf.RoundToInt(vector3.y);
                    vector3.z = Mathf.RoundToInt(vector3.z);
                    cachedTransform.localPosition = vector3;
                    var vector4 = cachedTransform.localScale;
                    vector4.x = Mathf.RoundToInt(vector4.x);
                    vector4.y = Mathf.RoundToInt(vector4.y);
                    vector4.z = 1f;
                    cachedTransform.localScale = vector4;
                    break;
                }

                default:
                {
                    var mainTexture = this.mainTexture;
                    var vector5 = cachedTransform.localScale;
                    if (mainTexture != null)
                    {
                        var rect = NGUIMath.ConvertToPixels(outerUV, mainTexture.width, mainTexture.height, true);
                        var pixelSize = atlas.pixelSize;
                        vector5.x = Mathf.RoundToInt(rect.width * pixelSize) * Mathf.Sign(vector5.x);
                        vector5.y = Mathf.RoundToInt(rect.height * pixelSize) * Mathf.Sign(vector5.y);
                        vector5.z = 1f;
                        cachedTransform.localScale = vector5;
                    }

                    var num2 = Mathf.RoundToInt(Mathf.Abs(vector5.x) * (1f + mSprite.paddingLeft + mSprite.paddingRight));
                    var num3 = Mathf.RoundToInt(Mathf.Abs(vector5.y) * (1f + mSprite.paddingTop + mSprite.paddingBottom));
                    var vector6 = cachedTransform.localPosition;
                    vector6.x = Mathf.CeilToInt(vector6.x * 4f) >> 2;
                    vector6.y = Mathf.CeilToInt(vector6.y * 4f) >> 2;
                    vector6.z = Mathf.RoundToInt(vector6.z);
                    if (num2 % 2 == 1 && (pivot == Pivot.Top || pivot == Pivot.Center || pivot == Pivot.Bottom))
                    {
                        vector6.x += 0.5f;
                    }

                    if (num3 % 2 == 1 && (pivot == Pivot.Left || pivot == Pivot.Center || pivot == Pivot.Right))
                    {
                        vector6.y += 0.5f;
                    }

                    cachedTransform.localPosition = vector6;
                    break;
                }
            }
        }
    }

    public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        switch (type)
        {
            case Type.Simple:
                SimpleFill(verts, uvs, cols);
                break;

            case Type.Sliced:
                SlicedFill(verts, uvs, cols);
                break;

            case Type.Tiled:
                TiledFill(verts, uvs, cols);
                break;

            case Type.Filled:
                FilledFill(verts, uvs, cols);
                break;
        }
    }

    protected override void OnStart()
    {
        if (mAtlas != null)
        {
            UpdateUVs(true);
        }
    }

    protected void Rotate(Vector2[] v, int offset)
    {
        for (var i = 0; i < offset; i++)
        {
            var vector = new Vector2(v[3].x, v[3].y);
            v[3].x = v[2].y;
            v[3].y = v[2].x;
            v[2].x = v[1].y;
            v[2].y = v[1].x;
            v[1].x = v[0].y;
            v[1].y = v[0].x;
            v[0].x = vector.y;
            v[0].y = vector.x;
        }
    }

    protected void SetAtlasSprite(UIAtlas.Sprite sp)
    {
        mChanged = true;
        mSpriteSet = true;
        if (sp != null)
        {
            mSprite = sp;
            mSpriteName = mSprite.name;
        }
        else
        {
            mSpriteName = mSprite == null ? string.Empty : mSprite.name;
            mSprite = sp;
        }
    }

    protected void SimpleFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        var item = new Vector2(mOuterUV.xMin, mOuterUV.yMin);
        var vector2 = new Vector2(mOuterUV.xMax, mOuterUV.yMax);
        verts.Add(new Vector3(1f, 0f, 0f));
        verts.Add(new Vector3(1f, -1f, 0f));
        verts.Add(new Vector3(0f, -1f, 0f));
        verts.Add(new Vector3(0f, 0f, 0f));
        uvs.Add(vector2);
        uvs.Add(new Vector2(vector2.x, item.y));
        uvs.Add(item);
        uvs.Add(new Vector2(item.x, vector2.y));
        var c = color;
        c.a *= mPanel.alpha;
        Color32 color2 = !atlas.premultipliedAlpha ? c : NGUITools.ApplyPMA(c);
        cols.Add(color2);
        cols.Add(color2);
        cols.Add(color2);
        cols.Add(color2);
    }

    protected void SlicedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        if (mOuterUV == mInnerUV)
        {
            SimpleFill(verts, uvs, cols);
        }
        else
        {
            var vectorArray = new Vector2[4];
            var vectorArray2 = new Vector2[4];
            var mainTexture = this.mainTexture;
            vectorArray[0] = Vector2.zero;
            vectorArray[1] = Vector2.zero;
            vectorArray[2] = new Vector2(1f, -1f);
            vectorArray[3] = new Vector2(1f, -1f);
            if (mainTexture == null)
            {
                for (var j = 0; j < 4; j++)
                {
                    vectorArray2[j] = Vector2.zero;
                }
            }
            else
            {
                var pixelSize = atlas.pixelSize;
                var num2 = (mInnerUV.xMin - mOuterUV.xMin) * pixelSize;
                var num3 = (mOuterUV.xMax - mInnerUV.xMax) * pixelSize;
                var num4 = (mInnerUV.yMax - mOuterUV.yMax) * pixelSize;
                var num5 = (mOuterUV.yMin - mInnerUV.yMin) * pixelSize;
                var localScale = cachedTransform.localScale;
                localScale.x = Mathf.Max(0f, localScale.x);
                localScale.y = Mathf.Max(0f, localScale.y);
                var vector2 = new Vector2(localScale.x / mainTexture.width, localScale.y / mainTexture.height);
                var vector3 = new Vector2(num2 / vector2.x, num4 / vector2.y);
                var vector4 = new Vector2(num3 / vector2.x, num5 / vector2.y);
                var pivot = this.pivot;
                switch (pivot)
                {
                    case Pivot.Right:
                    case Pivot.TopRight:
                    case Pivot.BottomRight:
                        vectorArray[0].x = Mathf.Min(0f, 1f - (vector4.x + vector3.x));
                        vectorArray[1].x = vectorArray[0].x + vector3.x;
                        vectorArray[2].x = vectorArray[0].x + Mathf.Max(vector3.x, 1f - vector4.x);
                        vectorArray[3].x = vectorArray[0].x + Mathf.Max(vector3.x + vector4.x, 1f);
                        break;

                    default:
                        vectorArray[1].x = vector3.x;
                        vectorArray[2].x = Mathf.Max(vector3.x, 1f - vector4.x);
                        vectorArray[3].x = Mathf.Max(vector3.x + vector4.x, 1f);
                        break;
                }

                switch (pivot)
                {
                    case Pivot.Bottom:
                    case Pivot.BottomLeft:
                    case Pivot.BottomRight:
                        vectorArray[0].y = Mathf.Max(0f, -1f - (vector4.y + vector3.y));
                        vectorArray[1].y = vectorArray[0].y + vector3.y;
                        vectorArray[2].y = vectorArray[0].y + Mathf.Min(vector3.y, -1f - vector4.y);
                        vectorArray[3].y = vectorArray[0].y + Mathf.Min(vector3.y + vector4.y, -1f);
                        break;

                    default:
                        vectorArray[1].y = vector3.y;
                        vectorArray[2].y = Mathf.Min(vector3.y, -1f - vector4.y);
                        vectorArray[3].y = Mathf.Min(vector3.y + vector4.y, -1f);
                        break;
                }

                vectorArray2[0] = new Vector2(mOuterUV.xMin, mOuterUV.yMax);
                vectorArray2[1] = new Vector2(mInnerUV.xMin, mInnerUV.yMax);
                vectorArray2[2] = new Vector2(mInnerUV.xMax, mInnerUV.yMin);
                vectorArray2[3] = new Vector2(mOuterUV.xMax, mOuterUV.yMin);
            }

            var c = color;
            c.a *= mPanel.alpha;
            Color32 item = !atlas.premultipliedAlpha ? c : NGUITools.ApplyPMA(c);
            for (var i = 0; i < 3; i++)
            {
                var index = i + 1;
                for (var k = 0; k < 3; k++)
                {
                    if (mFillCenter || i != 1 || k != 1)
                    {
                        var num10 = k + 1;
                        verts.Add(new Vector3(vectorArray[index].x, vectorArray[k].y, 0f));
                        verts.Add(new Vector3(vectorArray[index].x, vectorArray[num10].y, 0f));
                        verts.Add(new Vector3(vectorArray[i].x, vectorArray[num10].y, 0f));
                        verts.Add(new Vector3(vectorArray[i].x, vectorArray[k].y, 0f));
                        uvs.Add(new Vector2(vectorArray2[index].x, vectorArray2[k].y));
                        uvs.Add(new Vector2(vectorArray2[index].x, vectorArray2[num10].y));
                        uvs.Add(new Vector2(vectorArray2[i].x, vectorArray2[num10].y));
                        uvs.Add(new Vector2(vectorArray2[i].x, vectorArray2[k].y));
                        cols.Add(item);
                        cols.Add(item);
                        cols.Add(item);
                        cols.Add(item);
                    }
                }
            }
        }
    }

    protected void TiledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        var mainTexture = material.mainTexture;
        if (mainTexture != null)
        {
            var mInner = this.mInner;
            if (atlas.coordinates == UIAtlas.Coordinates.TexCoords)
            {
                mInner = NGUIMath.ConvertToPixels(mInner, mainTexture.width, mainTexture.height, true);
            }

            Vector2 localScale = cachedTransform.localScale;
            var pixelSize = atlas.pixelSize;
            var num2 = Mathf.Abs(mInner.width / localScale.x) * pixelSize;
            var num3 = Mathf.Abs(mInner.height / localScale.y) * pixelSize;
            if (num2 < 0.01f || num3 < 0.01f)
            {
                Debug.LogWarning("The tiled sprite (" + NGUITools.GetHierarchy(gameObject) + ") is too small.\nConsider using a bigger one.");
                num2 = 0.01f;
                num3 = 0.01f;
            }

            var vector2 = new Vector2(mInner.xMin / mainTexture.width, mInner.yMin / mainTexture.height);
            var vector3 = new Vector2(mInner.xMax / mainTexture.width, mInner.yMax / mainTexture.height);
            var vector4 = vector3;
            var c = color;
            c.a *= mPanel.alpha;
            Color32 item = !atlas.premultipliedAlpha ? c : NGUITools.ApplyPMA(c);
            for (var i = 0f; i < 1f; i += num3)
            {
                var x = 0f;
                vector4.x = vector3.x;
                var num6 = i + num3;
                if (num6 > 1f)
                {
                    vector4.y = vector2.y + (vector3.y - vector2.y) * (1f - i) / (num6 - i);
                    num6 = 1f;
                }

                while (x < 1f)
                {
                    var num7 = x + num2;
                    if (num7 > 1f)
                    {
                        vector4.x = vector2.x + (vector3.x - vector2.x) * (1f - x) / (num7 - x);
                        num7 = 1f;
                    }

                    verts.Add(new Vector3(num7, -i, 0f));
                    verts.Add(new Vector3(num7, -num6, 0f));
                    verts.Add(new Vector3(x, -num6, 0f));
                    verts.Add(new Vector3(x, -i, 0f));
                    uvs.Add(new Vector2(vector4.x, 1f - vector2.y));
                    uvs.Add(new Vector2(vector4.x, 1f - vector4.y));
                    uvs.Add(new Vector2(vector2.x, 1f - vector4.y));
                    uvs.Add(new Vector2(vector2.x, 1f - vector2.y));
                    cols.Add(item);
                    cols.Add(item);
                    cols.Add(item);
                    cols.Add(item);
                    x += num2;
                }
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (mChanged || !mSpriteSet)
        {
            mSpriteSet = true;
            mSprite = null;
            mChanged = true;
            UpdateUVs(true);
        }
        else
        {
            UpdateUVs(false);
        }
    }

    public virtual void UpdateUVs(bool force)
    {
        if ((type == Type.Sliced || type == Type.Tiled) && cachedTransform.localScale != mScale)
        {
            mScale = cachedTransform.localScale;
            mChanged = true;
        }

        if (isValid && force)
        {
            var mainTexture = this.mainTexture;
            if (mainTexture != null)
            {
                mInner = mSprite.inner;
                mOuter = mSprite.outer;
                mInnerUV = mInner;
                mOuterUV = mOuter;
                if (atlas.coordinates == UIAtlas.Coordinates.Pixels)
                {
                    mOuterUV = NGUIMath.ConvertToTexCoords(mOuterUV, mainTexture.width, mainTexture.height);
                    mInnerUV = NGUIMath.ConvertToTexCoords(mInnerUV, mainTexture.width, mainTexture.height);
                }
            }
        }
    }

    public UIAtlas atlas
    {
        get { return mAtlas; }
        set
        {
            if (mAtlas != value)
            {
                mAtlas = value;
                mSpriteSet = false;
                mSprite = null;
                material = mAtlas == null ? null : mAtlas.spriteMaterial;
                if (string.IsNullOrEmpty(this.mSpriteName) && mAtlas != null && mAtlas.spriteList.Count > 0)
                {
                    SetAtlasSprite(mAtlas.spriteList[0]);
                    mSpriteName = mSprite.name;
                }

                if (!string.IsNullOrEmpty(this.mSpriteName))
                {
                    var mSpriteName = this.mSpriteName;
                    this.mSpriteName = string.Empty;
                    spriteName = mSpriteName;
                    mChanged = true;
                    UpdateUVs(true);
                }
            }
        }
    }

    public override Vector4 border
    {
        get
        {
            if (type != Type.Sliced)
            {
                return base.border;
            }

            var atlasSprite = GetAtlasSprite();
            if (atlasSprite == null)
            {
                return Vector2.zero;
            }

            var outer = atlasSprite.outer;
            var inner = atlasSprite.inner;
            var mainTexture = this.mainTexture;
            if (atlas.coordinates == UIAtlas.Coordinates.TexCoords && mainTexture != null)
            {
                outer = NGUIMath.ConvertToPixels(outer, mainTexture.width, mainTexture.height, true);
                inner = NGUIMath.ConvertToPixels(inner, mainTexture.width, mainTexture.height, true);
            }

            return new Vector4(inner.xMin - outer.xMin, inner.yMin - outer.yMin, outer.xMax - inner.xMax, outer.yMax - inner.yMax) * atlas.pixelSize;
        }
    }

    public float fillAmount
    {
        get { return mFillAmount; }
        set
        {
            var num = Mathf.Clamp01(value);
            if (mFillAmount != num)
            {
                mFillAmount = num;
                mChanged = true;
            }
        }
    }

    public bool fillCenter
    {
        get { return mFillCenter; }
        set
        {
            if (mFillCenter != value)
            {
                mFillCenter = value;
                MarkAsChanged();
            }
        }
    }

    public FillDirection fillDirection
    {
        get { return mFillDirection; }
        set
        {
            if (mFillDirection != value)
            {
                mFillDirection = value;
                mChanged = true;
            }
        }
    }

    public Rect innerUV
    {
        get
        {
            UpdateUVs(false);
            return mInnerUV;
        }
    }

    public bool invert
    {
        get { return mInvert; }
        set
        {
            if (mInvert != value)
            {
                mInvert = value;
                mChanged = true;
            }
        }
    }

    public bool isValid
    {
        get { return GetAtlasSprite() != null; }
    }

    public override Material material
    {
        get
        {
            var material = base.material;
            if (material == null)
            {
                material = mAtlas == null ? null : mAtlas.spriteMaterial;
                mSprite = null;
                this.material = material;
                if (material != null)
                {
                    UpdateUVs(true);
                }
            }

            return material;
        }
    }

    public Rect outerUV
    {
        get
        {
            UpdateUVs(false);
            return mOuterUV;
        }
    }

    public override bool pixelPerfectAfterResize
    {
        get { return type == Type.Sliced; }
    }

    public override Vector4 relativePadding
    {
        get
        {
            if (isValid && type == Type.Simple)
            {
                return new Vector4(mSprite.paddingLeft, mSprite.paddingTop, mSprite.paddingRight, mSprite.paddingBottom);
            }

            return base.relativePadding;
        }
    }

    public string spriteName
    {
        get { return mSpriteName; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(mSpriteName))
                {
                    mSpriteName = string.Empty;
                    mSprite = null;
                    mChanged = true;
                    mSpriteSet = false;
                }
            }
            else if (mSpriteName != value)
            {
                mSpriteName = value;
                mSprite = null;
                mChanged = true;
                mSpriteSet = false;
                if (isValid)
                {
                    UpdateUVs(true);
                }
            }
        }
    }

    public virtual Type type
    {
        get { return mType; }
        set
        {
            if (mType != value)
            {
                mType = value;
                MarkAsChanged();
            }
        }
    }

    public enum FillDirection
    {
        Horizontal,
        Vertical,
        Radial90,
        Radial180,
        Radial360
    }

    public enum Type
    {
        Simple,
        Sliced,
        Tiled,
        Filled
    }
}