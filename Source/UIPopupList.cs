//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : MonoBehaviour
{
    private const float animSpeed = 0.15f;
    public UIAtlas atlas;
    public Color backgroundColor = Color.white;
    public string backgroundSprite;
    public static UIPopupList current;
    public GameObject eventReceiver;
    public UIFont font;
    public string functionName = "OnSelectionChange";
    public Color highlightColor = new Color(0.5960785f, 1f, 0.2f, 1f);
    public string highlightSprite;
    public bool isAnimated = true;
    public bool isLocalized;
    public List<string> items = new List<string>();
    private UISprite mBackground;
    private float mBgBorder;
    private GameObject mChild;
    private UISprite mHighlight;
    private UILabel mHighlightedLabel;
    private List<UILabel> mLabelList = new List<UILabel>();
    private UIPanel mPanel;
    [HideInInspector, SerializeField]
    private string mSelectedItem;
    public OnSelectionChange onSelectionChange;
    public Vector2 padding = new Vector3(4f, 4f);
    public Position position;
    public Color textColor = Color.white;
    public UILabel textLabel;
    public float textScale = 1f;

    private void Animate(UIWidget widget, bool placeAbove, float bottom)
    {
        AnimateColor(widget);
        AnimatePosition(widget, placeAbove, bottom);
    }

    private void AnimateColor(UIWidget widget)
    {
        var color = widget.color;
        widget.color = new Color(color.r, color.g, color.b, 0f);
        TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
    }

    private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
    {
        var localPosition = widget.cachedTransform.localPosition;
        var vector2 = !placeAbove ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
        widget.cachedTransform.localPosition = vector2;
        TweenPosition.Begin(widget.gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
    }

    private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
    {
        var gameObject = widget.gameObject;
        var cachedTransform = widget.cachedTransform;
        var y = font.size * textScale + mBgBorder * 2f;
        var localScale = cachedTransform.localScale;
        cachedTransform.localScale = new Vector3(localScale.x, y, localScale.z);
        TweenScale.Begin(gameObject, 0.15f, localScale).method = UITweener.Method.EaseOut;
        if (placeAbove)
        {
            var localPosition = cachedTransform.localPosition;
            cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - localScale.y + y, localPosition.z);
            TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
        }
    }

    private void Highlight(UILabel lbl, bool instant)
    {
        if (mHighlight != null)
        {
            var component = lbl.GetComponent<TweenPosition>();
            if (component == null || !component.enabled)
            {
                mHighlightedLabel = lbl;
                var atlasSprite = mHighlight.GetAtlasSprite();
                if (atlasSprite != null)
                {
                    var num = atlasSprite.inner.xMin - atlasSprite.outer.xMin;
                    var y = atlasSprite.inner.yMin - atlasSprite.outer.yMin;
                    var pos = lbl.cachedTransform.localPosition + new Vector3(-num, y, 1f);
                    if (instant || !isAnimated)
                    {
                        mHighlight.cachedTransform.localPosition = pos;
                    }
                    else
                    {
                        TweenPosition.Begin(mHighlight.gameObject, 0.1f, pos).method = UITweener.Method.EaseOut;
                    }
                }
            }
        }
    }

    private void OnClick()
    {
        if (mChild == null && atlas != null && font != null && items.Count > 0)
        {
            mLabelList.Clear();
            handleEvents = true;
            if (mPanel == null)
            {
                mPanel = UIPanel.Find(this.transform, true);
            }
            var child = this.transform;
            var bounds = NGUIMath.CalculateRelativeWidgetBounds(child.parent, child);
            mChild = new GameObject("Drop-down List");
            mChild.layer = gameObject.layer;
            var transform = mChild.transform;
            transform.parent = child.parent;
            transform.localPosition = bounds.min;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            mBackground = NGUITools.AddSprite(mChild, atlas, backgroundSprite);
            mBackground.pivot = UIWidget.Pivot.TopLeft;
            mBackground.depth = NGUITools.CalculateNextDepth(mPanel.gameObject);
            mBackground.color = backgroundColor;
            var border = mBackground.border;
            mBgBorder = border.y;
            mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
            mHighlight = NGUITools.AddSprite(mChild, atlas, highlightSprite);
            mHighlight.pivot = UIWidget.Pivot.TopLeft;
            mHighlight.color = highlightColor;
            var atlasSprite = mHighlight.GetAtlasSprite();
            if (atlasSprite != null)
            {
                var num = atlasSprite.inner.yMin - atlasSprite.outer.yMin;
                var num2 = font.size * font.pixelSize * textScale;
                var a = 0f;
                var y = -padding.y;
                var list = new List<UILabel>();
                var num5 = 0;
                var count = items.Count;
                while (num5 < count)
                {
                    var key = items[num5];
                    var item = NGUITools.AddWidget<UILabel>(mChild);
                    item.pivot = UIWidget.Pivot.TopLeft;
                    item.font = font;
                    item.text = !isLocalized || Localization.instance == null ? key : Localization.instance.Get(key);
                    item.color = textColor;
                    item.cachedTransform.localPosition = new Vector3(border.x + padding.x, y, -1f);
                    item.MakePixelPerfect();
                    if (textScale != 1f)
                    {
                        var localScale = item.cachedTransform.localScale;
                        item.cachedTransform.localScale = localScale * textScale;
                    }
                    list.Add(item);
                    y -= num2;
                    y -= padding.y;
                    a = Mathf.Max(a, item.relativeSize.x * num2);
                    var listener = UIEventListener.Get(item.gameObject);
                    listener.onHover = new UIEventListener.BoolDelegate(OnItemHover);
                    listener.onPress = new UIEventListener.BoolDelegate(OnItemPress);
                    listener.parameter = key;
                    if (mSelectedItem == key)
                    {
                        Highlight(item, true);
                    }
                    mLabelList.Add(item);
                    num5++;
                }
                a = Mathf.Max(a, bounds.size.x - (border.x + padding.x) * 2f);
                var vector5 = new Vector3(a * 0.5f / num2, -0.5f, 0f);
                var vector6 = new Vector3(a / num2, (num2 + padding.y) / num2, 1f);
                var num7 = 0;
                var num8 = list.Count;
                while (num7 < num8)
                {
                    var label2 = list[num7];
                    var collider = NGUITools.AddWidgetCollider(label2.gameObject);
                    vector5.z = collider.center.z;
                    collider.center = vector5;
                    collider.size = vector6;
                    num7++;
                }
                a += (border.x + padding.x) * 2f;
                y -= border.y;
                mBackground.cachedTransform.localScale = new Vector3(a, -y + border.y, 1f);
                mHighlight.cachedTransform.localScale = new Vector3(a - (border.x + padding.x) * 2f + (atlasSprite.inner.xMin - atlasSprite.outer.xMin) * 2f, num2 + num * 2f, 1f);
                var placeAbove = position == Position.Above;
                if (position == Position.Auto)
                {
                    var camera = UICamera.FindCameraForLayer(gameObject.layer);
                    if (camera != null)
                    {
                        placeAbove = camera.cachedCamera.WorldToViewportPoint(child.position).y < 0.5f;
                    }
                }
                if (isAnimated)
                {
                    var bottom = y + num2;
                    Animate(mHighlight, placeAbove, bottom);
                    var num10 = 0;
                    var num11 = list.Count;
                    while (num10 < num11)
                    {
                        Animate(list[num10], placeAbove, bottom);
                        num10++;
                    }
                    AnimateColor(mBackground);
                    AnimateScale(mBackground, placeAbove, bottom);
                }
                if (placeAbove)
                {
                    transform.localPosition = new Vector3(bounds.min.x, bounds.max.y - y - border.y, bounds.min.z);
                }
            }
        }
        else
        {
            OnSelect(false);
        }
    }

    private void OnItemHover(GameObject go, bool isOver)
    {
        if (isOver)
        {
            var component = go.GetComponent<UILabel>();
            Highlight(component, false);
        }
    }

    private void OnItemPress(GameObject go, bool isPressed)
    {
        if (isPressed)
        {
            Select(go.GetComponent<UILabel>(), true);
        }
    }

    private void OnKey(KeyCode key)
    {
        if (enabled && NGUITools.GetActive(gameObject) && handleEvents)
        {
            var index = mLabelList.IndexOf(mHighlightedLabel);
            if (key == KeyCode.UpArrow)
            {
                if (index > 0)
                {
                    Select(mLabelList[--index], false);
                }
            }
            else if (key == KeyCode.DownArrow)
            {
                if (index + 1 < mLabelList.Count)
                {
                    Select(mLabelList[++index], false);
                }
            }
            else if (key == KeyCode.Escape)
            {
                OnSelect(false);
            }
        }
    }

    private void OnLocalize(Localization loc)
    {
        if (isLocalized && textLabel != null)
        {
            textLabel.text = loc.Get(mSelectedItem);
        }
    }

    private void OnSelect(bool isSelected)
    {
        if (!isSelected && mChild != null)
        {
            mLabelList.Clear();
            handleEvents = false;
            if (isAnimated)
            {
                var componentsInChildren = mChild.GetComponentsInChildren<UIWidget>();
                var index = 0;
                var length = componentsInChildren.Length;
                while (index < length)
                {
                    var widget = componentsInChildren[index];
                    var color = widget.color;
                    color.a = 0f;
                    TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
                    index++;
                }
                var colliderArray = mChild.GetComponentsInChildren<Collider>();
                var num3 = 0;
                var num4 = colliderArray.Length;
                while (num3 < num4)
                {
                    colliderArray[num3].enabled = false;
                    num3++;
                }
                Destroy(mChild, 0.15f);
            }
            else
            {
                Destroy(mChild);
            }
            mBackground = null;
            mHighlight = null;
            mChild = null;
        }
    }

    private void Select(UILabel lbl, bool instant)
    {
        Highlight(lbl, instant);
        var component = lbl.gameObject.GetComponent<UIEventListener>();
        selection = component.parameter as string;
        var components = GetComponents<UIButtonSound>();
        var index = 0;
        var length = components.Length;
        while (index < length)
        {
            var sound = components[index];
            if (sound.trigger == UIButtonSound.Trigger.OnClick)
            {
                NGUITools.PlaySound(sound.audioClip, sound.volume, 1f);
            }
            index++;
        }
    }

    private void Start()
    {
        if (textLabel != null)
        {
            if (string.IsNullOrEmpty(this.mSelectedItem))
            {
                if (items.Count > 0)
                {
                    selection = items[0];
                }
            }
            else
            {
                var mSelectedItem = this.mSelectedItem;
                this.mSelectedItem = null;
                selection = mSelectedItem;
            }
        }
    }

    private bool handleEvents
    {
        get
        {
            var component = GetComponent<UIButtonKeys>();
            return component == null || !component.enabled;
        }
        set
        {
            var component = GetComponent<UIButtonKeys>();
            if (component != null)
            {
                component.enabled = !value;
            }
        }
    }

    public bool isOpen
    {
        get
        {
            return mChild != null;
        }
    }

    public string selection
    {
        get
        {
            return mSelectedItem;
        }
        set
        {
            if (mSelectedItem != value)
            {
                mSelectedItem = value;
                if (textLabel != null)
                {
                    textLabel.text = !isLocalized ? value : Localization.Localize(value);
                }
                current = this;
                if (onSelectionChange != null)
                {
                    onSelectionChange(mSelectedItem);
                }
                if (eventReceiver != null && !string.IsNullOrEmpty(functionName) && Application.isPlaying)
                {
                    eventReceiver.SendMessage(functionName, mSelectedItem, SendMessageOptions.DontRequireReceiver);
                }
                current = null;
                if (textLabel == null)
                {
                    mSelectedItem = null;
                }
            }
        }
    }

    public delegate void OnSelectionChange(string item);

    public enum Position
    {
        Auto,
        Above,
        Below
    }
}

