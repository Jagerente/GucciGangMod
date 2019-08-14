using UnityEngine;

[AddComponentMenu("NGUI/UI/Anchor"), ExecuteInEditMode]
public class UIAnchor : MonoBehaviour
{
    public bool halfPixelOffset = true;
    private Animation mAnim;
    private bool mNeedsHalfPixelOffset;
    private Rect mRect;
    private UIRoot mRoot;
    private Transform mTrans;
    public UIPanel panelContainer;
    public Vector2 relativeOffset = Vector2.zero;
    public bool runOnlyOnce;
    public Side side = Side.Center;
    public Camera uiCamera;
    public UIWidget widgetContainer;

    private void Awake()
    {
        mTrans = transform;
        mAnim = animation;
    }

    private void Start()
    {
        mRoot = NGUITools.FindInParents<UIRoot>(gameObject);
        mNeedsHalfPixelOffset = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.XBOX360 || Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.WindowsEditor;
        if (mNeedsHalfPixelOffset)
        {
            mNeedsHalfPixelOffset = SystemInfo.graphicsShaderLevel < 40;
        }

        if (uiCamera == null)
        {
            uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        }

        Update();
    }

    private void Update()
    {
        if (mAnim == null || !mAnim.enabled || !mAnim.isPlaying)
        {
            var flag = false;
            if (panelContainer != null)
            {
                if (panelContainer.clipping == UIDrawCall.Clipping.None)
                {
                    var num = mRoot == null ? 0.5f : mRoot.activeHeight / (float) Screen.height * 0.5f;
                    mRect.xMin = -Screen.width * num;
                    mRect.yMin = -Screen.height * num;
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
                var localScale = cachedTransform.localScale;
                var localPosition = cachedTransform.localPosition;
                Vector3 relativeSize = widgetContainer.relativeSize;
                Vector3 pivotOffset = widgetContainer.pivotOffset;
                pivotOffset.y--;
                pivotOffset.x *= widgetContainer.relativeSize.x * localScale.x;
                pivotOffset.y *= widgetContainer.relativeSize.y * localScale.y;
                mRect.x = localPosition.x + pivotOffset.x;
                mRect.y = localPosition.y + pivotOffset.y;
                mRect.width = relativeSize.x * localScale.x;
                mRect.height = relativeSize.y * localScale.y;
            }
            else if (uiCamera != null)
            {
                flag = true;
                mRect = uiCamera.pixelRect;
            }
            else
            {
                return;
            }

            var x = (mRect.xMin + mRect.xMax) * 0.5f;
            var y = (mRect.yMin + mRect.yMax) * 0.5f;
            var position = new Vector3(x, y, 0f);
            if (side != Side.Center)
            {
                if (side == Side.Right || side == Side.TopRight || side == Side.BottomRight)
                {
                    position.x = mRect.xMax;
                }
                else if (side == Side.Top || side == Side.Center || side == Side.Bottom)
                {
                    position.x = x;
                }
                else
                {
                    position.x = mRect.xMin;
                }

                if (side == Side.Top || side == Side.TopRight || side == Side.TopLeft)
                {
                    position.y = mRect.yMax;
                }
                else if (side == Side.Left || side == Side.Center || side == Side.Right)
                {
                    position.y = y;
                }
                else
                {
                    position.y = mRect.yMin;
                }
            }

            var width = mRect.width;
            var height = mRect.height;
            position.x += relativeOffset.x * width;
            position.y += relativeOffset.y * height;
            if (flag)
            {
                if (uiCamera.orthographic)
                {
                    position.x = Mathf.Round(position.x);
                    position.y = Mathf.Round(position.y);
                    if (halfPixelOffset && mNeedsHalfPixelOffset)
                    {
                        position.x -= 0.5f;
                        position.y += 0.5f;
                    }
                }

                position.z = uiCamera.WorldToScreenPoint(mTrans.position).z;
                position = uiCamera.ScreenToWorldPoint(position);
            }
            else
            {
                position.x = Mathf.Round(position.x);
                position.y = Mathf.Round(position.y);
                if (panelContainer != null)
                {
                    position = panelContainer.cachedTransform.TransformPoint(position);
                }
                else if (widgetContainer != null)
                {
                    var parent = widgetContainer.cachedTransform.parent;
                    if (parent != null)
                    {
                        position = parent.TransformPoint(position);
                    }
                }

                position.z = mTrans.position.z;
            }

            if (mTrans.position != position)
            {
                mTrans.position = position;
            }

            if (runOnlyOnce && Application.isPlaying)
            {
                Destroy(this);
            }
        }
    }

    public enum Side
    {
        BottomLeft,
        Left,
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        Center
    }
}