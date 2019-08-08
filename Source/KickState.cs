public class KickState
{
    public int id;
    private int kickCount;
    private string kickers;
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