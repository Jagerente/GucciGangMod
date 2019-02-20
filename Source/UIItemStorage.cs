//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
    public UIWidget background;
    public int maxColumns = 4;
    public int maxItemCount = 8;
    public int maxRows = 4;
    private List<InvGameItem> mItems = new List<InvGameItem>();
    public int padding = 10;
    public int spacing = 128;
    public GameObject template;

    public InvGameItem GetItem(int slot)
    {
        return slot >= items.Count ? null : mItems[slot];
    }

    public InvGameItem Replace(int slot, InvGameItem item)
    {
        if (slot < maxItemCount)
        {
            var item2 = items[slot];
            mItems[slot] = item;
            return item2;
        }
        return item;
    }

    private void Start()
    {
        if (template != null)
        {
            var num = 0;
            var bounds = new Bounds();
            for (var i = 0; i < maxRows; i++)
            {
                for (var j = 0; j < maxColumns; j++)
                {
                    var obj2 = NGUITools.AddChild(gameObject, template);
                    obj2.transform.localPosition = new Vector3(padding + (j + 0.5f) * spacing, -padding - (i + 0.5f) * spacing, 0f);
                    var component = obj2.GetComponent<UIStorageSlot>();
                    if (component != null)
                    {
                        component.storage = this;
                        component.slot = num;
                    }
                    bounds.Encapsulate(new Vector3(padding * 2f + (j + 1) * spacing, -padding * 2f - (i + 1) * spacing, 0f));
                    if (++num >= maxItemCount)
                    {
                        if (background != null)
                        {
                            background.transform.localScale = bounds.size;
                        }
                        return;
                    }
                }
            }
            if (background != null)
            {
                background.transform.localScale = bounds.size;
            }
        }
    }

    public List<InvGameItem> items
    {
        get
        {
            while (mItems.Count < maxItemCount)
            {
                mItems.Add(null);
            }
            return mItems;
        }
    }
}

