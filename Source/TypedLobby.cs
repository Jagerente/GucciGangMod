//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

public class TypedLobby
{
    public static readonly TypedLobby Default = new TypedLobby();
    public string Name;
    public LobbyType Type;

    public TypedLobby()
    {
        Name = string.Empty;
        Type = LobbyType.Default;
    }

    public TypedLobby(string name, LobbyType type)
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return string.Format("Lobby '{0}'[{1}]", Name, Type);
    }

    public bool IsDefault
    {
        get
        {
            return ((Type == LobbyType.Default) && string.IsNullOrEmpty(Name));
        }
    }
}

