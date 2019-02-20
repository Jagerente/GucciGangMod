//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class TestStyledComboBox : MonoBehaviour
{
    public StyledComboBox comboBox;

    private void Start()
    {
        var list = new object[] { "English", "简体中文", "繁體中文", "繁體中文", "繁體中文", "繁體中文", "繁體中文", "繁體中文", "繁體中文", "繁體中文", "繁體中文" };
        comboBox.AddItems(list);
    }
}

