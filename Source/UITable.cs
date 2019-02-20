//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : MonoBehaviour
{
    public int columns;
    public Direction direction;
    public bool hideInactive = true;
    public bool keepWithinPanel;
    private List<Transform> mChildren = new List<Transform>();
    private UIDraggablePanel mDrag;
    private UIPanel mPanel;
    private bool mStarted;
    public OnReposition onReposition;
    public Vector2 padding = Vector2.zero;
    public bool repositionNow;
    public bool sorted;

    private void LateUpdate()
    {
        if (repositionNow)
        {
            repositionNow = false;
            Reposition();
        }
    }

    public void Reposition()
    {
        if (mStarted)
        {
            var target = transform;
            mChildren.Clear();
            var children = this.children;
            if (children.Count > 0)
            {
                RepositionVariableSize(children);
            }
            if (mDrag != null)
            {
                mDrag.UpdateScrollbars(true);
                mDrag.RestrictWithinBounds(true);
            }
            else if (mPanel != null)
            {
                mPanel.ConstrainTargetToBounds(target, true);
            }
            if (onReposition != null)
            {
                onReposition();
            }
        }
        else
        {
            repositionNow = true;
        }
    }

    private void RepositionVariableSize(List<Transform> children)
    {
        var num = 0f;
        var num2 = 0f;
        var num3 = columns <= 0 ? 1 : children.Count / columns + 1;
        var num4 = columns <= 0 ? children.Count : columns;
        var boundsArray = new Bounds[num3, num4];
        var boundsArray2 = new Bounds[num4];
        var boundsArray3 = new Bounds[num3];
        var index = 0;
        var num6 = 0;
        var num7 = 0;
        var count = children.Count;
        while (num7 < count)
        {
            var trans = children[num7];
            var bounds = NGUIMath.CalculateRelativeWidgetBounds(trans);
            var localScale = trans.localScale;
            bounds.min = Vector3.Scale(bounds.min, localScale);
            bounds.max = Vector3.Scale(bounds.max, localScale);
            boundsArray[num6, index] = bounds;
            boundsArray2[index].Encapsulate(bounds);
            boundsArray3[num6].Encapsulate(bounds);
            if (++index >= columns && columns > 0)
            {
                index = 0;
                num6++;
            }
            num7++;
        }
        index = 0;
        num6 = 0;
        var num9 = 0;
        var num10 = children.Count;
        while (num9 < num10)
        {
            var transform2 = children[num9];
            var bounds2 = boundsArray[num6, index];
            var bounds3 = boundsArray2[index];
            var bounds4 = boundsArray3[num6];
            var localPosition = transform2.localPosition;
            localPosition.x = num + bounds2.extents.x - bounds2.center.x;
            localPosition.x += bounds2.min.x - bounds3.min.x + padding.x;
            if (direction == Direction.Down)
            {
                localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
                localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - padding.y;
            }
            else
            {
                localPosition.y = num2 + bounds2.extents.y - bounds2.center.y;
                localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - padding.y;
            }
            num += bounds3.max.x - bounds3.min.x + padding.x * 2f;
            transform2.localPosition = localPosition;
            if (++index >= columns && columns > 0)
            {
                index = 0;
                num6++;
                num = 0f;
                num2 += bounds4.size.y + padding.y * 2f;
            }
            num9++;
        }
    }

    public static int SortByName(Transform a, Transform b)
    {
        return string.Compare(a.name, b.name);
    }

    private void Start()
    {
        mStarted = true;
        if (keepWithinPanel)
        {
            mPanel = NGUITools.FindInParents<UIPanel>(gameObject);
            mDrag = NGUITools.FindInParents<UIDraggablePanel>(gameObject);
        }
        Reposition();
    }

    public List<Transform> children
    {
        get
        {
            if (mChildren.Count == 0)
            {
                var transform = this.transform;
                mChildren.Clear();
                for (var i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    if (child != null && child.gameObject != null && (!hideInactive || NGUITools.GetActive(child.gameObject)))
                    {
                        mChildren.Add(child);
                    }
                }
                if (sorted)
                {
                    mChildren.Sort(new Comparison<Transform>(SortByName));
                }
            }
            return mChildren;
        }
    }

    public enum Direction
    {
        Down,
        Up
    }

    public delegate void OnReposition();
}

