//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections.Generic;

[Serializable]
public class BMGlyph
{
    public int advance;
    public int channel;
    public int height;
    public int index;
    public List<int> kerning;
    public int offsetX;
    public int offsetY;
    public int width;
    public int x;
    public int y;

    public int GetKerning(int previousChar)
    {
        if (kerning != null)
        {
            var num = 0;
            var count = kerning.Count;
            while (num < count)
            {
                if (kerning[num] == previousChar)
                {
                    return kerning[num + 1];
                }
                num += 2;
            }
        }
        return 0;
    }

    public void SetKerning(int previousChar, int amount)
    {
        if (kerning == null)
        {
            kerning = new List<int>();
        }
        for (var i = 0; i < kerning.Count; i += 2)
        {
            if (kerning[i] == previousChar)
            {
                kerning[i + 1] = amount;
                return;
            }
        }
        kerning.Add(previousChar);
        kerning.Add(amount);
    }

    public void Trim(int xMin, int yMin, int xMax, int yMax)
    {
        var num = x + width;
        var num2 = y + height;
        if (x < xMin)
        {
            var num3 = xMin - x;
            x += num3;
            width -= num3;
            offsetX += num3;
        }
        if (y < yMin)
        {
            var num4 = yMin - y;
            y += num4;
            height -= num4;
            offsetY += num4;
        }
        if (num > xMax)
        {
            width -= num - xMax;
        }
        if (num2 > yMax)
        {
            height -= num2 - yMax;
        }
    }
}

