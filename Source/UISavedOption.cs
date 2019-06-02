using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
    public string keyName;
    private UICheckbox mCheck;
    private UIPopupList mList;

    private void Awake()
    {
        mList = GetComponent<UIPopupList>();
        mCheck = GetComponent<UICheckbox>();
        if (mList != null)
        {
            mList.onSelectionChange = (UIPopupList.OnSelectionChange) Delegate.Combine(mList.onSelectionChange,
                new UIPopupList.OnSelectionChange(SaveSelection));
        }

        if (mCheck != null)
        {
            mCheck.onStateChange =
                (UICheckbox.OnStateChange) Delegate.Combine(mCheck.onStateChange,
                    new UICheckbox.OnStateChange(SaveState));
        }
    }

    private void OnDestroy()
    {
        if (mCheck != null)
        {
            mCheck.onStateChange =
                (UICheckbox.OnStateChange) Delegate.Remove(mCheck.onStateChange,
                    new UICheckbox.OnStateChange(SaveState));
        }

        if (mList != null)
        {
            mList.onSelectionChange = (UIPopupList.OnSelectionChange) Delegate.Remove(mList.onSelectionChange,
                new UIPopupList.OnSelectionChange(SaveSelection));
        }
    }

    private void OnDisable()
    {
        if (mCheck == null && mList == null)
        {
            var componentsInChildren = GetComponentsInChildren<UICheckbox>(true);
            var index = 0;
            var length = componentsInChildren.Length;
            while (index < length)
            {
                var checkbox = componentsInChildren[index];
                if (checkbox.isChecked)
                {
                    SaveSelection(checkbox.name);
                    break;
                }

                index++;
            }
        }
    }

    private void OnEnable()
    {
        if (mList != null)
        {
            var str = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(str))
            {
                mList.selection = str;
            }
        }
        else if (mCheck != null)
        {
            mCheck.isChecked = PlayerPrefs.GetInt(key, 1) != 0;
        }
        else
        {
            var str2 = PlayerPrefs.GetString(key);
            var componentsInChildren = GetComponentsInChildren<UICheckbox>(true);
            var index = 0;
            var length = componentsInChildren.Length;
            while (index < length)
            {
                var checkbox = componentsInChildren[index];
                checkbox.isChecked = checkbox.name == str2;
                index++;
            }
        }
    }

    private void SaveSelection(string selection)
    {
        PlayerPrefs.SetString(key, selection);
    }

    private void SaveState(bool state)
    {
        PlayerPrefs.SetInt(key, !state ? 0 : 1);
    }

    private string key
    {
        get { return !string.IsNullOrEmpty(keyName) ? keyName : "NGUI State: " + name; }
    }
}