//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using ExitGames.Client.Photon;
using System.Collections.Generic;

public class WebRpcResponse
{
    public WebRpcResponse(OperationResponse response)
    {
        object obj2;
        response.Parameters.TryGetValue(0xd1, out obj2);
        Name = obj2 as string;
        response.Parameters.TryGetValue(0xcf, out obj2);
        ReturnCode = (obj2 == null) ? -1 : ((byte) obj2);
        response.Parameters.TryGetValue(0xd0, out obj2);
        Parameters = obj2 as Dictionary<string, object>;
        response.Parameters.TryGetValue(0xce, out obj2);
        DebugMessage = obj2 as string;
    }

    public string ToStringFull()
    {
        var args = new object[] { Name, SupportClass.DictionaryToString(Parameters), ReturnCode, DebugMessage };
        return string.Format("{0}={2}: {1} \"{3}\"", args);
    }

    public string DebugMessage { get; private set; }

    public string Name { get; private set; }

    public Dictionary<string, object> Parameters { get; private set; }

    public int ReturnCode { get; private set; }
}

