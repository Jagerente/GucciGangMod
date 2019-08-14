using ExitGames.Client.Photon;
using System.Collections.Generic;

public class WebRpcResponse
{
    public WebRpcResponse(OperationResponse response)
    {
        object obj2;
        response.Parameters.TryGetValue(209, out obj2);
        Name = obj2 as string;
        response.Parameters.TryGetValue(207, out obj2);
        ReturnCode = obj2 == null ? -1 : (byte) obj2;
        response.Parameters.TryGetValue(208, out obj2);
        Parameters = obj2 as Dictionary<string, object>;
        response.Parameters.TryGetValue(206, out obj2);
        DebugMessage = obj2 as string;
    }

    public string ToStringFull()
    {
        object[] args = {Name, SupportClass.DictionaryToString(Parameters), ReturnCode, DebugMessage};
        return string.Format("{0}={2}: {1} \"{3}\"", args);
    }

    public string DebugMessage { get; private set; }

    public string Name { get; private set; }

    public Dictionary<string, object> Parameters { get; private set; }

    public int ReturnCode { get; private set; }
}