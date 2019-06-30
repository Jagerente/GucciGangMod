using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using ExitGames.Client.Photon;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PhotonPingManager
{
    public static int Attempts = 5;
    public static bool IgnoreInitialAttempt = true;
    public static int MaxMilliseconsPerPing = 800;
    private int PingsRunning;
    public bool UseNative;

    
    public IEnumerator PingSocket(Region region)
    {
        region.Ping = Attempts * MaxMilliseconsPerPing;
        PingsRunning++;
        PhotonPing ping;
        if (PhotonHandler.PingImplementation == typeof(PingNativeDynamic))
        {
            Debug.Log("Using constructor for new PingNativeDynamic()");
            ping = new PingNativeDynamic();
        }
        else
        {
            ping = (PhotonPing)Activator.CreateInstance(PhotonHandler.PingImplementation);
        }
        var rttSum = 0f;
        var replyCount = 0;
        var cleanIpOfRegion = region.HostAndPort;
        var indexOfColon = cleanIpOfRegion.LastIndexOf(':');
        if (indexOfColon > 1)
        {
            cleanIpOfRegion = cleanIpOfRegion.Substring(0, indexOfColon);
        }
        cleanIpOfRegion = ResolveHost(cleanIpOfRegion);
        for (var i = 0; i < Attempts; i++)
        {
            var overtime = false;
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                ping.StartPing(cleanIpOfRegion);
            }
            catch (Exception ex)
            {
                var e = ex;
                Debug.Log("catched: " + e);
                PingsRunning--;
                break;
            }
            while (!ping.Done())
            {
                if (sw.ElapsedMilliseconds < MaxMilliseconsPerPing)
                {
                    yield return 0;
                    continue;
                }
                overtime = true;
                break;
            }
            var rtt = (int)sw.ElapsedMilliseconds;
            if ((!IgnoreInitialAttempt || i != 0) && ping.Successful && !overtime)
            {
                rttSum += rtt;
                replyCount++;
                region.Ping = (int)(rttSum / replyCount);
            }
            yield return new WaitForSeconds(0.1f);
        }
        PingsRunning--;
        yield return null;
    }

    public static string ResolveHost(string hostName)
    {
        try
        {
            var hostAddresses = Dns.GetHostAddresses(hostName);
            if (hostAddresses.Length == 1)
            {
                return hostAddresses[0].ToString();
            }
            for (var i = 0; i < hostAddresses.Length; i++)
            {
                var address = hostAddresses[i];
                if (address != null)
                {
                    var str2 = address.ToString();
                    if (str2.IndexOf('.') >= 0)
                    {
                        return str2;
                    }
                }
            }
        }
        catch (Exception exception)
        {
            Debug.Log("Exception caught! " + exception.Source + " Message: " + exception.Message);
        }
        return string.Empty;
    }

    public Region BestRegion
    {
        get
        {
            Region region = null;
            var ping = 2147483647;
            foreach (var region2 in PhotonNetwork.networkingPeer.AvailableRegions)
            {
                Debug.Log("BestRegion checks region: " + region2);
                if (region2.Ping != 0 && region2.Ping < ping)
                {
                    ping = region2.Ping;
                    region = region2;
                }
            }
            return region;
        }
    }

    public bool Done
    {
        get
        {
            return PingsRunning == 0;
        }
    }

}

