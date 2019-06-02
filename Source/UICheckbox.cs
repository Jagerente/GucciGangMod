using AnimationOrTween;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Checkbox")]
public class UICheckbox : MonoBehaviour
{
    public Animation checkAnimation;
    public UISprite checkSprite;
    public static UICheckbox current;
    public GameObject eventReceiver;
    public string functionName = "OnActivate";
    public bool instantTween;
    private bool mChecked = true;
    private bool mStarted;
    private Transform mTrans;
    public OnStateChange onStateChange;
    [SerializeField, HideInInspector]
    private bool option;
    public bool optionCanBeNone;
    public Transform radioButtonRoot;
    public bool startsChecked = true;

    private void Awake()
    {
        mTrans = transform;
        if (checkSprite != null)
        {
            checkSprite.alpha = !startsChecked ? 0f : 1f;
        }
        if (option)
        {
            option = false;
            if (radioButtonRoot == null)
            {
                radioButtonRoot = mTrans.parent;
            }
        }
    }

    private void OnClick()
    {
        if (enabled)
        {
            isChecked = !isChecked;
        }
    }

    private void Set(bool state)
    {
        if (!mStarted)
        {
            mChecked = state;
            startsChecked = state;
            if (checkSprite != null)
            {
                checkSprite.alpha = !state ? 0f : 1f;
            }
        }
        else if (mChecked != state)
        {
            if (radioButtonRoot != null && state)
            {
                var componentsInChildren = radioButtonRoot.GetComponentsInChildren<UICheckbox>(true);
                var index = 0;
                var length = componentsInChildren.Length;
                while (index < length)
                {
                    var checkbox = componentsInChildren[index];
                    if (checkbox != this && checkbox.radioButtonRoot == radioButtonRoot)
                    {
                        checkbox.Set(false);
                    }
                    index++;
                }
            }
            mChecked = state;
            if (checkSprite != null)
            {
                if (instantTween)
                {
                    checkSprite.alpha = !mChecked ? 0f : 1f;
                }
                else
                {
                    TweenAlpha.Begin(checkSprite.gameObject, 0.15f, !mChecked ? 0f : 1f);
                }
            }
            current = this;
            onStateChange?.Invoke(mChecked);
            if (eventReceiver != null && !string.IsNullOrEmpty(functionName))
            {
                eventReceiver.SendMessage(functionName, mChecked, SendMessageOptions.DontRequireReceiver);
            }
            current = null;
            if (checkAnimation != null)
            {
                ActiveAnimation.Play(checkAnimation, !state ? Direction.Reverse : Direction.Forward);
            }
        }
    }

    private void Start()
    {
        if (eventReceiver == null)
        {
            eventReceiver = gameObject;
        }
        mChecked = !startsChecked;
        mStarted = true;
        Set(startsChecked);
    }

    public bool isChecked
    {
        get
        {
            return mChecked;
        }
        set
        {
            if (radioButtonRoot == null || value || optionCanBeNone || !mStarted)
            {
                Set(value);
            }
        }
    }

    public delegate void OnStateChange(bool state);
}

