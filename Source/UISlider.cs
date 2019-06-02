using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Slider")]
public class UISlider : IgnoreTimeScale
{
    public static UISlider current;
    public Direction direction;
    public GameObject eventReceiver;
    public Transform foreground;
    public string functionName = "OnSliderChange";
    private Vector2 mCenter = Vector3.zero;
    private BoxCollider mCol;
    private UISprite mFGFilled;
    private Transform mFGTrans;
    private UIWidget mFGWidget;
    private bool mInitDone;
    private Vector2 mSize = Vector2.zero;
    private Transform mTrans;
    public int numberOfSteps;
    public OnValueChange onValueChange;
    [HideInInspector, SerializeField] private float rawValue = 1f;
    public Transform thumb;

    private void Awake()
    {
        mTrans = transform;
        mCol = collider as BoxCollider;
    }

    public void ForceUpdate()
    {
        Set(rawValue, true);
    }

    private void Init()
    {
        mInitDone = true;
        if (foreground != null)
        {
            mFGWidget = foreground.GetComponent<UIWidget>();
            mFGFilled = mFGWidget == null ? null : mFGWidget as UISprite;
            mFGTrans = foreground.transform;
            if (mSize == Vector2.zero)
            {
                mSize = foreground.localScale;
            }

            if (mCenter == Vector2.zero)
            {
                mCenter = foreground.localPosition + foreground.localScale * 0.5f;
            }
        }
        else if (mCol != null)
        {
            if (mSize == Vector2.zero)
            {
                mSize = mCol.size;
            }

            if (mCenter == Vector2.zero)
            {
                mCenter = mCol.center;
            }
        }
        else
        {
            Debug.LogWarning("UISlider expected to find a foreground object or a box collider to work with", this);
        }
    }

    private void OnDrag(Vector2 delta)
    {
        UpdateDrag();
    }

    private void OnDragThumb(GameObject go, Vector2 delta)
    {
        UpdateDrag();
    }

    private void OnKey(KeyCode key)
    {
        var num = numberOfSteps <= 1f ? 0.125f : 1f / (numberOfSteps - 1);
        if (direction == Direction.Horizontal)
        {
            if (key == KeyCode.LeftArrow)
            {
                Set(rawValue - num, false);
            }
            else if (key == KeyCode.RightArrow)
            {
                Set(rawValue + num, false);
            }
        }
        else if (key == KeyCode.DownArrow)
        {
            Set(rawValue - num, false);
        }
        else if (key == KeyCode.UpArrow)
        {
            Set(rawValue + num, false);
        }
    }

    private void OnPress(bool pressed)
    {
        if (pressed && UICamera.currentTouchID != -100)
        {
            UpdateDrag();
        }
    }

    private void OnPressThumb(GameObject go, bool pressed)
    {
        if (pressed)
        {
            UpdateDrag();
        }
    }

    private void Set(float input, bool force)
    {
        if (!mInitDone)
        {
            Init();
        }

        var num = Mathf.Clamp01(input);
        if (num < 0.001f)
        {
            num = 0f;
        }

        var sliderValue = this.sliderValue;
        rawValue = num;
        var num3 = this.sliderValue;
        if (force || sliderValue != num3)
        {
            Vector3 mSize = this.mSize;
            if (direction == Direction.Horizontal)
            {
                mSize.x *= num3;
            }
            else
            {
                mSize.y *= num3;
            }

            if (mFGFilled != null && mFGFilled.type == UISprite.Type.Filled)
            {
                mFGFilled.fillAmount = num3;
            }
            else if (foreground != null)
            {
                mFGTrans.localScale = mSize;
                if (mFGWidget != null)
                {
                    if (num3 > 0.001f)
                    {
                        mFGWidget.enabled = true;
                        mFGWidget.MarkAsChanged();
                    }
                    else
                    {
                        mFGWidget.enabled = false;
                    }
                }
            }

            if (thumb != null)
            {
                var localPosition = thumb.localPosition;
                if (mFGFilled != null && mFGFilled.type == UISprite.Type.Filled)
                {
                    if (mFGFilled.fillDirection == UISprite.FillDirection.Horizontal)
                    {
                        localPosition.x = !mFGFilled.invert ? mSize.x : this.mSize.x - mSize.x;
                    }
                    else if (mFGFilled.fillDirection == UISprite.FillDirection.Vertical)
                    {
                        localPosition.y = !mFGFilled.invert ? mSize.y : this.mSize.y - mSize.y;
                    }
                    else
                    {
                        Debug.LogWarning("Slider thumb is only supported with Horizontal or Vertical fill direction",
                            this);
                    }
                }
                else if (direction == Direction.Horizontal)
                {
                    localPosition.x = mSize.x;
                }
                else
                {
                    localPosition.y = mSize.y;
                }

                thumb.localPosition = localPosition;
            }

            current = this;
            if (eventReceiver != null && !string.IsNullOrEmpty(functionName) && Application.isPlaying)
            {
                eventReceiver.SendMessage(functionName, num3, SendMessageOptions.DontRequireReceiver);
            }

            onValueChange?.Invoke(num3);
            current = null;
        }
    }

    private void Start()
    {
        Init();
        if (Application.isPlaying && thumb != null && thumb.collider != null)
        {
            var listener = UIEventListener.Get(thumb.gameObject);
            listener.onPress = (UIEventListener.BoolDelegate) Delegate.Combine(listener.onPress,
                new UIEventListener.BoolDelegate(OnPressThumb));
            listener.onDrag = (UIEventListener.VectorDelegate) Delegate.Combine(listener.onDrag,
                new UIEventListener.VectorDelegate(OnDragThumb));
        }

        Set(rawValue, true);
    }

    private void UpdateDrag()
    {
        if (mCol != null && UICamera.currentCamera != null && UICamera.currentTouch != null)
        {
            float num;
            UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
            var ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
            var plane = new Plane(mTrans.rotation * Vector3.back, mTrans.position);
            if (plane.Raycast(ray, out num))
            {
                var vector = mTrans.localPosition + (Vector3) (mCenter - mSize * 0.5f);
                var vector2 = mTrans.localPosition - vector;
                var vector4 = mTrans.InverseTransformPoint(ray.GetPoint(num)) + vector2;
                Set(direction != Direction.Horizontal ? vector4.y / mSize.y : vector4.x / mSize.x, false);
            }
        }
    }

    public Vector2 fullSize
    {
        get { return mSize; }
        set
        {
            if (mSize != value)
            {
                mSize = value;
                ForceUpdate();
            }
        }
    }

    public float sliderValue
    {
        get
        {
            var rawValue = this.rawValue;
            if (numberOfSteps > 1)
            {
                rawValue = Mathf.Round(rawValue * (numberOfSteps - 1)) / (numberOfSteps - 1);
            }

            return rawValue;
        }
        set { Set(value, false); }
    }

    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public delegate void OnValueChange(float val);
}