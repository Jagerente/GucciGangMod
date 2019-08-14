using UnityEngine;

[AddComponentMenu("NGUI/UI/Input (Basic)")]
public class UIInput : MonoBehaviour
{
    public Color activeColor = Color.white;
    public bool autoCorrect;
    public string caratChar = "|";
    public static UIInput current;
    public GameObject eventReceiver;
    public string functionName = "OnSubmit";
    public bool isPassword;
    public UILabel label;
    public int maxChars;
    private Color mDefaultColor = Color.white;
    private string mDefaultText = string.Empty;
    private bool mDoInit = true;
    private string mLastIME = string.Empty;
    private UIWidget.Pivot mPivot = UIWidget.Pivot.Left;
    private float mPosition;
    private string mText = string.Empty;
    public OnSubmit onSubmit;
    public GameObject selectOnTab;
    public KeyboardType type;
    public bool useLabelTextAtStart;
    public Validator validator;

    private void Append(string input)
    {
        var num = 0;
        var length = input.Length;
        while (num < length)
        {
            var nextChar = input[num];
            switch (nextChar)
            {
                case '\b':
                    if (mText.Length > 0)
                    {
                        mText = mText.Substring(0, mText.Length - 1);
                        SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
                    }

                    break;

                case '\r':
                case '\n':
                    if ((UICamera.current.submitKey0 == KeyCode.Return || UICamera.current.submitKey1 == KeyCode.Return) && (!label.multiLine || !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)))
                    {
                        current = this;
                        onSubmit?.Invoke(mText);
                        if (eventReceiver == null)
                        {
                            eventReceiver = gameObject;
                        }

                        eventReceiver.SendMessage(functionName, mText, SendMessageOptions.DontRequireReceiver);
                        current = null;
                        selected = false;
                        return;
                    }

                    if (validator != null)
                    {
                        nextChar = validator(mText, nextChar);
                    }

                    if (nextChar != '\0')
                    {
                        if (nextChar == '\n' || nextChar == '\r')
                        {
                            if (label.multiLine)
                            {
                                mText = mText + "\n";
                            }
                        }
                        else
                        {
                            mText = mText + nextChar;
                        }

                        SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
                    }

                    break;

                default:
                    if (nextChar >= ' ')
                    {
                        if (validator != null)
                        {
                            nextChar = validator(mText, nextChar);
                        }

                        if (nextChar != '\0')
                        {
                            mText = mText + nextChar;
                            SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
                        }
                    }

                    break;
            }

            num++;
        }

        UpdateLabel();
    }

    protected void Init()
    {
        maxChars = 100;
        initMain();
    }

    protected void initMain()
    {
        maxChars = 100;
        if (mDoInit)
        {
            mDoInit = false;
            if (label == null)
            {
                label = GetComponentInChildren<UILabel>();
            }

            if (label != null)
            {
                if (useLabelTextAtStart)
                {
                    mText = label.text;
                }

                mDefaultText = label.text;
                mDefaultColor = label.color;
                label.supportEncoding = false;
                label.password = isPassword;
                mPivot = label.pivot;
                mPosition = label.cachedTransform.localPosition.x;
            }
            else
            {
                enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        if (UICamera.IsHighlighted(gameObject))
        {
            OnSelect(false);
        }
    }

    private void OnEnable()
    {
        if (UICamera.IsHighlighted(gameObject))
        {
            OnSelect(true);
        }
    }

    private void OnInput(string input)
    {
        if (mDoInit)
        {
            initMain();
        }

        if (selected && enabled && NGUITools.GetActive(gameObject) && Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            Append(input);
        }
    }

    private void OnSelect(bool isSelected)
    {
        if (mDoInit)
        {
            initMain();
        }

        if (label != null && enabled && NGUITools.GetActive(gameObject))
        {
            if (isSelected)
            {
                mText = useLabelTextAtStart || !(label.text == mDefaultText) ? label.text : string.Empty;
                label.color = activeColor;
                if (isPassword)
                {
                    label.password = true;
                }

                Input.imeCompositionMode = IMECompositionMode.On;
                var cachedTransform = label.cachedTransform;
                Vector3 pivotOffset = label.pivotOffset;
                pivotOffset.y += label.relativeSize.y;
                pivotOffset = cachedTransform.TransformPoint(pivotOffset);
                Input.compositionCursorPos = UICamera.currentCamera.WorldToScreenPoint(pivotOffset);
                UpdateLabel();
            }
            else
            {
                if (string.IsNullOrEmpty(mText))
                {
                    label.text = mDefaultText;
                    label.color = mDefaultColor;
                    if (isPassword)
                    {
                        label.password = false;
                    }
                }
                else
                {
                    label.text = mText;
                }

                label.showLastPasswordChar = false;
                Input.imeCompositionMode = IMECompositionMode.Off;
                RestoreLabel();
            }
        }
    }

    private void RestoreLabel()
    {
        if (label != null)
        {
            label.pivot = mPivot;
            var localPosition = label.cachedTransform.localPosition;
            localPosition.x = mPosition;
            label.cachedTransform.localPosition = localPosition;
        }
    }

    private void Update()
    {
        if (selected)
        {
            if (selectOnTab != null && Input.GetKeyDown(KeyCode.Tab))
            {
                UICamera.selectedObject = selectOnTab;
            }

            if (Input.GetKeyDown(KeyCode.V) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            {
                Append(NGUITools.clipboard);
            }

            if (mLastIME != Input.compositionString)
            {
                mLastIME = Input.compositionString;
                UpdateLabel();
            }
        }
    }

    private void UpdateLabel()
    {
        if (mDoInit)
        {
            initMain();
        }

        if (maxChars > 0 && mText.Length > maxChars)
        {
            mText = mText.Substring(0, maxChars);
        }

        if (label.font != null)
        {
            string str;
            if (isPassword && selected)
            {
                str = string.Empty;
                var num = 0;
                var length = mText.Length;
                while (num < length)
                {
                    str = str + "*";
                    num++;
                }

                str = str + Input.compositionString + caratChar;
            }
            else
            {
                str = !selected ? mText : mText + Input.compositionString + caratChar;
            }

            label.supportEncoding = false;
            if (!label.shrinkToFit)
            {
                if (label.multiLine)
                {
                    str = label.font.WrapText(str, label.lineWidth / label.cachedTransform.localScale.x, 0, false, UIFont.SymbolStyle.None);
                }
                else
                {
                    var str2 = label.font.GetEndOfLineThatFits(str, label.lineWidth / label.cachedTransform.localScale.x, false, UIFont.SymbolStyle.None);
                    if (str2 != str)
                    {
                        str = str2;
                        var localPosition = label.cachedTransform.localPosition;
                        localPosition.x = mPosition + label.lineWidth;
                        if (mPivot == UIWidget.Pivot.Left)
                        {
                            label.pivot = UIWidget.Pivot.Right;
                        }
                        else if (mPivot == UIWidget.Pivot.TopLeft)
                        {
                            label.pivot = UIWidget.Pivot.TopRight;
                        }
                        else if (mPivot == UIWidget.Pivot.BottomLeft)
                        {
                            label.pivot = UIWidget.Pivot.BottomRight;
                        }

                        label.cachedTransform.localPosition = localPosition;
                    }
                    else
                    {
                        RestoreLabel();
                    }
                }
            }

            label.text = str;
            label.showLastPasswordChar = selected;
        }
    }

    public string defaultText
    {
        get { return mDefaultText; }
        set
        {
            if (label.text == mDefaultText)
            {
                label.text = value;
            }

            mDefaultText = value;
        }
    }

    public bool selected
    {
        get { return UICamera.selectedObject == gameObject; }
        set
        {
            if (!value && UICamera.selectedObject == gameObject)
            {
                UICamera.selectedObject = null;
            }
            else if (value)
            {
                UICamera.selectedObject = gameObject;
            }
        }
    }

    public virtual string text
    {
        get
        {
            if (mDoInit)
            {
                initMain();
            }

            return mText;
        }
        set
        {
            if (mDoInit)
            {
                initMain();
            }

            mText = value;
            if (label != null)
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = mDefaultText;
                }

                label.supportEncoding = false;
                label.text = !selected ? value : value + caratChar;
                label.showLastPasswordChar = selected;
                label.color = !selected && !(value != mDefaultText) ? mDefaultColor : activeColor;
            }
        }
    }

    public enum KeyboardType
    {
        Default,
        ASCIICapable,
        NumbersAndPunctuation,
        URL,
        NumberPad,
        PhonePad,
        NamePhonePad,
        EmailAddress
    }

    public delegate void OnSubmit(string inputString);

    public delegate char Validator(string currentText, char nextChar);
}