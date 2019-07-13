using ExitGames.Client.Photon;
using UnityEngine;

internal static class CustomTypes
{
    private static object DeserializePhotonPlayer(byte[] bytes)
    {
        int num2;
        var offset = 0;
        Protocol.Deserialize(out num2, bytes, ref offset);
        if (PhotonNetwork.networkingPeer.mActors.ContainsKey(num2))
        {
            return PhotonNetwork.networkingPeer.mActors[num2];
        }
        return null;
    }

    private static object DeserializeQuaternion(byte[] bytes)
    {
        var quaternion = new Quaternion();
        var offset = 0;
        Protocol.Deserialize(out quaternion.w, bytes, ref offset);
        Protocol.Deserialize(out quaternion.x, bytes, ref offset);
        Protocol.Deserialize(out quaternion.y, bytes, ref offset);
        Protocol.Deserialize(out quaternion.z, bytes, ref offset);
        return quaternion;
    }

    private static object DeserializeVector2(byte[] bytes)
    {
        var vector = new Vector2();
        var offset = 0;
        Protocol.Deserialize(out vector.x, bytes, ref offset);
        Protocol.Deserialize(out vector.y, bytes, ref offset);
        return vector;
    }

    private static object DeserializeVector3(byte[] bytes)
    {
        var vector = new Vector3();
        var offset = 0;
        Protocol.Deserialize(out vector.x, bytes, ref offset);
        Protocol.Deserialize(out vector.y, bytes, ref offset);
        Protocol.Deserialize(out vector.z, bytes, ref offset);
        return vector;
    }

    internal static void Register()
    {
        PhotonPeer.RegisterType(typeof(Vector2), 87, SerializeVector2, DeserializeVector2);
        PhotonPeer.RegisterType(typeof(Vector3), 86, SerializeVector3, DeserializeVector3);
        PhotonPeer.RegisterType(typeof(Quaternion), 81, SerializeQuaternion, DeserializeQuaternion);
        PhotonPeer.RegisterType(typeof(PhotonPlayer), 80, SerializePhotonPlayer, DeserializePhotonPlayer);
    }

    private static byte[] SerializePhotonPlayer(object customobject)
    {
        var iD = ((PhotonPlayer)customobject).ID;
        var target = new byte[4];
        var targetOffset = 0;
        Protocol.Serialize(iD, target, ref targetOffset);
        return target;
    }

    private static byte[] SerializeQuaternion(object obj)
    {
        var quaternion = (Quaternion)obj;
        var target = new byte[16];
        var targetOffset = 0;
        Protocol.Serialize(quaternion.w, target, ref targetOffset);
        Protocol.Serialize(quaternion.x, target, ref targetOffset);
        Protocol.Serialize(quaternion.y, target, ref targetOffset);
        Protocol.Serialize(quaternion.z, target, ref targetOffset);
        return target;
    }

    private static byte[] SerializeVector2(object customobject)
    {
        var vector = (Vector2)customobject;
        var target = new byte[8];
        var targetOffset = 0;
        Protocol.Serialize(vector.x, target, ref targetOffset);
        Protocol.Serialize(vector.y, target, ref targetOffset);
        return target;
    }

    private static byte[] SerializeVector3(object customobject)
    {
        var vector = (Vector3)customobject;
        var targetOffset = 0;
        var target = new byte[12];
        Protocol.Serialize(vector.x, target, ref targetOffset);
        Protocol.Serialize(vector.y, target, ref targetOffset);
        Protocol.Serialize(vector.z, target, ref targetOffset);
        return target;
    }
}