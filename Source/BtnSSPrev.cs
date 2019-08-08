using UnityEngine;

public class BtnSSPrev : MonoBehaviour
{
    private void OnClick()
    {
        if (gameObject.transform.parent.gameObject.GetComponent<CharacterCreationComponent>() != null)
        {
            gameObject.transform.parent.gameObject.GetComponent<CharacterCreationComponent>().prevOption();
        }
        else
        {
            gameObject.transform.parent.gameObject.GetComponent<CharacterStatComponent>().prevOption();
        }
    }
}