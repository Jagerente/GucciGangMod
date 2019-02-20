//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

public class RCCondition
{
    private int operand;
    private RCActionHelper parameter1;
    private RCActionHelper parameter2;
    private int type;

    public RCCondition(int sentOperand, int sentType, RCActionHelper sentParam1, RCActionHelper sentParam2)
    {
        operand = sentOperand;
        type = sentType;
        parameter1 = sentParam1;
        parameter2 = sentParam2;
    }

    private bool boolCompare(bool baseBool, bool compareBool)
    {
        switch (operand)
        {
            case 2:
                return (baseBool == compareBool);

            case 5:
                return (baseBool != compareBool);
        }
        return false;
    }

    public bool checkCondition()
    {
        switch (type)
        {
            case 0:
                return intCompare(parameter1.returnInt(null), parameter2.returnInt(null));

            case 1:
                return boolCompare(parameter1.returnBool(null), parameter2.returnBool(null));

            case 2:
                return stringCompare(parameter1.returnString(null), parameter2.returnString(null));

            case 3:
                return floatCompare(parameter1.returnFloat(null), parameter2.returnFloat(null));

            case 4:
                return playerCompare(parameter1.returnPlayer(null), parameter2.returnPlayer(null));

            case 5:
                return titanCompare(parameter1.returnTitan(null), parameter2.returnTitan(null));
        }
        return false;
    }

    private bool floatCompare(float baseFloat, float compareFloat)
    {
        switch (operand)
        {
            case 0:
                if (baseFloat >= compareFloat)
                {
                    return false;
                }
                return true;

            case 1:
                if (baseFloat > compareFloat)
                {
                    return false;
                }
                return true;

            case 2:
                if (baseFloat != compareFloat)
                {
                    return false;
                }
                return true;

            case 3:
                if (baseFloat < compareFloat)
                {
                    return false;
                }
                return true;

            case 4:
                if (baseFloat <= compareFloat)
                {
                    return false;
                }
                return true;

            case 5:
                if (baseFloat == compareFloat)
                {
                    return false;
                }
                return true;
        }
        return false;
    }

    private bool intCompare(int baseInt, int compareInt)
    {
        switch (operand)
        {
            case 0:
                if (baseInt >= compareInt)
                {
                    return false;
                }
                return true;

            case 1:
                if (baseInt > compareInt)
                {
                    return false;
                }
                return true;

            case 2:
                if (baseInt != compareInt)
                {
                    return false;
                }
                return true;

            case 3:
                if (baseInt < compareInt)
                {
                    return false;
                }
                return true;

            case 4:
                if (baseInt <= compareInt)
                {
                    return false;
                }
                return true;

            case 5:
                if (baseInt == compareInt)
                {
                    return false;
                }
                return true;
        }
        return false;
    }

    private bool playerCompare(PhotonPlayer basePlayer, PhotonPlayer comparePlayer)
    {
        switch (operand)
        {
            case 2:
                return (basePlayer == comparePlayer);

            case 5:
                return (basePlayer != comparePlayer);
        }
        return false;
    }

    private bool stringCompare(string baseString, string compareString)
    {
        switch (operand)
        {
            case 0:
                if (!(baseString == compareString))
                {
                    return false;
                }
                return true;

            case 1:
                if (!(baseString != compareString))
                {
                    return false;
                }
                return true;

            case 2:
                if (!baseString.Contains(compareString))
                {
                    return false;
                }
                return true;

            case 3:
                if (baseString.Contains(compareString))
                {
                    return false;
                }
                return true;

            case 4:
                if (!baseString.StartsWith(compareString))
                {
                    return false;
                }
                return true;

            case 5:
                if (baseString.StartsWith(compareString))
                {
                    return false;
                }
                return true;

            case 6:
                if (!baseString.EndsWith(compareString))
                {
                    return false;
                }
                return true;

            case 7:
                if (baseString.EndsWith(compareString))
                {
                    return false;
                }
                return true;
        }
        return false;
    }

    private bool titanCompare(TITAN baseTitan, TITAN compareTitan)
    {
        switch (operand)
        {
            case 2:
                return (baseTitan == compareTitan);

            case 5:
                return (baseTitan != compareTitan);
        }
        return false;
    }

    public enum castTypes
    {
        typeInt,
        typeBool,
        typeString,
        typeFloat,
        typePlayer,
        typeTitan
    }

    public enum operands
    {
        lt,
        lte,
        e,
        gte,
        gt,
        ne
    }

    public enum stringOperands
    {
        equals,
        notEquals,
        contains,
        notContains,
        startsWith,
        notStartsWith,
        endsWith,
        notEndsWith
    }
}

