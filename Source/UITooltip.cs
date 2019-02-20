//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
    public float appearSpeed = 10f;
    public UISprite background;
    private float mCurrent;
    private static UITooltip mInstance;
    private Vector3 mPos;
    private Vector3 mSize;
    private float mTarget;
    private Transform mTrans;
    private UIWidget[] mWidgets;
    public bool scalingTransitions = true;
    public UILabel text;
    public Camera uiCamera;

    private void Awake()
    {
        mInstance = this;
    }

    private void OnDestroy()
    {
        mInstance = null;
    }

    private void SetAlpha(float val)
    {
        var index = 0;
        var length = mWidgets.Length;
        while (index < length)
        {
            var widget = mWidgets[index];
            var color = widget.color;
            color.a = val;
            widget.color = color;
            index++;
        }
    }

    private void SetText(string tooltipText)
    {
        if (text != null && !string.IsNullOrEmpty(tooltipText))
        {
            mTarget = 1f;
            if (text != null)
            {
                text.text = tooltipText;
            }
            mPos = Input.mousePosition;
            if (background != null)
            {
                var transform = background.transform;
                var transform2 = text.transform;
                var localPosition = transform2.localPosition;
                var localScale = transform2.localScale;
                mSize = text.relativeSize;
                mSize.x *= localScale.x;
                mSize.y *= localScale.y;
                mSize.x += background.border.x + background.border.z + (localPosition.x - background.border.x) * 2f;
                mSize.y += background.border.y + background.border.w + (-localPosition.y - background.border.y) * 2f;
                mSize.z = 1f;
                transform.localScale = mSize;
            }
            if (uiCamera != null)
            {
                mPos.x = Mathf.Clamp01(mPos.x / Screen.width);
                mPos.y = Mathf.Clamp01(mPos.y / Screen.height);
                var num = uiCamera.orthographicSize / mTrans.parent.lossyScale.y;
                var num2 = Screen.height * 0.5f / num;
                var vector10 = new Vector2(num2 * mSize.x / Screen.width, num2 * mSize.y / Screen.height);
                mPos.x = Mathf.Min(mPos.x, 1f - vector10.x);
                mPos.y = Mathf.Max(mPos.y, vector10.y);
                mTrans.position = uiCamera.ViewportToWorldPoint(mPos);
                mPos = mTrans.localPosition;
                mPos.x = Mathf.Round(mPos.x);
                mPos.y = Mathf.Round(mPos.y);
                mTrans.localPosition = mPos;
            }
            else
            {
                if (mPos.x + mSize.x > Screen.width)
                {
                    mPos.x = Screen.width - mSize.x;
                }
                if (mPos.y - mSize.y < 0f)
                {
                    mPos.y = mSize.y;
                }
                mPos.x -= Screen.width * 0.5f;
                mPos.y -= Screen.height * 0.5f;
            }
        }
        else
        {
            mTarget = 0f;
        }
    }

    public static void ShowText(string tooltipText)
    {
        if (mInstance != null)
        {
            mInstance.SetText(tooltipText);
        }
    }

    private void Start()
    {
        mTrans = transform;
        mWidgets = GetComponentsInChildren<UIWidget>();
        mPos = mTrans.localPosition;
        mSize = mTrans.localScale;
        if (uiCamera == null)
        {
            uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        }
        SetAlpha(0f);
    }

    private void Update()
    {
        if (mCurrent != mTarget)
        {
            mCurrent = Mathf.Lerp(mCurrent, mTarget, Time.deltaTime * appearSpeed);
            if (Mathf.Abs(mCurrent - mTarget) < 0.001f)
            {
                mCurrent = mTarget;
            }
            SetAlpha(mCurrent * mCurrent);
            if (scalingTransitions)
            {
                var vector = mSize * 0.25f;
                vector.y = -vector.y;
                var vector2 = Vector3.one * (1.5f - mCurrent * 0.5f);
                var vector3 = Vector3.Lerp(mPos - vector, mPos, mCurrent);
                mTrans.localPosition = vector3;
                mTrans.localScale = vector2;
            }
        }
    }
}

