//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class CharacterStatComponent : MonoBehaviour
{
    public GameObject manager;
    public CreateStat type;

    public void nextOption()
    {
        manager.GetComponent<CustomCharacterManager>().nextStatOption(type);
    }

    public void prevOption()
    {
        manager.GetComponent<CustomCharacterManager>().prevStatOption(type);
    }
}

