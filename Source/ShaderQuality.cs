//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Examples/Shader Quality"), ExecuteInEditMode]
public class ShaderQuality : MonoBehaviour
{
    private int mCurrent = 600;

    private void Update()
    {
        var num = (QualitySettings.GetQualityLevel() + 1) * 100;
        if (mCurrent != num)
        {
            mCurrent = num;
            Shader.globalMaximumLOD = mCurrent;
        }
    }
}

