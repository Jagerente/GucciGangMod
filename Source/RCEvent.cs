using System.Collections.Generic;

public class RCEvent
{
    private RCCondition condition;
    private RCAction elseAction;
    private int eventClass;
    private int eventType;
    public string foreachVariableName;
    public List<RCAction> trueActions;

    public RCEvent(RCCondition sentCondition, List<RCAction> sentTrueActions, int sentClass, int sentType)
    {
        condition = sentCondition;
        trueActions = sentTrueActions;
        eventClass = sentClass;
        eventType = sentType;
    }

    public void checkEvent()
    {
        int num2;
        switch (eventClass)
        {
            case 0:
                for (num2 = 0; num2 < trueActions.Count; num2++)
                {
                    trueActions[num2].doAction();
                }

                break;

            case 1:
                if (!condition.checkCondition())
                {
                    elseAction?.doAction();
                    break;
                }

                for (num2 = 0; num2 < trueActions.Count; num2++)
                {
                    trueActions[num2].doAction();
                }

                break;

            case 2:
                switch (eventType)
                {
                    case 0:
                        foreach (TITAN titan in FengGameManagerMKII.FGM.getTitans())
                        {
                            if (FengGameManagerMKII.titanVariables.ContainsKey(foreachVariableName))
                            {
                                FengGameManagerMKII.titanVariables[foreachVariableName] = titan;
                            }
                            else
                            {
                                FengGameManagerMKII.titanVariables.Add(foreachVariableName, titan);
                            }

                            foreach (var action in trueActions)
                            {
                                action.doAction();
                            }
                        }

                        return;

                    case 1:
                        foreach (var player in PhotonNetwork.playerList)
                        {
                            if (FengGameManagerMKII.playerVariables.ContainsKey(foreachVariableName))
                            {
                                FengGameManagerMKII.playerVariables[foreachVariableName] = player;
                            }
                            else
                            {
                                FengGameManagerMKII.titanVariables.Add(foreachVariableName, player);
                            }

                            foreach (var action in trueActions)
                            {
                                action.doAction();
                            }
                        }

                        return;
                }

                break;

            case 3:
                while (condition.checkCondition())
                {
                    foreach (var action in trueActions)
                    {
                        action.doAction();
                    }
                }

                break;
        }
    }

    public void setElse(RCAction sentElse)
    {
        elseAction = sentElse;
    }

    public enum foreachType
    {
        titan,
        player
    }

    public enum loopType
    {
        noLoop,
        ifLoop,
        foreachLoop,
        whileLoop
    }
}