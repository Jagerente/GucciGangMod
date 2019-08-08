using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Item Database"), ExecuteInEditMode]
public class InvDatabase : MonoBehaviour
{
    public int databaseID;
    public UIAtlas iconAtlas;
    public List<InvBaseItem> items = new List<InvBaseItem>();
    private static bool mIsDirty = true;
    private static InvDatabase[] mList;

    public static InvBaseItem FindByID(int id32)
    {
        var database = GetDatabase(id32 >> 16);
        return database == null ? null : database.GetItem(id32 & 65535);
    }

    public static InvBaseItem FindByName(string exact)
    {
        var index = 0;
        var length = list.Length;
        while (index < length)
        {
            var database = list[index];
            var num3 = 0;
            var count = database.items.Count;
            while (num3 < count)
            {
                var item = database.items[num3];
                if (item.name == exact)
                {
                    return item;
                }
                num3++;
            }
            index++;
        }
        return null;
    }

    public static int FindItemID(InvBaseItem item)
    {
        var index = 0;
        var length = list.Length;
        while (index < length)
        {
            var database = list[index];
            if (database.items.Contains(item))
            {
                return (database.databaseID << 16) | item.id16;
            }
            index++;
        }
        return -1;
    }

    private static InvDatabase GetDatabase(int dbID)
    {
        var index = 0;
        var length = list.Length;
        while (index < length)
        {
            var database = list[index];
            if (database.databaseID == dbID)
            {
                return database;
            }
            index++;
        }
        return null;
    }

    private InvBaseItem GetItem(int id16)
    {
        var num = 0;
        var count = items.Count;
        while (num < count)
        {
            var item = items[num];
            if (item.id16 == id16)
            {
                return item;
            }
            num++;
        }
        return null;
    }

    private void OnDisable()
    {
        mIsDirty = true;
    }

    private void OnEnable()
    {
        mIsDirty = true;
    }

    public static InvDatabase[] list
    {
        get
        {
            if (mIsDirty)
            {
                mIsDirty = false;
                mList = NGUITools.FindActive<InvDatabase>();
            }
            return mList;
        }
    }
}