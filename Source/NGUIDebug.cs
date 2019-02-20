//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
    private static NGUIDebug mInstance = null;
    private static List<string> mLines = new List<string>();

    public static void DrawBounds(Bounds b)
    {
        var center = b.center;
        var vector2 = b.center - b.extents;
        var vector3 = b.center + b.extents;
        Debug.DrawLine(new Vector3(vector2.x, vector2.y, center.z), new Vector3(vector3.x, vector2.y, center.z), Color.red);
        Debug.DrawLine(new Vector3(vector2.x, vector2.y, center.z), new Vector3(vector2.x, vector3.y, center.z), Color.red);
        Debug.DrawLine(new Vector3(vector3.x, vector2.y, center.z), new Vector3(vector3.x, vector3.y, center.z), Color.red);
        Debug.DrawLine(new Vector3(vector2.x, vector3.y, center.z), new Vector3(vector3.x, vector3.y, center.z), Color.red);
    }

    public static void Log(string text)
    {
        if (Application.isPlaying)
        {
            if (mLines.Count > 20)
            {
                mLines.RemoveAt(0);
            }
            mLines.Add(text);
            if (mInstance == null)
            {
                var target = new GameObject("_NGUI Debug");
                mInstance = target.AddComponent<NGUIDebug>();
                DontDestroyOnLoad(target);
            }
        }
        else
        {
            Debug.Log(text);
        }
    }

    private void OnGUI()
    {
        var num = 0;
        var count = mLines.Count;
        while (num < count)
        {
            GUILayout.Label(mLines[num]);
            num++;
        }
    }
}

