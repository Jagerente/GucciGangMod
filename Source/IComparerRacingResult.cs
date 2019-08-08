using System;
using System.Collections;

public class IComparerRacingResult : IComparer
{
    int IComparer.Compare(object x, object y)
    {
        var time = ((RacingResult)x).time;
        var num2 = ((RacingResult)y).time;
        if (time == num2 || Math.Abs(time - num2) < float.Epsilon)
        {
            return 0;
        }
        if (time < num2)
        {
            return -1;
        }
        return 1;
    }
}