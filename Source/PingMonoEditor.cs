//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using ExitGames.Client.Photon;
using System;
using System.Net.Sockets;
using UnityEngine;

public class PingMonoEditor : PhotonPing
{
    private Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    public override void Dispose()
    {
        try
        {
            sock.Close();
        }
        catch
        {
        }
        sock = null;
    }

    public override bool Done()
    {
        if (!GotResult && (sock != null))
        {
            if (sock.Available <= 0)
            {
                return false;
            }
            var num = sock.Receive(PingBytes, SocketFlags.None);
            if ((PingBytes[PingBytes.Length - 1] != PingId) || (num != PingLength))
            {
                Debug.Log("ReplyMatch is false! ");
            }
            Successful = (num == PingBytes.Length) && (PingBytes[PingBytes.Length - 1] == PingId);
            GotResult = true;
        }
        return true;
    }

    public override bool StartPing(string ip)
    {
        Init();
        try
        {
            sock.ReceiveTimeout = 0x1388;
            sock.Connect(ip, 0x13bf);
            PingBytes[PingBytes.Length - 1] = PingId;
            sock.Send(PingBytes);
            PingBytes[PingBytes.Length - 1] = (byte) (PingId - 1);
        }
        catch (Exception exception)
        {
            sock = null;
            Console.WriteLine(exception);
        }
        return false;
    }
}

