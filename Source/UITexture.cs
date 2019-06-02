using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : UIWidget
{
    private bool mCreatingMat;
    private Material mDynamicMat;
    private int mPMA = -1;
    [HideInInspector, SerializeField] private Rect mRect = new Rect(0f, 0f, 1f, 1f);
    [SerializeField, HideInInspector] private Shader mShader;
    [SerializeField, HideInInspector] private Texture mTexture;

    public override void MakePixelPerfect()
    {
        var mainTexture = this.mainTexture;
        if (mainTexture != null)
        {
            var localScale = cachedTransform.localScale;
            localScale.x = mainTexture.width * uvRect.width;
            localScale.y = mainTexture.height * uvRect.height;
            localScale.z = 1f;
            cachedTransform.localScale = localScale;
        }

        base.MakePixelPerfect();
    }

    private void OnDestroy()
    {
        NGUITools.Destroy(mDynamicMat);
    }

    public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        var c = color;
        c.a *= mPanel.alpha;
        Color32 item = !premultipliedAlpha ? c : NGUITools.ApplyPMA(c);
        verts.Add(new Vector3(1f, 0f, 0f));
        verts.Add(new Vector3(1f, -1f, 0f));
        verts.Add(new Vector3(0f, -1f, 0f));
        verts.Add(new Vector3(0f, 0f, 0f));
        uvs.Add(new Vector2(mRect.xMax, mRect.yMax));
        uvs.Add(new Vector2(mRect.xMax, mRect.yMin));
        uvs.Add(new Vector2(mRect.xMin, mRect.yMin));
        uvs.Add(new Vector2(mRect.xMin, mRect.yMax));
        cols.Add(item);
        cols.Add(item);
        cols.Add(item);
        cols.Add(item);
    }

    public bool hasDynamicMaterial
    {
        get { return mDynamicMat != null; }
    }

    public override bool keepMaterial
    {
        get { return true; }
    }

    public override Texture mainTexture
    {
        get { return mTexture == null ? base.mainTexture : mTexture; }
        set
        {
            if (mPanel != null && mMat != null)
            {
                mPanel.RemoveWidget(this);
            }

            if (mMat == null)
            {
                mDynamicMat = new Material(shader);
                mDynamicMat.hideFlags = HideFlags.DontSave;
                mMat = mDynamicMat;
            }

            mPanel = null;
            mTex = value;
            mTexture = value;
            mMat.mainTexture = value;
            if (enabled)
            {
                CreatePanel();
            }
        }
    }

    public override Material material
    {
        get
        {
            if (!mCreatingMat && mMat == null)
            {
                mCreatingMat = true;
                if (mainTexture != null)
                {
                    if (mShader == null)
                    {
                        mShader = Shader.Find("Unlit/Texture");
                    }

                    mDynamicMat = new Material(mShader);
                    mDynamicMat.hideFlags = HideFlags.DontSave;
                    mDynamicMat.mainTexture = mainTexture;
                    base.material = mDynamicMat;
                    mPMA = 0;
                }

                mCreatingMat = false;
            }

            return mMat;
        }
        set
        {
            if (mDynamicMat != value && mDynamicMat != null)
            {
                NGUITools.Destroy(mDynamicMat);
                mDynamicMat = null;
            }

            base.material = value;
            mPMA = -1;
        }
    }

    public bool premultipliedAlpha
    {
        get
        {
            if (mPMA == -1)
            {
                var material = this.material;
                mPMA = material == null || material.shader == null || !material.shader.name.Contains("Premultiplied")
                    ? 0
                    : 1;
            }

            return mPMA == 1;
        }
    }

    public Shader shader
    {
        get
        {
            if (mShader == null)
            {
                var material = this.material;
                if (material != null)
                {
                    mShader = material.shader;
                }

                if (mShader == null)
                {
                    mShader = Shader.Find("Unlit/Texture");
                }
            }

            return mShader;
        }
        set
        {
            if (mShader != value)
            {
                mShader = value;
                var material = this.material;
                if (material != null)
                {
                    material.shader = value;
                }

                mPMA = -1;
            }
        }
    }

    public Rect uvRect
    {
        get { return mRect; }
        set
        {
            if (mRect != value)
            {
                mRect = value;
                MarkAsChanged();
            }
        }
    }
}