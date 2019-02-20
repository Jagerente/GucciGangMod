//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
    public int slot;
    public UIItemStorage storage;

    protected override InvGameItem Replace(InvGameItem item)
    {
        return storage == null ? item : storage.Replace(slot, item);
    }

    protected override InvGameItem observedItem
    {
        get
        {
            return storage == null ? null : storage.GetItem(slot);
        }
    }
}

