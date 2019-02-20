//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Grid"), ExecuteInEditMode]
public class UIGrid : MonoBehaviour
{
    public Arrangement arrangement;
    public float cellHeight = 200f;
    public float cellWidth = 200f;
    public bool hideInactive = true;
    public int maxPerLine;
    private bool mStarted;
    public bool repositionNow;
    public bool sorted;

    public void Reposition()
    {
        if (!mStarted)
        {
            repositionNow = true;
        }
        else
        {
            var transform = this.transform;
            var num = 0;
            var num2 = 0;
            if (sorted)
            {
                var list = new List<Transform>();
                for (var i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    if ((child != null) && (!hideInactive || NGUITools.GetActive(child.gameObject)))
                    {
                        list.Add(child);
                    }
                }
                list.Sort(new Comparison<Transform>(SortByName));
                var num4 = 0;
                var count = list.Count;
                while (num4 < count)
                {
                    var transform3 = list[num4];
                    if (NGUITools.GetActive(transform3.gameObject) || !hideInactive)
                    {
                        var z = transform3.localPosition.z;
                        transform3.localPosition = (arrangement != Arrangement.Horizontal) ? new Vector3(cellWidth * num2, -cellHeight * num, z) : new Vector3(cellWidth * num, -cellHeight * num2, z);
                        if ((++num >= maxPerLine) && (maxPerLine > 0))
                        {
                            num = 0;
                            num2++;
                        }
                    }
                    num4++;
                }
            }
            else
            {
                for (var j = 0; j < transform.childCount; j++)
                {
                    var transform4 = transform.GetChild(j);
                    if (NGUITools.GetActive(transform4.gameObject) || !hideInactive)
                    {
                        var num8 = transform4.localPosition.z;
                        transform4.localPosition = (arrangement != Arrangement.Horizontal) ? new Vector3(cellWidth * num2, -cellHeight * num, num8) : new Vector3(cellWidth * num, -cellHeight * num2, num8);
                        if ((++num >= maxPerLine) && (maxPerLine > 0))
                        {
                            num = 0;
                            num2++;
                        }
                    }
                }
            }
            var panel = NGUITools.FindInParents<UIDraggablePanel>(gameObject);
            if (panel != null)
            {
                panel.UpdateScrollbars(true);
            }
        }
    }

    public static int SortByName(Transform a, Transform b)
    {
        return string.Compare(a.name, b.name);
    }

    private void Start()
    {
        mStarted = true;
        Reposition();
    }

    private void Update()
    {
        if (repositionNow)
        {
            repositionNow = false;
            Reposition();
        }
    }

    public enum Arrangement
    {
        Horizontal,
        Vertical
    }
}

