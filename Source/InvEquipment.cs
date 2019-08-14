using UnityEngine;

[AddComponentMenu("NGUI/Examples/Equipment")]
public class InvEquipment : MonoBehaviour
{
    private InvAttachmentPoint[] mAttachments;
    private InvGameItem[] mItems;

    public InvGameItem Equip(InvGameItem item)
    {
        if (item != null)
        {
            var baseItem = item.baseItem;
            if (baseItem != null)
            {
                return Replace(baseItem.slot, item);
            }

            Debug.LogWarning("Can't resolve the item ID of " + item.baseItemID);
        }

        return item;
    }

    public InvGameItem GetItem(InvBaseItem.Slot slot)
    {
        if (slot != InvBaseItem.Slot.None)
        {
            var index = (int) slot - 1;
            if (mItems != null && index < mItems.Length)
            {
                return mItems[index];
            }
        }

        return null;
    }

    public bool HasEquipped(InvBaseItem.Slot slot)
    {
        if (mItems != null)
        {
            var index = 0;
            var length = mItems.Length;
            while (index < length)
            {
                var baseItem = mItems[index].baseItem;
                if (baseItem != null && baseItem.slot == slot)
                {
                    return true;
                }

                index++;
            }
        }

        return false;
    }

    public bool HasEquipped(InvGameItem item)
    {
        if (mItems != null)
        {
            var index = 0;
            var length = mItems.Length;
            while (index < length)
            {
                if (mItems[index] == item)
                {
                    return true;
                }

                index++;
            }
        }

        return false;
    }

    public InvGameItem Replace(InvBaseItem.Slot slot, InvGameItem item)
    {
        var item2 = item == null ? null : item.baseItem;
        if (slot != InvBaseItem.Slot.None)
        {
            if (item2 != null && item2.slot != slot)
            {
                return item;
            }

            if (mItems == null)
            {
                var num = 8;
                mItems = new InvGameItem[num];
            }

            var item3 = mItems[(int) slot - 1];
            mItems[(int) slot - 1] = item;
            if (mAttachments == null)
            {
                mAttachments = GetComponentsInChildren<InvAttachmentPoint>();
            }

            var index = 0;
            var length = mAttachments.Length;
            while (index < length)
            {
                var point = mAttachments[index];
                if (point.slot == slot)
                {
                    var obj2 = point.Attach(item2 == null ? null : item2.attachment);
                    if (item2 != null && obj2 != null)
                    {
                        var renderer = obj2.renderer;
                        if (renderer != null)
                        {
                            renderer.material.color = item2.color;
                        }
                    }
                }

                index++;
            }

            return item3;
        }

        if (item != null)
        {
            Debug.LogWarning("Can't equip \"" + item.name + "\" because it doesn't specify an item slot");
        }

        return item;
    }

    public InvGameItem Unequip(InvBaseItem.Slot slot)
    {
        return Replace(slot, null);
    }

    public InvGameItem Unequip(InvGameItem item)
    {
        var baseItem = item?.baseItem;
        if (baseItem != null)
        {
            return Replace(baseItem.slot, null);
        }

        return item;
    }

    public InvGameItem[] equippedItems
    {
        get { return mItems; }
    }
}