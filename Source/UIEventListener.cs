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
        onClick?.Invoke(gameObject);
    }

    private void OnDoubleClick()
    {
        onDoubleClick?.Invoke(gameObject);
    }

    private void OnDrag(Vector2 delta)
    {
        onDrag?.Invoke(gameObject, delta);
    }

    private void OnDrop(GameObject go)
    {
        onDrop?.Invoke(gameObject, go);
    }

    private void OnHover(bool isOver)
    {
        onHover?.Invoke(gameObject, isOver);
    }

    private void OnInput(string text)
    {
        onInput?.Invoke(gameObject, text);
    }

    private void OnKey(KeyCode key)
    {
        onKey?.Invoke(gameObject, key);
    }

    private void OnPress(bool isPressed)
    {
        onPress?.Invoke(gameObject, isPressed);
    }

    private void OnScroll(float delta)
    {
        onScroll?.Invoke(gameObject, delta);
    }

    private void OnSelect(bool selected)
    {
        onSelect?.Invoke(gameObject, selected);
    }

    private void OnSubmit()
    {
        onSubmit?.Invoke(gameObject);
    }

    public delegate void BoolDelegate(GameObject go, bool state);

    public delegate void FloatDelegate(GameObject go, float delta);

    public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

    public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

    public delegate void StringDelegate(GameObject go, string text);

    public delegate void VectorDelegate(GameObject go, Vector2 delta);

    public delegate void VoidDelegate(GameObject go);
}