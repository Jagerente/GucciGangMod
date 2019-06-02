using System.Collections.Generic;
using UnityEngine;

public abstract class UIItemSlot : MonoBehaviour
{
    public UIWidget background;
    public AudioClip errorSound;
    public AudioClip grabSound;
    public UISprite icon;
    public UILabel label;
    private static InvGameItem mDraggedItem;
    private InvGameItem mItem;
    private string mText = string.Empty;
    public AudioClip placeSound;

    private void OnClick()
    {
        if (mDraggedItem != null)
        {
            OnDrop(null);
        }
        else if (mItem != null)
        {
            mDraggedItem = Replace(null);
            if (mDraggedItem != null)
            {
                NGUITools.PlaySound(grabSound);
            }

            UpdateCursor();
        }
    }

    private void OnDrag(Vector2 delta)
    {
        if (mDraggedItem == null && mItem != null)
        {
            UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
            mDraggedItem = Replace(null);
            NGUITools.PlaySound(grabSound);
            UpdateCursor();
        }
    }

    private void OnDrop(GameObject go)
    {
        var item = Replace(mDraggedItem);
        if (mDraggedItem == item)
        {
            NGUITools.PlaySound(errorSound);
        }
        else if (item != null)
        {
            NGUITools.PlaySound(grabSound);
        }
        else
        {
            NGUITools.PlaySound(placeSound);
        }

        mDraggedItem = item;
        UpdateCursor();
    }

    private void OnTooltip(bool show)
    {
        var item = !show ? null : mItem;
        InvBaseItem baseItem = item?.baseItem;
        if (baseItem != null)
        {
            string[] textArray1 = {"[", NGUITools.EncodeColor(item.color), "]", item.name, "[-]\n"};
            var str2 = string.Concat(textArray1);
            object[] objArray1 = {str2, "[AFAFAF]Level ", item.itemLevel, " ", baseItem.slot};
            var tooltipText = string.Concat(objArray1);
            var list = item.CalculateStats();
            var num = 0;
            var count = list.Count;
            while (num < count)
            {
                var stat = list[num];
                if (stat.amount != 0)
                {
                    if (stat.amount < 0)
                    {
                        tooltipText = tooltipText + "\n[FF0000]" + stat.amount;
                    }
                    else
                    {
                        tooltipText = tooltipText + "\n[00FF00]+" + stat.amount;
                    }

                    if (stat.modifier == InvStat.Modifier.Percent)
                    {
                        tooltipText = tooltipText + "%";
                    }

                    tooltipText = tooltipText + " " + stat.id + "[-]";
                }

                num++;
            }

            if (!string.IsNullOrEmpty(baseItem.description))
            {
                tooltipText = tooltipText + "\n[FF9900]" + baseItem.description;
            }

            UITooltip.ShowText(tooltipText);
            return;
        }

        UITooltip.ShowText(null);
    }

    protected abstract InvGameItem Replace(InvGameItem item);

    private void Update()
    {
        var observedItem = this.observedItem;
        if (mItem != observedItem)
        {
            mItem = observedItem;
            InvBaseItem item2 = observedItem == null ? null : observedItem.baseItem;
            if (label != null)
            {
                string str = observedItem == null ? null : observedItem.name;
                if (string.IsNullOrEmpty(mText))
                {
                    mText = label.text;
                }

                label.text = str == null ? mText : str;
            }

            if (icon != null)
            {
                if (item2 == null || item2.iconAtlas == null)
                {
                    icon.enabled = false;
                }
                else
                {
                    icon.atlas = item2.iconAtlas;
                    icon.spriteName = item2.iconName;
                    icon.enabled = true;
                    icon.MakePixelPerfect();
                }
            }

            if (background != null)
            {
                background.color = observedItem == null ? Color.white : observedItem.color;
            }
        }
    }

    private void UpdateCursor()
    {
        if (mDraggedItem != null && mDraggedItem.baseItem != null)
        {
            UICursor.Set(mDraggedItem.baseItem.iconAtlas, mDraggedItem.baseItem.iconName);
        }
        else
        {
            UICursor.Clear();
        }
    }

    protected abstract InvGameItem observedItem { get; }
}