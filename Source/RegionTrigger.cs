//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class RegionTrigger : MonoBehaviour
{
    public string myName;
    public RCEvent playerEventEnter;
    public RCEvent playerEventExit;
    public RCEvent titanEventEnter;
    public RCEvent titanEventExit;

    public void CopyTrigger(RegionTrigger copyTrigger)
    {
        playerEventEnter = copyTrigger.playerEventEnter;
        titanEventEnter = copyTrigger.titanEventEnter;
        playerEventExit = copyTrigger.playerEventExit;
        titanEventExit = copyTrigger.titanEventExit;
        myName = copyTrigger.myName;
    }

    private void OnTriggerEnter(Collider other)
    {
        string str;
        var gameObject = other.transform.gameObject;
        if (gameObject.layer == 8)
        {
            if (playerEventEnter != null)
            {
                var component = gameObject.GetComponent<HERO>();
                if (component != null)
                {
                    str = (string) FengGameManagerMKII.RCVariableNames["OnPlayerEnterRegion[" + myName + "]"];
                    if (FengGameManagerMKII.playerVariables.ContainsKey(str))
                    {
                        FengGameManagerMKII.playerVariables[str] = component.photonView.owner;
                    }
                    else
                    {
                        FengGameManagerMKII.playerVariables.Add(str, component.photonView.owner);
                    }
                    playerEventEnter.checkEvent();
                }
            }
        }
        else if ((gameObject.layer == 11) && (titanEventEnter != null))
        {
            var titan = gameObject.transform.root.gameObject.GetComponent<TITAN>();
            if (titan != null)
            {
                str = (string) FengGameManagerMKII.RCVariableNames["OnTitanEnterRegion[" + myName + "]"];
                if (FengGameManagerMKII.titanVariables.ContainsKey(str))
                {
                    FengGameManagerMKII.titanVariables[str] = titan;
                }
                else
                {
                    FengGameManagerMKII.titanVariables.Add(str, titan);
                }
                titanEventEnter.checkEvent();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string str;
        var gameObject = other.transform.root.gameObject;
        if (gameObject.layer == 8)
        {
            if (playerEventExit != null)
            {
                var component = gameObject.GetComponent<HERO>();
                if (component != null)
                {
                    str = (string) FengGameManagerMKII.RCVariableNames["OnPlayerLeaveRegion[" + myName + "]"];
                    if (FengGameManagerMKII.playerVariables.ContainsKey(str))
                    {
                        FengGameManagerMKII.playerVariables[str] = component.photonView.owner;
                    }
                    else
                    {
                        FengGameManagerMKII.playerVariables.Add(str, component.photonView.owner);
                    }
                }
            }
        }
        else if ((gameObject.layer == 11) && (titanEventExit != null))
        {
            var titan = gameObject.GetComponent<TITAN>();
            if (titan != null)
            {
                str = (string) FengGameManagerMKII.RCVariableNames["OnTitanLeaveRegion[" + myName + "]"];
                if (FengGameManagerMKII.titanVariables.ContainsKey(str))
                {
                    FengGameManagerMKII.titanVariables[str] = titan;
                }
                else
                {
                    FengGameManagerMKII.titanVariables.Add(str, titan);
                }
                titanEventExit.checkEvent();
            }
        }
    }
}

