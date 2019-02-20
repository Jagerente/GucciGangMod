//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Center On Child")]
public class UICenterOnChild : MonoBehaviour
{
    private GameObject mCenteredObject;
    private UIDraggablePanel mDrag;
    public SpringPanel.OnFinished onFinished;
    public float springStrength = 8f;

    private void OnDragFinished()
    {
        if (enabled)
        {
            Recenter();
        }
    }

    private void OnEnable()
    {
        Recenter();
    }

    public void Recenter()
    {
        if (mDrag == null)
        {
            mDrag = NGUITools.FindInParents<UIDraggablePanel>(gameObject);
            if (mDrag == null)
            {
                Debug.LogWarning(string.Concat(new object[] { GetType(), " requires ", typeof(UIDraggablePanel), " on a parent object in order to work" }), this);
                enabled = false;
                return;
            }
            mDrag.onDragFinished = new UIDraggablePanel.OnDragFinished(OnDragFinished);
            if (mDrag.horizontalScrollBar != null)
            {
                mDrag.horizontalScrollBar.onDragFinished = new UIScrollBar.OnDragFinished(OnDragFinished);
            }
            if (mDrag.verticalScrollBar != null)
            {
                mDrag.verticalScrollBar.onDragFinished = new UIScrollBar.OnDragFinished(OnDragFinished);
            }
        }
        if (mDrag.panel != null)
        {
            var clipRange = mDrag.panel.clipRange;
            var cachedTransform = mDrag.panel.cachedTransform;
            var localPosition = cachedTransform.localPosition;
            localPosition.x += clipRange.x;
            localPosition.y += clipRange.y;
            localPosition = cachedTransform.parent.TransformPoint(localPosition);
            var vector3 = localPosition - mDrag.currentMomentum * (mDrag.momentumAmount * 0.1f);
            mDrag.currentMomentum = Vector3.zero;
            var maxValue = float.MaxValue;
            Transform transform2 = null;
            var transform = this.transform;
            var index = 0;
            var childCount = transform.childCount;
            while (index < childCount)
            {
                var child = transform.GetChild(index);
                var num4 = Vector3.SqrMagnitude(child.position - vector3);
                if (num4 < maxValue)
                {
                    maxValue = num4;
                    transform2 = child;
                }
                index++;
            }
            if (transform2 != null)
            {
                mCenteredObject = transform2.gameObject;
                var vector4 = cachedTransform.InverseTransformPoint(transform2.position);
                var vector5 = cachedTransform.InverseTransformPoint(localPosition);
                var vector6 = vector4 - vector5;
                if (mDrag.scale.x == 0f)
                {
                    vector6.x = 0f;
                }
                if (mDrag.scale.y == 0f)
                {
                    vector6.y = 0f;
                }
                if (mDrag.scale.z == 0f)
                {
                    vector6.z = 0f;
                }
                SpringPanel.Begin(mDrag.gameObject, cachedTransform.localPosition - vector6, springStrength).onFinished = onFinished;
            }
            else
            {
                mCenteredObject = null;
            }
        }
    }

    public GameObject centeredObject
    {
        get
        {
            return mCenteredObject;
        }
    }
}

