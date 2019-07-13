using UnityEngine;

public class PopuplistCharacterSelection : MonoBehaviour
{
    public GameObject ACL;
    public GameObject BLA;
    public GameObject GAS;
    public GameObject SPD;

    private void onCharacterChange()
    {
        HeroStat stat;
        var selection = GetComponent<UIPopupList>().selection;
        switch (selection)
        {
            case "Set 1":
            case "Set 2":
            case "Set 3":
                {
                    var costume = CostumeConeveter.LocalDataToHeroCostume(selection.ToUpper());
                    if (costume == null)
                    {
                        stat = new HeroStat();
                    }
                    else
                    {
                        stat = costume.stat;
                    }
                    break;
                }
            default:
                stat = HeroStat.getInfo(GetComponent<UIPopupList>().selection);
                break;
        }
        SPD.transform.localScale = new Vector3(stat.SPD, 20f, 0f);
        GAS.transform.localScale = new Vector3(stat.GAS, 20f, 0f);
        BLA.transform.localScale = new Vector3(stat.BLA, 20f, 0f);
        ACL.transform.localScale = new Vector3(stat.ACL, 20f, 0f);
    }
}