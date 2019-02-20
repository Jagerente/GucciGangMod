//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class FengMath
{
    public static float getHorizontalAngle(Vector3 from, Vector3 to)
    {
        var vector = to - from;
        return -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
    }

    public static Quaternion getHorizontalRotation(Vector3 from, Vector3 to)
    {
        var vector = from - to;
        var y = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
        return Quaternion.Euler(0f, y, 0f);
    }
}

