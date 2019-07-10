using UnityEngine;

public class CB_cameraTilt : MonoBehaviour
{
    private bool init;

    private void OnActivate(bool result)
    {
        if (!init)
        {
            init = true;
            if (PlayerPrefs.HasKey("cameraTilt"))
            {
                gameObject.GetComponent<UICheckbox>().isChecked = PlayerPrefs.GetInt("cameraTilt") == 1;
            }
            else
            {
                PlayerPrefs.SetInt("cameraTilt", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("cameraTilt", !result ? 0 : 1);
        }
    }
}

