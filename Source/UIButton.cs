//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
    public Color disabledColor = Color.grey;

    protected override void OnEnable()
    {
        if (isEnabled)
        {
            base.OnEnable();
        }
        else
        {
            UpdateColor(false, true);
        }
    }

    public override void OnHover(bool isOver)
    {
        if (isEnabled)
        {
            base.OnHover(isOver);
        }
    }

    public override void OnPress(bool isPressed)
    {
        if (isEnabled)
        {
            base.OnPress(isPressed);
        }
    }

    public void UpdateColor(bool shouldBeEnabled, bool immediate)
    {
        if (tweenTarget != null)
        {
            if (!mStarted)
            {
                mStarted = true;
                Init();
            }
            var color = !shouldBeEnabled ? disabledColor : defaultColor;
            var color2 = TweenColor.Begin(tweenTarget, 0.15f, color);
            if (immediate)
            {
                color2.color = color;
                color2.enabled = false;
            }
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
                UpdateColor(value, false);
            }
        }
    }
}

