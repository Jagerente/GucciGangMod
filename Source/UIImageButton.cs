using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
    public string disabledSprite;
    public string hoverSprite;
    public string normalSprite;
    public string pressedSprite;
    public UISprite target;

    private void Awake()
    {
        if (target == null)
        {
            target = GetComponentInChildren<UISprite>();
        }
    }

    private void OnEnable()
    {
        UpdateImage();
    }

    private void OnHover(bool isOver)
    {
        if (isEnabled && target != null)
        {
            target.spriteName = !isOver ? normalSprite : hoverSprite;
            target.MakePixelPerfect();
        }
    }

    private void OnPress(bool pressed)
    {
        if (pressed)
        {
            target.spriteName = pressedSprite;
            target.MakePixelPerfect();
        }
        else
        {
            UpdateImage();
        }
    }

    private void UpdateImage()
    {
        if (target != null)
        {
            if (isEnabled)
            {
                target.spriteName = !UICamera.IsHighlighted(gameObject) ? normalSprite : hoverSprite;
            }
            else
            {
                target.spriteName = disabledSprite;
            }

            target.MakePixelPerfect();
        }
    }

    public bool isEnabled
    {
        get
        {
            var collider = this.collider;
            return collider != null && collider.enabled;
        }
        set
        {
            var collider = this.collider;
            if (collider != null && collider.enabled != value)
            {
                collider.enabled = value;
                UpdateImage();
            }
        }
    }
}