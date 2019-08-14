using UnityEngine;

[AddComponentMenu("NGUI/Examples/Drag and Drop Item")]
public class DragDropItem : MonoBehaviour
{
    private bool mIsDragging;
    private Transform mParent;
    private bool mSticky;
    private Transform mTrans;
    public GameObject prefab;

    private void Awake()
    {
        mTrans = transform;
    }

    private void Drop()
    {
        var collider = UICamera.lastHit.collider;
        var container = collider == null ? null : collider.gameObject.GetComponent<DragDropContainer>();
        if (container != null)
        {
            mTrans.parent = container.transform;
            var localPosition = mTrans.localPosition;
            localPosition.z = 0f;
            mTrans.localPosition = localPosition;
        }
        else
        {
            mTrans.parent = mParent;
        }

        UpdateTable();
        NGUITools.MarkParentAsChanged(gameObject);
    }

    private void OnDrag(Vector2 delta)
    {
        if (enabled && UICamera.currentTouchID > -2)
        {
            if (!mIsDragging)
            {
                mIsDragging = true;
                mParent = mTrans.parent;
                mTrans.parent = DragDropRoot.root;
                var localPosition = mTrans.localPosition;
                localPosition.z = 0f;
                mTrans.localPosition = localPosition;
                NGUITools.MarkParentAsChanged(gameObject);
            }
            else
            {
                mTrans.localPosition += (Vector3) delta;
            }
        }
    }

    private void OnPress(bool isPressed)
    {
        if (enabled)
        {
            if (isPressed)
            {
                if (!UICamera.current.stickyPress)
                {
                    mSticky = true;
                    UICamera.current.stickyPress = true;
                }
            }
            else if (mSticky)
            {
                mSticky = false;
                UICamera.current.stickyPress = false;
            }

            mIsDragging = false;
            var collider = this.collider;
            if (collider != null)
            {
                collider.enabled = !isPressed;
            }

            if (!isPressed)
            {
                Drop();
            }
        }
    }

    private void UpdateTable()
    {
        var table = NGUITools.FindInParents<UITable>(gameObject);
        if (table != null)
        {
            table.repositionNow = true;
        }
    }
}