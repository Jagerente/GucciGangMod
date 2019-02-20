//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
    public VoidDelegate onClick;
    public VoidDelegate onDoubleClick;
    public VectorDelegate onDrag;
    public ObjectDelegate onDrop;
    public BoolDelegate onHover;
    public StringDelegate onInput;
    public KeyCodeDelegate onKey;
    public BoolDelegate onPress;
    public FloatDelegate onScroll;
    public BoolDelegate onSelect;
    public VoidDelegate onSubmit;
    public object parameter;

    public static UIEventListener Get(GameObject go)
    {
        var component = go.GetComponent<UIEventListener>();
        if (component == null)
        {
            component = go.AddComponent<UIEventListener>();
        }
        return component;
    }

    private void OnClick()
    {
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

    private void OnDoubleClick()
    {
        if (onDoubleClick != null)
        {
            onDoubleClick(gameObject);
        }
    }

    private void OnDrag(Vector2 delta)
    {
        if (onDrag != null)
        {
            onDrag(gameObject, delta);
        }
    }

    private void OnDrop(GameObject go)
    {
        if (onDrop != null)
        {
            onDrop(gameObject, go);
        }
    }

    private void OnHover(bool isOver)
    {
        if (onHover != null)
        {
            onHover(gameObject, isOver);
        }
    }

    private void OnInput(string text)
    {
        if (onInput != null)
        {
            onInput(gameObject, text);
        }
    }

    private void OnKey(KeyCode key)
    {
        if (onKey != null)
        {
            onKey(gameObject, key);
        }
    }

    private void OnPress(bool isPressed)
    {
        if (onPress != null)
        {
            onPress(gameObject, isPressed);
        }
    }

    private void OnScroll(float delta)
    {
        if (onScroll != null)
        {
            onScroll(gameObject, delta);
        }
    }

    private void OnSelect(bool selected)
    {
        if (onSelect != null)
        {
            onSelect(gameObject, selected);
        }
    }

    private void OnSubmit()
    {
        if (onSubmit != null)
        {
            onSubmit(gameObject);
        }
    }

    public delegate void BoolDelegate(GameObject go, bool state);

    public delegate void FloatDelegate(GameObject go, float delta);

    public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

    public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

    public delegate void StringDelegate(GameObject go, string text);

    public delegate void VectorDelegate(GameObject go, Vector2 delta);

    public delegate void VoidDelegate(GameObject go);
}

