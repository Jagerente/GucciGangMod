using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Object")]
public class UICheckboxControlledObject : MonoBehaviour
{
    public bool inverse;
    public GameObject target;

    private void OnActivate(bool isActive)
    {
        if (target != null)
        {
            NGUITools.SetActive(target, !inverse ? isActive : !isActive);
            UIPanel panel = NGUITools.FindInParents<UIPanel>(target);
            if (panel != null)
            {
                panel.Refresh();
            }
        }
    }

    private void OnEnable()
    {
        var component = GetComponent<UICheckbox>();
        if (component != null)
        {
            OnActivate(component.isChecked);
        }
    }
}

