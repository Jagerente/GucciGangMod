//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using System.Text;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
    public int maxEntries = 50;
    public float maxHeight;
    public float maxWidth;
    protected List<Paragraph> mParagraphs = new List<Paragraph>();
    protected float mScroll;
    protected bool mSelected;
    protected char[] mSeparator = new[] { '\n' };
    protected int mTotalLines;
    public Style style;
    public bool supportScrollWheel = true;
    public UILabel textLabel;

    public void Add(string text)
    {
        Add(text, true);
    }

    protected void Add(string text, bool updateVisible)
    {
        Paragraph item = null;
        if (mParagraphs.Count < maxEntries)
        {
            item = new Paragraph();
        }
        else
        {
            item = mParagraphs[0];
            mParagraphs.RemoveAt(0);
        }
        item.text = text;
        mParagraphs.Add(item);
        if (textLabel != null && textLabel.font != null)
        {
            item.lines = textLabel.font.WrapText(item.text, maxWidth / textLabel.transform.localScale.y, textLabel.maxLineCount, textLabel.supportEncoding, textLabel.symbolStyle).Split(mSeparator);
            mTotalLines = 0;
            var num = 0;
            var count = mParagraphs.Count;
            while (num < count)
            {
                mTotalLines += mParagraphs[num].lines.Length;
                num++;
            }
        }
        if (updateVisible)
        {
            UpdateVisibleText();
        }
    }

    private void Awake()
    {
        if (textLabel == null)
        {
            textLabel = GetComponentInChildren<UILabel>();
        }
        if (textLabel != null)
        {
            textLabel.lineWidth = 0;
        }
        var collider = this.collider;
        if (collider != null)
        {
            if (maxHeight <= 0f)
            {
                maxHeight = collider.bounds.size.y / transform.lossyScale.y;
            }
            if (maxWidth <= 0f)
            {
                maxWidth = collider.bounds.size.x / transform.lossyScale.x;
            }
        }
    }

    public void Clear()
    {
        mParagraphs.Clear();
        UpdateVisibleText();
    }

    private void OnScroll(float val)
    {
        if (mSelected && supportScrollWheel)
        {
            val *= style != Style.Chat ? -10f : 10f;
            mScroll = Mathf.Max(0f, mScroll + val);
            UpdateVisibleText();
        }
    }

    private void OnSelect(bool selected)
    {
        mSelected = selected;
    }

    protected void UpdateVisibleText()
    {
        if (textLabel != null && textLabel.font != null)
        {
            var num = 0;
            var num2 = maxHeight <= 0f ? 100000 : Mathf.FloorToInt(maxHeight / textLabel.cachedTransform.localScale.y);
            var num3 = Mathf.RoundToInt(mScroll);
            if (num2 + num3 > mTotalLines)
            {
                num3 = Mathf.Max(0, mTotalLines - num2);
                mScroll = num3;
            }
            if (style == Style.Chat)
            {
                num3 = Mathf.Max(0, mTotalLines - num2 - num3);
            }
            var builder = new StringBuilder();
            var num4 = 0;
            var count = mParagraphs.Count;
            while (num4 < count)
            {
                var paragraph = mParagraphs[num4];
                var index = 0;
                var length = paragraph.lines.Length;
                while (index < length)
                {
                    var str = paragraph.lines[index];
                    if (num3 > 0)
                    {
                        num3--;
                    }
                    else
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("\n");
                        }
                        builder.Append(str);
                        num++;
                        if (num >= num2)
                        {
                            break;
                        }
                    }
                    index++;
                }
                if (num >= num2)
                {
                    break;
                }
                num4++;
            }
            textLabel.text = builder.ToString();
        }
    }

    protected class Paragraph
    {
        public string[] lines;
        public string text;
    }

    public enum Style
    {
        Text,
        Chat
    }
}

