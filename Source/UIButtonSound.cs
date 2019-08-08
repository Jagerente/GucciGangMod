using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Sound")]
public class UIButtonSound : MonoBehaviour
{
    public AudioClip audioClip;
    public float pitch = 1f;
    public Trigger trigger;
    public float volume = 1f;

    private void OnClick()
    {
        if (enabled && trigger == Trigger.OnClick)
        {
            NGUITools.PlaySound(audioClip, volume, pitch);
        }
    }

    private void OnHover(bool isOver)
    {
        if (enabled && (isOver && trigger == Trigger.OnMouseOver || !isOver && trigger == Trigger.OnMouseOut))
        {
            NGUITools.PlaySound(audioClip, volume, pitch);
        }
    }

    private void OnPress(bool isPressed)
    {
        if (enabled && (isPressed && trigger == Trigger.OnPress || !isPressed && trigger == Trigger.OnRelease))
        {
            NGUITools.PlaySound(audioClip, volume, pitch);
        }
    }

    public enum Trigger
    {
        OnClick,
        OnMouseOver,
        OnMouseOut,
        OnPress,
        OnRelease
    }
}