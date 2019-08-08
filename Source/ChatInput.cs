using UnityEngine;

[AddComponentMenu("NGUI/Examples/Chat Input"), RequireComponent(typeof(UIInput))]
public class ChatInput : MonoBehaviour
{
    public bool fillWithDummyData;
    private bool mIgnoreNextEnter;
    private UIInput mInput;
    public UITextList textList;

    private void OnSubmit()
    {
        if (textList != null)
        {
            var str = NGUITools.StripSymbols(mInput.text);
            if (!string.IsNullOrEmpty(str))
            {
                textList.Add(str);
                mInput.text = string.Empty;
                mInput.selected = false;
            }
        }
        mIgnoreNextEnter = true;
    }

    private void Start()
    {
        mInput = GetComponent<UIInput>();
        if (fillWithDummyData && textList != null)
        {
            for (var i = 0; i < 30; i++)
            {
                textList.Add(string.Concat(i % 2 != 0 ? "[AAAAAA]" : "[FFFFFF]", "This is an example paragraph for the text list, testing line ", i, "[-]"));
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (!mIgnoreNextEnter && !mInput.selected)
            {
                mInput.selected = true;
            }
            mIgnoreNextEnter = false;
        }
    }
}