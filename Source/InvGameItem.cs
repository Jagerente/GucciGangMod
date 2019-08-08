using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InvGameItem
{
    public int itemLevel;
    private InvBaseItem mBaseItem;

    [SerializeField]
    private int mBaseItemID;

    public Quality quality;

    public InvGameItem(int id)
    {
        quality = Quality.Sturdy;
        itemLevel = 1;
        mBaseItemID = id;
    }

    public InvGameItem(int id, InvBaseItem bi)
    {
        quality = Quality.Sturdy;
        itemLevel = 1;
        mBaseItemID = id;
        mBaseItem = bi;
    }

    public List<InvStat> CalculateStats()
    {
        var list = new List<InvStat>();
        if (baseItem != null)
        {
            var statMultiplier = this.statMultiplier;
            var stats = baseItem.stats;
            var num2 = 0;
            var count = stats.Count;
            while (num2 < count)
            {
                var stat = stats[num2];
                var num4 = Mathf.RoundToInt(statMultiplier * stat.amount);
                if (num4 != 0)
                {
                    var flag = false;
                    var num5 = 0;
                    var num6 = list.Count;
                    while (num5 < num6)
                    {
                        var stat2 = list[num5];
                        if (stat2.id == stat.id && stat2.modifier == stat.modifier)
                        {
                            stat2.amount += num4;
                            flag = true;
                            break;
                        }
                        num5++;
                    }
                    if (!flag)
                    {
                        var item = new InvStat
                        {
                            id = stat.id,
                            amount = num4,
                            modifier = stat.modifier
                        };
                        list.Add(item);
                    }
                }
                num2++;
            }
            list.Sort(InvStat.CompareArmor);
        }
        return list;
    }

    public InvBaseItem baseItem
    {
        get
        {
            if (mBaseItem == null)
            {
                mBaseItem = InvDatabase.FindByID(baseItemID);
            }
            return mBaseItem;
        }
    }

    public int baseItemID
    {
        get
        {
            return mBaseItemID;
        }
    }

    public Color color
    {
        get
        {
            var white = Color.white;
            switch (quality)
            {
                case Quality.Broken:
                    return new Color(0.4f, 0.2f, 0.2f);

                case Quality.Cursed:
                    return Color.red;

                case Quality.Damaged:
                    return new Color(0.4f, 0.4f, 0.4f);

                case Quality.Worn:
                    return new Color(0.7f, 0.7f, 0.7f);

                case Quality.Sturdy:
                    return new Color(1f, 1f, 1f);

                case Quality.Polished:
                    return NGUIMath.HexToColor(3774856959);

                case Quality.Improved:
                    return NGUIMath.HexToColor(2480359935);

                case Quality.Crafted:
                    return NGUIMath.HexToColor(1325334783);

                case Quality.Superior:
                    return NGUIMath.HexToColor(12255231);

                case Quality.Enchanted:
                    return NGUIMath.HexToColor(1937178111);

                case Quality.Epic:
                    return NGUIMath.HexToColor(2516647935);

                case Quality.Legendary:
                    return NGUIMath.HexToColor(4287627519);
            }
            return white;
        }
    }

    public string name
    {
        get
        {
            if (baseItem == null)
            {
                return null;
            }
            return quality + " " + baseItem.name;
        }
    }

    public float statMultiplier
    {
        get
        {
            var num = 0f;
            switch (quality)
            {
                case Quality.Broken:
                    num = 0f;
                    break;

                case Quality.Cursed:
                    num = -1f;
                    break;

                case Quality.Damaged:
                    num = 0.25f;
                    break;

                case Quality.Worn:
                    num = 0.9f;
                    break;

                case Quality.Sturdy:
                    num = 1f;
                    break;

                case Quality.Polished:
                    num = 1.1f;
                    break;

                case Quality.Improved:
                    num = 1.25f;
                    break;

                case Quality.Crafted:
                    num = 1.5f;
                    break;

                case Quality.Superior:
                    num = 1.75f;
                    break;

                case Quality.Enchanted:
                    num = 2f;
                    break;

                case Quality.Epic:
                    num = 2.5f;
                    break;

                case Quality.Legendary:
                    num = 3f;
                    break;
            }
            var from = itemLevel / 50f;
            return num * Mathf.Lerp(@from, @from * @from, 0.5f);
        }
    }

    public enum Quality
    {
        Broken,
        Cursed,
        Damaged,
        Worn,
        Sturdy,
        Polished,
        Improved,
        Crafted,
        Superior,
        Enchanted,
        Epic,
        Legendary,
        _LastDoNotUse
    }
}