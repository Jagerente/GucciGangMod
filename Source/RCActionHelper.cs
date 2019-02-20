//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;

public class RCActionHelper
{
    public int helperClass;
    public int helperType;
    private RCActionHelper nextHelper;
    private object parameters;

    public RCActionHelper(int sentClass, int sentType, object options)
    {
        helperClass = sentClass;
        helperType = sentType;
        parameters = options;
    }

    public void callException(string str)
    {
        FengGameManagerMKII.instance.chatRoom.AddLine(str);
    }

    public bool returnBool(object sentObject)
    {
        var parameters = sentObject;
        if (this.parameters != null)
        {
            parameters = this.parameters;
        }
        switch (helperClass)
        {
            case 0:
                return (bool) parameters;

            case 1:
            {
                var helper = (RCActionHelper) parameters;
                switch (helperType)
                {
                    case 0:
                        return nextHelper.returnBool(FengGameManagerMKII.intVariables[helper.returnString(null)]);

                    case 1:
                        return (bool) FengGameManagerMKII.boolVariables[helper.returnString(null)];

                    case 2:
                        return nextHelper.returnBool(FengGameManagerMKII.stringVariables[helper.returnString(null)]);

                    case 3:
                        return nextHelper.returnBool(FengGameManagerMKII.floatVariables[helper.returnString(null)]);

                    case 4:
                        return nextHelper.returnBool(FengGameManagerMKII.playerVariables[helper.returnString(null)]);

                    case 5:
                        return nextHelper.returnBool(FengGameManagerMKII.titanVariables[helper.returnString(null)]);
                }
                return false;
            }
            case 2:
            {
                var player = (PhotonPlayer) parameters;
                if (player != null)
                {
                    HERO hero;
                    switch (helperType)
                    {
                        case 0:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.team]);

                        case 1:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.RCteam]);

                        case 2:
                            return !((bool) player.customProperties[PhotonPlayerProperty.dead]);

                        case 3:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.isTitan]);

                        case 4:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.kills]);

                        case 5:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.deaths]);

                        case 6:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.max_dmg]);

                        case 7:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.total_dmg]);

                        case 8:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.customInt]);

                        case 9:
                            return (bool) player.customProperties[PhotonPlayerProperty.customBool];

                        case 10:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.customString]);

                        case 11:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.customFloat]);

                        case 12:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.name]);

                        case 13:
                            return nextHelper.returnBool(player.customProperties[PhotonPlayerProperty.guildName]);

                        case 14:
                        {
                            var iD = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[iD];
                                return nextHelper.returnBool(hero.transform.position.x);
                            }
                            return false;
                        }
                        case 15:
                        {
                            var key = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(key))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[key];
                                return nextHelper.returnBool(hero.transform.position.y);
                            }
                            return false;
                        }
                        case 0x10:
                        {
                            var num6 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num6))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num6];
                                return nextHelper.returnBool(hero.transform.position.z);
                            }
                            return false;
                        }
                        case 0x11:
                        {
                            var num7 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num7))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num7];
                                return nextHelper.returnBool(hero.rigidbody.velocity.magnitude);
                            }
                            return false;
                        }
                    }
                }
                return false;
            }
            case 3:
            {
                var titan = (TITAN) parameters;
                if (titan != null)
                {
                    switch (helperType)
                    {
                        case 0:
                            return nextHelper.returnBool(titan.abnormalType);

                        case 1:
                            return nextHelper.returnBool(titan.myLevel);

                        case 2:
                            return nextHelper.returnBool(titan.currentHealth);

                        case 3:
                            return nextHelper.returnBool(titan.transform.position.x);

                        case 4:
                            return nextHelper.returnBool(titan.transform.position.y);

                        case 5:
                            return nextHelper.returnBool(titan.transform.position.z);
                    }
                }
                return false;
            }
            case 4:
            {
                var helper2 = (RCActionHelper) parameters;
                var region = (RCRegion) FengGameManagerMKII.RCRegions[helper2.returnString(null)];
                switch (helperType)
                {
                    case 0:
                        return nextHelper.returnBool(region.GetRandomX());

                    case 1:
                        return nextHelper.returnBool(region.GetRandomY());

                    case 2:
                        return nextHelper.returnBool(region.GetRandomZ());
                }
                return false;
            }
            case 5:
                switch (helperType)
                {
                    case 0:
                    {
                        var num2 = (int) parameters;
                        return Convert.ToBoolean(num2);
                    }
                    case 1:
                        return (bool) parameters;

                    case 2:
                    {
                        var str = (string) parameters;
                        return Convert.ToBoolean(str);
                    }
                    case 3:
                    {
                        var num3 = (float) parameters;
                        return Convert.ToBoolean(num3);
                    }
                }
                return false;
        }
        return false;
    }

    public float returnFloat(object sentObject)
    {
        var parameters = sentObject;
        if (this.parameters != null)
        {
            parameters = this.parameters;
        }
        switch (helperClass)
        {
            case 0:
                return (float) parameters;

            case 1:
            {
                var helper = (RCActionHelper) parameters;
                switch (helperType)
                {
                    case 0:
                        return nextHelper.returnFloat(FengGameManagerMKII.intVariables[helper.returnString(null)]);

                    case 1:
                        return nextHelper.returnFloat(FengGameManagerMKII.boolVariables[helper.returnString(null)]);

                    case 2:
                        return nextHelper.returnFloat(FengGameManagerMKII.stringVariables[helper.returnString(null)]);

                    case 3:
                        return (float) FengGameManagerMKII.floatVariables[helper.returnString(null)];

                    case 4:
                        return nextHelper.returnFloat(FengGameManagerMKII.playerVariables[helper.returnString(null)]);

                    case 5:
                        return nextHelper.returnFloat(FengGameManagerMKII.titanVariables[helper.returnString(null)]);
                }
                return 0f;
            }
            case 2:
            {
                var player = (PhotonPlayer) parameters;
                if (player != null)
                {
                    HERO hero;
                    switch (helperType)
                    {
                        case 0:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.team]);

                        case 1:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.RCteam]);

                        case 2:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.dead]);

                        case 3:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.isTitan]);

                        case 4:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.kills]);

                        case 5:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.deaths]);

                        case 6:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.max_dmg]);

                        case 7:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.total_dmg]);

                        case 8:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.customInt]);

                        case 9:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.customBool]);

                        case 10:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.customString]);

                        case 11:
                            return (float) player.customProperties[PhotonPlayerProperty.customFloat];

                        case 12:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.name]);

                        case 13:
                            return nextHelper.returnFloat(player.customProperties[PhotonPlayerProperty.guildName]);

                        case 14:
                        {
                            var iD = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[iD];
                                return hero.transform.position.x;
                            }
                            return 0f;
                        }
                        case 15:
                        {
                            var key = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(key))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[key];
                                return hero.transform.position.y;
                            }
                            return 0f;
                        }
                        case 0x10:
                        {
                            var num7 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num7))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num7];
                                return hero.transform.position.z;
                            }
                            return 0f;
                        }
                        case 0x11:
                        {
                            var num8 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num8))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num8];
                                return hero.rigidbody.velocity.magnitude;
                            }
                            return 0f;
                        }
                    }
                }
                return 0f;
            }
            case 3:
            {
                var titan = (TITAN) parameters;
                if (titan != null)
                {
                    switch (helperType)
                    {
                        case 0:
                            return nextHelper.returnFloat(titan.abnormalType);

                        case 1:
                            return titan.myLevel;

                        case 2:
                            return nextHelper.returnFloat(titan.currentHealth);

                        case 3:
                            return titan.transform.position.x;

                        case 4:
                            return titan.transform.position.y;

                        case 5:
                            return titan.transform.position.z;
                    }
                }
                return 0f;
            }
            case 4:
            {
                var helper2 = (RCActionHelper) parameters;
                var region = (RCRegion) FengGameManagerMKII.RCRegions[helper2.returnString(null)];
                switch (helperType)
                {
                    case 0:
                        return region.GetRandomX();

                    case 1:
                        return region.GetRandomY();

                    case 2:
                        return region.GetRandomZ();
                }
                return 0f;
            }
            case 5:
                switch (helperType)
                {
                    case 0:
                    {
                        var num3 = (int) parameters;
                        return Convert.ToSingle(num3);
                    }
                    case 1:
                    {
                        var flag2 = (bool) parameters;
                        return Convert.ToSingle(flag2);
                    }
                    case 2:
                    {
                        float num4;
                        var str = (string) parameters;
                        if (float.TryParse((string) parameters, out num4))
                        {
                            return num4;
                        }
                        return 0f;
                    }
                    case 3:
                        return (float) parameters;
                }
                return (float) parameters;
        }
        return 0f;
    }

    public int returnInt(object sentObject)
    {
        var parameters = sentObject;
        if (this.parameters != null)
        {
            parameters = this.parameters;
        }
        switch (helperClass)
        {
            case 0:
                return (int) parameters;

            case 1:
            {
                var helper = (RCActionHelper) parameters;
                switch (helperType)
                {
                    case 0:
                        return (int) FengGameManagerMKII.intVariables[helper.returnString(null)];

                    case 1:
                        return nextHelper.returnInt(FengGameManagerMKII.boolVariables[helper.returnString(null)]);

                    case 2:
                        return nextHelper.returnInt(FengGameManagerMKII.stringVariables[helper.returnString(null)]);

                    case 3:
                        return nextHelper.returnInt(FengGameManagerMKII.floatVariables[helper.returnString(null)]);

                    case 4:
                        return nextHelper.returnInt(FengGameManagerMKII.playerVariables[helper.returnString(null)]);

                    case 5:
                        return nextHelper.returnInt(FengGameManagerMKII.titanVariables[helper.returnString(null)]);
                }
                return 0;
            }
            case 2:
            {
                var player = (PhotonPlayer) parameters;
                if (player != null)
                {
                    HERO hero;
                    switch (helperType)
                    {
                        case 0:
                            return (int) player.customProperties[PhotonPlayerProperty.team];

                        case 1:
                            return (int) player.customProperties[PhotonPlayerProperty.RCteam];

                        case 2:
                            return nextHelper.returnInt(player.customProperties[PhotonPlayerProperty.dead]);

                        case 3:
                            return (int) player.customProperties[PhotonPlayerProperty.isTitan];

                        case 4:
                            return (int) player.customProperties[PhotonPlayerProperty.kills];

                        case 5:
                            return (int) player.customProperties[PhotonPlayerProperty.deaths];

                        case 6:
                            return (int) player.customProperties[PhotonPlayerProperty.max_dmg];

                        case 7:
                            return (int) player.customProperties[PhotonPlayerProperty.total_dmg];

                        case 8:
                            return (int) player.customProperties[PhotonPlayerProperty.customInt];

                        case 9:
                            return nextHelper.returnInt(player.customProperties[PhotonPlayerProperty.customBool]);

                        case 10:
                            return nextHelper.returnInt(player.customProperties[PhotonPlayerProperty.customString]);

                        case 11:
                            return nextHelper.returnInt(player.customProperties[PhotonPlayerProperty.customFloat]);

                        case 12:
                            return nextHelper.returnInt(player.customProperties[PhotonPlayerProperty.name]);

                        case 13:
                            return nextHelper.returnInt(player.customProperties[PhotonPlayerProperty.guildName]);

                        case 14:
                        {
                            var iD = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[iD];
                                return nextHelper.returnInt(hero.transform.position.x);
                            }
                            return 0;
                        }
                        case 15:
                        {
                            var key = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(key))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[key];
                                return nextHelper.returnInt(hero.transform.position.y);
                            }
                            return 0;
                        }
                        case 0x10:
                        {
                            var num7 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num7))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num7];
                                return nextHelper.returnInt(hero.transform.position.z);
                            }
                            return 0;
                        }
                        case 0x11:
                        {
                            var num8 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num8))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num8];
                                return nextHelper.returnInt(hero.rigidbody.velocity.magnitude);
                            }
                            return 0;
                        }
                    }
                }
                return 0;
            }
            case 3:
            {
                var titan = (TITAN) parameters;
                if (titan != null)
                {
                    switch (helperType)
                    {
                        case 0:
                            return (int) titan.abnormalType;

                        case 1:
                            return nextHelper.returnInt(titan.myLevel);

                        case 2:
                            return titan.currentHealth;

                        case 3:
                            return nextHelper.returnInt(titan.transform.position.x);

                        case 4:
                            return nextHelper.returnInt(titan.transform.position.y);

                        case 5:
                            return nextHelper.returnInt(titan.transform.position.z);
                    }
                }
                return 0;
            }
            case 4:
            {
                var helper2 = (RCActionHelper) parameters;
                var region = (RCRegion) FengGameManagerMKII.RCRegions[helper2.returnString(null)];
                switch (helperType)
                {
                    case 0:
                        return nextHelper.returnInt(region.GetRandomX());

                    case 1:
                        return nextHelper.returnInt(region.GetRandomY());

                    case 2:
                        return nextHelper.returnInt(region.GetRandomZ());
                }
                return 0;
            }
            case 5:
                switch (helperType)
                {
                    case 0:
                        return (int) parameters;

                    case 1:
                    {
                        var flag2 = (bool) parameters;
                        return Convert.ToInt32(flag2);
                    }
                    case 2:
                    {
                        int num4;
                        var str = (string) parameters;
                        if (int.TryParse((string) parameters, out num4))
                        {
                            return num4;
                        }
                        return 0;
                    }
                    case 3:
                    {
                        var num3 = (float) parameters;
                        return Convert.ToInt32(num3);
                    }
                }
                return (int) parameters;
        }
        return 0;
    }

    public PhotonPlayer returnPlayer(object objParameter)
    {
        var parameters = objParameter;
        if (this.parameters != null)
        {
            parameters = this.parameters;
        }
        switch (helperClass)
        {
            case 1:
            {
                var helper = (RCActionHelper) parameters;
                return (PhotonPlayer) FengGameManagerMKII.playerVariables[helper.returnString(null)];
            }
            case 2:
                return (PhotonPlayer) parameters;
        }
        return (PhotonPlayer) parameters;
    }

    public string returnString(object sentObject)
    {
        var parameters = sentObject;
        if (this.parameters != null)
        {
            parameters = this.parameters;
        }
        switch (helperClass)
        {
            case 0:
                return (string) parameters;

            case 1:
            {
                var helper = (RCActionHelper) parameters;
                switch (helperType)
                {
                    case 0:
                        return nextHelper.returnString(FengGameManagerMKII.intVariables[helper.returnString(null)]);

                    case 1:
                        return nextHelper.returnString(FengGameManagerMKII.boolVariables[helper.returnString(null)]);

                    case 2:
                        return (string) FengGameManagerMKII.stringVariables[helper.returnString(null)];

                    case 3:
                        return nextHelper.returnString(FengGameManagerMKII.floatVariables[helper.returnString(null)]);

                    case 4:
                        return nextHelper.returnString(FengGameManagerMKII.playerVariables[helper.returnString(null)]);

                    case 5:
                        return nextHelper.returnString(FengGameManagerMKII.titanVariables[helper.returnString(null)]);
                }
                return string.Empty;
            }
            case 2:
            {
                var player = (PhotonPlayer) parameters;
                if (player != null)
                {
                    HERO hero;
                    switch (helperType)
                    {
                        case 0:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.team]);

                        case 1:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.RCteam]);

                        case 2:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.dead]);

                        case 3:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.isTitan]);

                        case 4:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.kills]);

                        case 5:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.deaths]);

                        case 6:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.max_dmg]);

                        case 7:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.total_dmg]);

                        case 8:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.customInt]);

                        case 9:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.customBool]);

                        case 10:
                            return (string) player.customProperties[PhotonPlayerProperty.customString];

                        case 11:
                            return nextHelper.returnString(player.customProperties[PhotonPlayerProperty.customFloat]);

                        case 12:
                            return (string) player.customProperties[PhotonPlayerProperty.name];

                        case 13:
                            return (string) player.customProperties[PhotonPlayerProperty.guildName];

                        case 14:
                        {
                            var iD = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[iD];
                                return nextHelper.returnString(hero.transform.position.x);
                            }
                            return string.Empty;
                        }
                        case 15:
                        {
                            var key = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(key))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[key];
                                return nextHelper.returnString(hero.transform.position.y);
                            }
                            return string.Empty;
                        }
                        case 0x10:
                        {
                            var num6 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num6))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num6];
                                return nextHelper.returnString(hero.transform.position.z);
                            }
                            return string.Empty;
                        }
                        case 0x11:
                        {
                            var num7 = player.ID;
                            if (FengGameManagerMKII.heroHash.ContainsKey(num7))
                            {
                                hero = (HERO) FengGameManagerMKII.heroHash[num7];
                                return nextHelper.returnString(hero.rigidbody.velocity.magnitude);
                            }
                            return string.Empty;
                        }
                    }
                }
                return string.Empty;
            }
            case 3:
            {
                var titan = (TITAN) parameters;
                if (titan != null)
                {
                    switch (helperType)
                    {
                        case 0:
                            return nextHelper.returnString(titan.abnormalType);

                        case 1:
                            return nextHelper.returnString(titan.myLevel);

                        case 2:
                            return nextHelper.returnString(titan.currentHealth);

                        case 3:
                            return nextHelper.returnString(titan.transform.position.x);

                        case 4:
                            return nextHelper.returnString(titan.transform.position.y);

                        case 5:
                            return nextHelper.returnString(titan.transform.position.z);
                    }
                }
                return string.Empty;
            }
            case 4:
            {
                var helper2 = (RCActionHelper) parameters;
                var region = (RCRegion) FengGameManagerMKII.RCRegions[helper2.returnString(null)];
                switch (helperType)
                {
                    case 0:
                        return nextHelper.returnString(region.GetRandomX());

                    case 1:
                        return nextHelper.returnString(region.GetRandomY());

                    case 2:
                        return nextHelper.returnString(region.GetRandomZ());
                }
                return string.Empty;
            }
            case 5:
                switch (helperType)
                {
                    case 0:
                    {
                        var num2 = (int) parameters;
                        return num2.ToString();
                    }
                    case 1:
                    {
                        var flag2 = (bool) parameters;
                        return flag2.ToString();
                    }
                    case 2:
                        return (string) parameters;

                    case 3:
                    {
                        var num3 = (float) parameters;
                        return num3.ToString();
                    }
                }
                return string.Empty;
        }
        return string.Empty;
    }

    public TITAN returnTitan(object objParameter)
    {
        var parameters = objParameter;
        if (this.parameters != null)
        {
            parameters = this.parameters;
        }
        switch (helperClass)
        {
            case 1:
            {
                var helper = (RCActionHelper) parameters;
                return (TITAN) FengGameManagerMKII.titanVariables[helper.returnString(null)];
            }
            case 3:
                return (TITAN) parameters;
        }
        return (TITAN) parameters;
    }

    public void setNextHelper(RCActionHelper sentHelper)
    {
        nextHelper = sentHelper;
    }

    public enum helperClasses
    {
        primitive,
        variable,
        player,
        titan,
        region,
        convert
    }

    public enum mathTypes
    {
        add,
        subtract,
        multiply,
        divide,
        modulo,
        power
    }

    public enum other
    {
        regionX,
        regionY,
        regionZ
    }

    public enum playerTypes
    {
        playerType,
        playerTeam,
        playerAlive,
        playerTitan,
        playerKills,
        playerDeaths,
        playerMaxDamage,
        playerTotalDamage,
        playerCustomInt,
        playerCustomBool,
        playerCustomString,
        playerCustomFloat,
        playerName,
        playerGuildName,
        playerPosX,
        playerPosY,
        playerPosZ,
        playerSpeed
    }

    public enum titanTypes
    {
        titanType,
        titanSize,
        titanHealth,
        positionX,
        positionY,
        positionZ
    }

    public enum variableTypes
    {
        typeInt,
        typeBool,
        typeString,
        typeFloat,
        typePlayer,
        typeTitan
    }
}

