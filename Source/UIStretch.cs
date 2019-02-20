//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/UI/Stretch"), ExecuteInEditMode]
public class UIStretch : MonoBehaviour
{
    public Vector2 initialSize = Vector2.one;
    private Animation mAnim;
    private Rect mRect;
    private UIRoot mRoot;
    private Transform mTrans;
    public UIPanel panelContainer;
    public Vector2 relativeSize = Vector2.one;
    public Style style;
    public Camera uiCamera;
    public UIWidget widgetContainer;

    private void Awake()
    {
        mAnim = animation;
        mRect = new Rect();
        mTrans = transform;
    }

    private void Start()
    {
        if (uiCamera == null)
        {
            uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        }
        mRoot = NGUITools.FindInParents<UIRoot>(gameObject);
    }

    private void Update()
    {
        if ((mAnim == null || !mAnim.isPlaying) && style != Style.None)
        {
            var pixelSizeAdjustment = 1f;
            if (panelContainer != null)
            {
                if (panelContainer.clipping == UIDrawCall.Clipping.None)
                {
                    mRect.xMin = -Screen.width * 0.5f;
                    mRect.yMin = -Screen.height * 0.5f;
                    mRect.xMax = -mRect.xMin;
                    mRect.yMax = -mRect.yMin;
                }
                else
                {
                    var clipRange = panelContainer.clipRange;
                    mRect.x = clipRange.x - clipRange.z * 0.5f;
                    mRect.y = clipRange.y - clipRange.w * 0.5f;
                    mRect.width = clipRange.z;
                    mRect.height = clipRange.w;
                }
            }
            else if (widgetContainer != null)
            {
                var cachedTransform = widgetContainer.cachedTransform;
                var vector2 = cachedTransform.localScale;
                var localPosition = cachedTransform.localPosition;
                var relativeSize = (Vector3) widgetContainer.relativeSize;
                var pivotOffset = (Vector3) widgetContainer.pivotOffset;
                pivotOffset.y--;
                pivotOffset.x *= widgetContainer.relativeSize.x * vector2.x;
                pivotOffset.y *= widgetContainer.relativeSize.y * vector2.y;
                mRect.x = localPosition.x + pivotOffset.x;
                mRect.y = localPosition.y + pivotOffset.y;
                mRect.width = relativeSize.x * vector2.x;
                mRect.height = relativeSize.y * vector2.y;
            }
            else if (uiCamera != null)
            {
                mRect = uiCamera.pixelRect;
                if (mRoot != null)
                {
                    pixelSizeAdjustment = mRoot.pixelSizeAdjustment;
                }
            }
            else
            {
                return;
            }
            var width = mRect.width;
            var height = mRect.height;
            if (pixelSizeAdjustment != 1f && height > 1f)
            {
                var num4 = mRoot.activeHeight / height;
                width *= num4;
                height *= num4;
            }
            var localScale = mTrans.localScale;
            if (style == Style.BasedOnHeight)
            {
                localScale.x = relativeSize.x * height;
                localScale.y = relativeSize.y * height;
            }
            else if (style == Style.FillKeepingRatio)
            {
                var num5 = width / height;
                var num6 = initialSize.x / initialSize.y;
                if (num6 < num5)
                {
                    var num7 = width / initialSize.x;
                    localScale.x = width;
                    localScale.y = initialSize.y * num7;
                }
                else
                {
                    var num8 = height / initialSize.y;
                    localScale.x = initialSize.x * num8;
                    localScale.y = height;
                }
            }
            else if (style == Style.FitInternalKeepingRatio)
            {
                var num9 = width / height;
                var num10 = initialSize.x / initialSize.y;
                if (num10 > num9)
                {
                    var num11 = width / initialSize.x;
                    localScale.x = width;
                    localScale.y = initialSize.y * num11;
                }
                else
                {
                    var num12 = height / initialSize.y;
                    localScale.x = initialSize.x * num12;
                    localScale.y = height;
                }
            }
            else
            {
                if (style == Style.Both || style == Style.Horizontal)
                {
                    localScale.x = relativeSize.x * width;
                }
                if (style == Style.Both || style == Style.Vertical)
                {
                    localScale.y = relativeSize.y * height;
                }
            }
            if (mTrans.localScale != localScale)
            {
                mTrans.localScale = localScale;
            }
        }
    }

    public enum Style
    {
        None,
        Horizontal,
        Vertical,
        Both,
        BasedOnHeight,
        FillKeepingRatio,
        FitInternalKeepingRatio
    }
}

