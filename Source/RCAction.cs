using System;
using ExitGames.Client.Photon;
using Random = UnityEngine.Random;

public class RCAction
{
    private int actionClass;
    private int actionType;
    private RCEvent nextEvent;
    private RCActionHelper[] parameters;

    public RCAction(int category, int type, RCEvent next, RCActionHelper[] helpers)
    {
        actionClass = category;
        actionType = type;
        nextEvent = next;
        parameters = helpers;
    }

    public void callException(string str)
    {
        InRoomChat.SystemMessageLocal(str, false);
    }

    public void doAction()
    {
        switch (actionClass)
        {
            case 0:
                nextEvent.checkEvent();
                break;

            case 1:
            {
                var key = parameters[0].returnString(null);
                var num2 = parameters[1].returnInt(null);
                switch (actionType)
                {
                    case 0:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            FengGameManagerMKII.intVariables.Add(key, num2);
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = num2;
                        }
                        return;

                    case 1:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            callException("Variable not found: " + key);
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = (int) FengGameManagerMKII.intVariables[key] + num2;
                        }
                        return;

                    case 2:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            callException("Variable not found: " + key);
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = (int) FengGameManagerMKII.intVariables[key] - num2;
                        }
                        return;

                    case 3:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            callException("Variable not found: " + key);
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = (int) FengGameManagerMKII.intVariables[key] * num2;
                        }
                        return;

                    case 4:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            callException("Variable not found: " + key);
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = (int) FengGameManagerMKII.intVariables[key] / num2;
                        }
                        return;

                    case 5:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            callException("Variable not found: " + key);
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = (int) FengGameManagerMKII.intVariables[key] % num2;
                        }
                        return;

                    case 6:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            callException("Variable not found: " + key);
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = (int) Math.Pow((int) FengGameManagerMKII.intVariables[key], num2);
                        }
                        return;

                    case 12:
                        if (!FengGameManagerMKII.intVariables.ContainsKey(key))
                        {
                            FengGameManagerMKII.intVariables.Add(key, Random.Range(num2, parameters[2].returnInt(null)));
                        }
                        else
                        {
                            FengGameManagerMKII.intVariables[key] = Random.Range(num2, parameters[2].returnInt(null));
                        }
                        return;
                }
                break;
            }
            case 2:
            {
                var str2 = parameters[0].returnString(null);
                var flag2 = parameters[1].returnBool(null);
                switch (actionType)
                {
                    case 11:
                        if (!FengGameManagerMKII.boolVariables.ContainsKey(str2))
                        {
                            callException("Variable not found: " + str2);
                        }
                        else
                        {
                            FengGameManagerMKII.boolVariables[str2] = !(bool) FengGameManagerMKII.boolVariables[str2];
                        }
                        return;

                    case 12:
                        if (!FengGameManagerMKII.boolVariables.ContainsKey(str2))
                        {
                            FengGameManagerMKII.boolVariables.Add(str2, Convert.ToBoolean(Random.Range(0, 2)));
                        }
                        else
                        {
                            FengGameManagerMKII.boolVariables[str2] = Convert.ToBoolean(Random.Range(0, 2));
                        }
                        return;

                    case 0:
                        if (!FengGameManagerMKII.boolVariables.ContainsKey(str2))
                        {
                            FengGameManagerMKII.boolVariables.Add(str2, flag2);
                        }
                        else
                        {
                            FengGameManagerMKII.boolVariables[str2] = flag2;
                        }
                        return;
                }
                break;
            }
            case 3:
            {
                var str3 = parameters[0].returnString(null);
                switch (actionType)
                {
                    case 7:
                    {
                        var str5 = string.Empty;
                        for (var i = 1; i < parameters.Length; i++)
                        {
                            str5 = str5 + parameters[i].returnString(null);
                        }
                        if (!FengGameManagerMKII.stringVariables.ContainsKey(str3))
                        {
                            FengGameManagerMKII.stringVariables.Add(str3, str5);
                        }
                        else
                        {
                            FengGameManagerMKII.stringVariables[str3] = str5;
                        }
                        return;
                    }
                    case 8:
                    {
                        var str6 = parameters[1].returnString(null);
                        if (!FengGameManagerMKII.stringVariables.ContainsKey(str3))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.stringVariables[str3] = (string) FengGameManagerMKII.stringVariables[str3] + str6;
                        }
                        return;
                    }
                    case 9:
                    {
                        var str7 = parameters[1].returnString(null);
                        if (!FengGameManagerMKII.stringVariables.ContainsKey(str3))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.stringVariables[str3] = ((string) FengGameManagerMKII.stringVariables[str3]).Replace(parameters[1].returnString(null), parameters[2].returnString(null));
                        }
                        return;
                    }
                    case 0:
                    {
                        var str4 = parameters[1].returnString(null);
                        if (!FengGameManagerMKII.stringVariables.ContainsKey(str3))
                        {
                            FengGameManagerMKII.stringVariables.Add(str3, str4);
                        }
                        else
                        {
                            FengGameManagerMKII.stringVariables[str3] = str4;
                        }
                        return;
                    }
                }
                break;
            }
            case 4:
            {
                var str9 = parameters[0].returnString(null);
                var num4 = parameters[1].returnFloat(null);
                switch (actionType)
                {
                    case 0:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            FengGameManagerMKII.floatVariables.Add(str9, num4);
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = num4;
                        }
                        return;

                    case 1:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = (float) FengGameManagerMKII.floatVariables[str9] + num4;
                        }
                        return;

                    case 2:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = (float) FengGameManagerMKII.floatVariables[str9] - num4;
                        }
                        return;

                    case 3:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = (float) FengGameManagerMKII.floatVariables[str9] * num4;
                        }
                        return;

                    case 4:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = (float) FengGameManagerMKII.floatVariables[str9] / num4;
                        }
                        return;

                    case 5:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = (float) FengGameManagerMKII.floatVariables[str9] % num4;
                        }
                        return;

                    case 6:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            callException("No Variable");
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = (float) Math.Pow((int) FengGameManagerMKII.floatVariables[str9], num4);
                        }
                        return;

                    case 12:
                        if (!FengGameManagerMKII.floatVariables.ContainsKey(str9))
                        {
                            FengGameManagerMKII.floatVariables.Add(str9, Random.Range(num4, parameters[2].returnFloat(null)));
                        }
                        else
                        {
                            FengGameManagerMKII.floatVariables[str9] = Random.Range(num4, parameters[2].returnFloat(null));
                        }
                        return;
                }
                break;
            }
            case 5:
            {
                var str10 = parameters[0].returnString(null);
                var player = parameters[1].returnPlayer(null);
                if (actionType == 0)
                {
                    if (!FengGameManagerMKII.playerVariables.ContainsKey(str10))
                    {
                        FengGameManagerMKII.playerVariables.Add(str10, player);
                    }
                    else
                    {
                        FengGameManagerMKII.playerVariables[str10] = player;
                    }
                }
                break;
            }
            case 6:
            {
                var str11 = parameters[0].returnString(null);
                var titan = parameters[1].returnTitan(null);
                if (actionType == 0)
                {
                    if (!FengGameManagerMKII.titanVariables.ContainsKey(str11))
                    {
                        FengGameManagerMKII.titanVariables.Add(str11, titan);
                    }
                    else
                    {
                        FengGameManagerMKII.titanVariables[str11] = titan;
                    }
                }
                break;
            }
            case 7:
            {
                var targetPlayer = parameters[0].returnPlayer(null);
                switch (actionType)
                {
                    case 0:
                    {
                        var iD = targetPlayer.ID;
                        if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                        {
                            var hero = (HERO) FengGameManagerMKII.heroHash[iD];
                            hero.markDie();
                            hero.photonView.RPC("netDie2", PhotonTargets.All, -1, parameters[1].returnString(null) + " ");
                        }
                        else
                        {
                            callException("Player Not Alive");
                        }
                        return;
                    }
                    case 1:
                        FengGameManagerMKII.FGM.photonView.RPC("respawnHeroInNewRound", targetPlayer);
                        return;

                    case 2:
                        FengGameManagerMKII.FGM.photonView.RPC("spawnPlayerAtRPC", targetPlayer, parameters[1].returnFloat(null), parameters[2].returnFloat(null), parameters[3].returnFloat(null));
                        return;

                    case 3:
                    {
                        var num6 = targetPlayer.ID;
                        if (FengGameManagerMKII.heroHash.ContainsKey(num6))
                        {
                            var hero2 = (HERO) FengGameManagerMKII.heroHash[num6];
                            hero2.photonView.RPC("moveToRPC", targetPlayer, parameters[1].returnFloat(null), parameters[2].returnFloat(null), parameters[3].returnFloat(null));
                        }
                        else
                        {
                            callException("Player Not Alive");
                        }
                        return;
                    }
                    case 4:
                    {
                        var propertiesToSet = new Hashtable();
                        propertiesToSet.Add(PhotonPlayerProperty.kills, parameters[1].returnInt(null));
                        targetPlayer.SetCustomProperties(propertiesToSet);
                        return;
                    }
                    case 5:
                    {
                        var hashtable2 = new Hashtable();
                        hashtable2.Add(PhotonPlayerProperty.deaths, parameters[1].returnInt(null));
                        targetPlayer.SetCustomProperties(hashtable2);
                        return;
                    }
                    case 6:
                    {
                        var hashtable3 = new Hashtable();
                        hashtable3.Add(PhotonPlayerProperty.max_dmg, parameters[1].returnInt(null));
                        targetPlayer.SetCustomProperties(hashtable3);
                        return;
                    }
                    case 7:
                    {
                        var hashtable4 = new Hashtable();
                        hashtable4.Add(PhotonPlayerProperty.total_dmg, parameters[1].returnInt(null));
                        targetPlayer.SetCustomProperties(hashtable4);
                        return;
                    }
                    case 8:
                    {
                        var hashtable5 = new Hashtable();
                        hashtable5.Add(PhotonPlayerProperty.name, parameters[1].returnString(null));
                        targetPlayer.SetCustomProperties(hashtable5);
                        return;
                    }
                    case 9:
                    {
                        var hashtable6 = new Hashtable();
                        hashtable6.Add(PhotonPlayerProperty.guildName, parameters[1].returnString(null));
                        targetPlayer.SetCustomProperties(hashtable6);
                        return;
                    }
                    case 10:
                    {
                        var hashtable7 = new Hashtable();
                        hashtable7.Add(PhotonPlayerProperty.RCteam, parameters[1].returnInt(null));
                        targetPlayer.SetCustomProperties(hashtable7);
                        return;
                    }
                    case 11:
                    {
                        var hashtable8 = new Hashtable();
                        hashtable8.Add(PhotonPlayerProperty.customInt, parameters[1].returnInt(null));
                        targetPlayer.SetCustomProperties(hashtable8);
                        return;
                    }
                    case 12:
                    {
                        var hashtable9 = new Hashtable();
                        hashtable9.Add(PhotonPlayerProperty.customBool, parameters[1].returnBool(null));
                        targetPlayer.SetCustomProperties(hashtable9);
                        return;
                    }
                    case 13:
                    {
                        var hashtable10 = new Hashtable();
                        hashtable10.Add(PhotonPlayerProperty.customString, parameters[1].returnString(null));
                        targetPlayer.SetCustomProperties(hashtable10);
                        return;
                    }
                    case 14:
                    {
                        var hashtable11 = new Hashtable();
                        hashtable11.Add(PhotonPlayerProperty.RCteam, parameters[1].returnFloat(null));
                        targetPlayer.SetCustomProperties(hashtable11);
                        return;
                    }
                }
                break;
            }
            case 8:
                switch (actionType)
                {
                    case 0:
                    {
                        var titan2 = this.parameters[0].returnTitan(null);
                        object[] parameters = { this.parameters[1].returnPlayer(null).ID, this.parameters[2].returnInt(null) };
                        titan2.photonView.RPC("titanGetHit", titan2.photonView.owner, parameters);
                        return;
                    }
                    case 1:
                        FengGameManagerMKII.FGM.spawnTitanAction(this.parameters[0].returnInt(null), this.parameters[1].returnFloat(null), this.parameters[2].returnInt(null), this.parameters[3].returnInt(null));
                        return;

                    case 2:
                        FengGameManagerMKII.FGM.spawnTitanAtAction(this.parameters[0].returnInt(null), this.parameters[1].returnFloat(null), this.parameters[2].returnInt(null), this.parameters[3].returnInt(null), this.parameters[4].returnFloat(null), this.parameters[5].returnFloat(null), this.parameters[6].returnFloat(null));
                        return;

                    case 3:
                    {
                        var titan3 = parameters[0].returnTitan(null);
                        var num7 = parameters[1].returnInt(null);
                        titan3.currentHealth = num7;
                        if (titan3.maxHealth == 0)
                        {
                            titan3.maxHealth = titan3.currentHealth;
                        }
                        titan3.photonView.RPC("labelRPC", PhotonTargets.AllBuffered, titan3.currentHealth, titan3.maxHealth);
                        return;
                    }
                    case 4:
                    {
                        var titan4 = parameters[0].returnTitan(null);
                        if (titan4.photonView.isMine)
                        {
                            titan4.moveTo(parameters[1].returnFloat(null), parameters[2].returnFloat(null), parameters[3].returnFloat(null));
                        }
                        else
                        {
                            titan4.photonView.RPC("moveToRPC", titan4.photonView.owner, parameters[1].returnFloat(null), parameters[2].returnFloat(null), parameters[3].returnFloat(null));
                        }
                        return;
                    }
                }
                break;

            case 9:
                switch (actionType)
                {
                    case 0:
                        FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.All, this.parameters[0].returnString(null), string.Empty);
                        return;

                    case 1:
                        FengGameManagerMKII.FGM.gameWin();
                        if (this.parameters[0].returnBool(null))
                        {
                            FengGameManagerMKII.intVariables.Clear();
                            FengGameManagerMKII.boolVariables.Clear();
                            FengGameManagerMKII.stringVariables.Clear();
                            FengGameManagerMKII.floatVariables.Clear();
                            FengGameManagerMKII.playerVariables.Clear();
                            FengGameManagerMKII.titanVariables.Clear();
                        }
                        return;

                    case 2:
                        FengGameManagerMKII.FGM.gameLose();
                        if (this.parameters[0].returnBool(null))
                        {
                            FengGameManagerMKII.intVariables.Clear();
                            FengGameManagerMKII.boolVariables.Clear();
                            FengGameManagerMKII.stringVariables.Clear();
                            FengGameManagerMKII.floatVariables.Clear();
                            FengGameManagerMKII.playerVariables.Clear();
                            FengGameManagerMKII.titanVariables.Clear();
                        }
                        return;

                    case 3:
                        if (this.parameters[0].returnBool(null))
                        {
                            FengGameManagerMKII.intVariables.Clear();
                            FengGameManagerMKII.boolVariables.Clear();
                            FengGameManagerMKII.stringVariables.Clear();
                            FengGameManagerMKII.floatVariables.Clear();
                            FengGameManagerMKII.playerVariables.Clear();
                            FengGameManagerMKII.titanVariables.Clear();
                        }
                        FengGameManagerMKII.FGM.restartGame(false);
                        return;
                }
                break;
        }
    }

    public enum actionClasses
    {
        typeVoid,
        typeVariableInt,
        typeVariableBool,
        typeVariableString,
        typeVariableFloat,
        typeVariablePlayer,
        typeVariableTitan,
        typePlayer,
        typeTitan,
        typeGame
    }

    public enum gameTypes
    {
        printMessage,
        winGame,
        loseGame,
        restartGame
    }

    public enum playerTypes
    {
        killPlayer,
        spawnPlayer,
        spawnPlayerAt,
        movePlayer,
        setKills,
        setDeaths,
        setMaxDmg,
        setTotalDmg,
        setName,
        setGuildName,
        setTeam,
        setCustomInt,
        setCustomBool,
        setCustomString,
        setCustomFloat
    }

    public enum titanTypes
    {
        killTitan,
        spawnTitan,
        spawnTitanAt,
        setHealth,
        moveTitan
    }

    public enum varTypes
    {
        set,
        add,
        subtract,
        multiply,
        divide,
        modulo,
        power,
        concat,
        append,
        remove,
        replace,
        toOpposite,
        setRandom
    }
}

