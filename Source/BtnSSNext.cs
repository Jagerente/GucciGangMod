//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BtnSSNext : MonoBehaviour
{
    private void OnClick()
    {
        if (gameObject.transform.parent.gameObject.GetComponent<CharacterCreationComponent>() != null)
        {
            gameObject.transform.parent.gameObject.GetComponent<CharacterCreationComponent>().nextOption();
        }
        else
        {
            gameObject.transform.parent.gameObject.GetComponent<CharacterStatComponent>().nextOption();
        }
    }
}

