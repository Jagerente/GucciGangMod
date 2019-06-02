using UnityEngine;

[RequireComponent(typeof(UISprite)), AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
    private UIAtlas mAtlas;
    private static UICursor mInstance;
    private UISprite mSprite;
    private string mSpriteName;
    private Transform mTrans;
    public Camera uiCamera;

    private void Awake()
    {
        mInstance = this;
    }

    public static void Clear()
    {
        Set(mInstance.mAtlas, mInstance.mSpriteName);
    }

    private void OnDestroy()
    {
        mInstance = null;
    }

    public static void Set(UIAtlas atlas, string sprite)
    {
        if (mInstance != null)
        {
            mInstance.mSprite.atlas = atlas;
            mInstance.mSprite.spriteName = sprite;
            mInstance.mSprite.MakePixelPerfect();
            mInstance.Update();
        }
    }

    private void Start()
    {
        mTrans = transform;
        mSprite = GetComponentInChildren<UISprite>();
        mAtlas = mSprite.atlas;
        mSpriteName = mSprite.spriteName;
        mSprite.depth = 100;
        if (uiCamera == null)
        {
            uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        }
    }

    private void Update()
    {
        if (mSprite.atlas != null)
        {
            var mousePosition = Input.mousePosition;
            if (uiCamera != null)
            {
                mousePosition.x = Mathf.Clamp01(mousePosition.x / Screen.width);
                mousePosition.y = Mathf.Clamp01(mousePosition.y / Screen.height);
                mTrans.position = uiCamera.ViewportToWorldPoint(mousePosition);
                if (uiCamera.isOrthoGraphic)
                {
                    mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mTrans.localPosition, mTrans.localScale);
                }
            }
            else
            {
                mousePosition.x -= Screen.width * 0.5f;
                mousePosition.y -= Screen.height * 0.5f;
                mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mousePosition, mTrans.localScale);
            }
        }
    }
}