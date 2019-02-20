//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class StyledComboBox : StyledItem
{
    public StyledComboBoxPrefab containerPrefab;
    private bool isToggled;
    public StyledItem itemMenuPrefab;
    public StyledItem itemPrefab;
    [HideInInspector, SerializeField]
    private List<StyledItem> items = new List<StyledItem>();
    public SelectionChangedHandler OnSelectionChanged;
    [HideInInspector, SerializeField]
    private StyledComboBoxPrefab root;
    [SerializeField]
    private int selectedIndex;

    private void AddItem(object data)
    {
        if (itemPrefab != null)
        {
            var yf = new AddItemcAnonStoreyF {
                fthis = this
            };
            var fourCornersArray = new Vector3[4];
            itemPrefab.GetComponent<RectTransform>().GetLocalCorners(fourCornersArray);
            var position = fourCornersArray[0];
            var num = position.y - fourCornersArray[2].y;
            position.y = items.Count * num;
            yf.styledItem = Instantiate(itemPrefab, position, root.itemRoot.rotation) as StyledItem;
            var component = yf.styledItem.GetComponent<RectTransform>();
            yf.styledItem.Populate(data);
            component.SetParent(root.itemRoot.transform, false);
            component.pivot = new Vector2(0f, 1f);
            component.anchorMin = new Vector2(0f, 1f);
            component.anchorMax = Vector2.one;
            component.anchoredPosition = new Vector2(0f, position.y);
            items.Add(yf.styledItem);
            component.offsetMin = new Vector2(0f, position.y + num);
            component.offsetMax = new Vector2(0f, position.y);
            root.itemRoot.offsetMin = new Vector2(root.itemRoot.offsetMin.x, (items.Count + 2) * num);
            var button = yf.styledItem.GetButton();
            yf.curIndex = items.Count - 1;
            if (button != null)
            {
                button.onClick.AddListener(new UnityAction(yf.m0));
            }
        }
    }

    public void AddItems(params object[] list)
    {
        ClearItems();
        for (var i = 0; i < list.Length; i++)
        {
            AddItem(list[i]);
        }
        SelectedIndex = 0;
    }

    private void Awake()
    {
        InitControl();
    }

    public void ClearItems()
    {
        for (var i = items.Count - 1; i >= 0; i--)
        {
            DestroyObject(items[i].gameObject);
        }
    }

    private void CreateMenuButton(object data)
    {
        if (root.menuItem.transform.childCount > 0)
        {
            for (var i = root.menuItem.transform.childCount - 1; i >= 0; i--)
            {
                DestroyObject(root.menuItem.transform.GetChild(i).gameObject);
            }
        }
        if ((itemMenuPrefab != null) && (root.menuItem != null))
        {
            var item = Instantiate(itemMenuPrefab) as StyledItem;
            item.Populate(data);
            item.transform.SetParent(root.menuItem.transform, false);
            var component = item.GetComponent<RectTransform>();
            component.pivot = new Vector2(0.5f, 0.5f);
            component.anchorMin = Vector2.zero;
            component.anchorMax = Vector2.one;
            component.offsetMin = Vector2.zero;
            component.offsetMax = Vector2.zero;
            root.gameObject.hideFlags = HideFlags.HideInHierarchy;
            var button = item.GetButton();
            if (button != null)
            {
                button.onClick.AddListener(new UnityAction(TogglePanelState));
            }
        }
    }

    public void InitControl()
    {
        if (root != null)
        {
            DestroyImmediate(root.gameObject);
        }
        if (containerPrefab != null)
        {
            var component = GetComponent<RectTransform>();
            root = Instantiate(containerPrefab, component.position, component.rotation) as StyledComboBoxPrefab;
            root.transform.SetParent(transform, false);
            var transform2 = root.GetComponent<RectTransform>();
            transform2.pivot = new Vector2(0.5f, 0.5f);
            transform2.anchorMin = Vector2.zero;
            transform2.anchorMax = Vector2.one;
            transform2.offsetMax = Vector2.zero;
            transform2.offsetMin = Vector2.zero;
            root.gameObject.hideFlags = HideFlags.HideInHierarchy;
            root.itemPanel.gameObject.SetActive(isToggled);
        }
    }

    public void OnItemClicked(StyledItem item, int index)
    {
        SelectedIndex = index;
        TogglePanelState();
        if (OnSelectionChanged != null)
        {
            OnSelectionChanged(item);
        }
    }

    public void TogglePanelState()
    {
        isToggled = !isToggled;
        root.itemPanel.gameObject.SetActive(isToggled);
    }

    public int SelectedIndex
    {
        get
        {
            return selectedIndex;
        }
        set
        {
            if ((value >= 0) && (value <= items.Count))
            {
                selectedIndex = value;
                CreateMenuButton(items[selectedIndex].GetText().text);
            }
        }
    }

    public StyledItem SelectedItem
    {
        get
        {
            if ((selectedIndex >= 0) && (selectedIndex <= items.Count))
            {
                return items[selectedIndex];
            }
            return null;
        }
    }

    [CompilerGenerated]
    private sealed class AddItemcAnonStoreyF
    {
        internal StyledComboBox fthis;
        internal int curIndex;
        internal StyledItem styledItem;

        internal void m0()
        {
            fthis.OnItemClicked(styledItem, curIndex);
        }
    }

    public delegate void SelectionChangedHandler(StyledItem item);
}

