//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

public class FriendInfo
{
    public override string ToString()
    {
        return string.Format("{0}\t is: {1}", Name, IsOnline ? !IsInRoom ? "on master" : "playing" : "offline");
    }

    public bool IsInRoom
    {
        get
        {
            return IsOnline && !string.IsNullOrEmpty(Room);
        }
    }

    public bool IsOnline { get; protected internal set; }

    public string Name { get; protected internal set; }

    public string Room { get; protected internal set; }
}

