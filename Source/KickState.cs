//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections;

public class KickState
{
    public int id;
    private int kickCount;
    private string kickers;
    private ArrayList kickers2;
    public string name;

    public void addKicker(string n)
    {
        if (!kickers.Contains(n))
        {
            kickers = kickers + n;
            kickCount++;
        }
    }

    public int getKickCount()
    {
        return kickCount;
    }

    public void init(string n)
    {
        name = n;
        kickers = string.Empty;
        kickCount = 0;
    }
}

