//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag Camera"), ExecuteInEditMode]
public class UIDragCamera : IgnoreTimeScale
{
    public UIDraggableCamera draggableCamera;
    [SerializeField, HideInInspector]
    private Component target;

    private void Awake()
    {
        if (target != null)
        {
            if (draggableCamera == null)
            {
                draggableCamera = target.GetComponent<UIDraggableCamera>();
                if (draggableCamera == null)
                {
                    draggableCamera = target.gameObject.AddComponent<UIDraggableCamera>();
                }
            }
            target = null;
        }
        else if (draggableCamera == null)
        {
            draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(gameObject);
        }
    }

    private void OnDrag(Vector2 delta)
    {
        if ((enabled && NGUITools.GetActive(gameObject)) && (draggableCamera != null))
        {
            draggableCamera.Drag(delta);
        }
    }

    private void OnPress(bool isPressed)
    {
        if ((enabled && NGUITools.GetActive(gameObject)) && (draggableCamera != null))
        {
            draggableCamera.Press(isPressed);
        }
    }

    private void OnScroll(float delta)
    {
        if ((enabled && NGUITools.GetActive(gameObject)) && (draggableCamera != null))
        {
            draggableCamera.Scroll(delta);
        }
    }
}

