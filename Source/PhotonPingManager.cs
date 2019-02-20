//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PhotonPingManager
{
    public static int Attempts = 5;
    public static bool IgnoreInitialAttempt = true;
    public static int MaxMilliseconsPerPing = 800;
    private int PingsRunning;
    public bool UseNative;

    [DebuggerHidden]
    public IEnumerator PingSocket(Region region)
    {
        return new PingSocketcIteratorB { region = region, Sregion = region, fthis = this };
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
            UnityEngine.Debug.Log("Exception caught! " + exception.Source + " Message: " + exception.Message);
        }
        return string.Empty;
    }

    public Region BestRegion
    {
        get
        {
            Region region = null;
            var ping = 0x7fffffff;
            foreach (var region2 in PhotonNetwork.networkingPeer.AvailableRegions)
            {
                UnityEngine.Debug.Log("BestRegion checks region: " + region2);
                if ((region2.Ping != 0) && (region2.Ping < ping))
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
            return (PingsRunning == 0);
        }
    }

    [CompilerGenerated]
    private sealed class PingSocketcIteratorB : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal Region Sregion;
        internal PhotonPingManager fthis;
        internal string cleanIpOfRegion3;
        internal Exception e8;
        internal int i5;
        internal int indexOfColon4;
        internal bool overtime6;
        internal PhotonPing ping0;
        internal int replyCount2;
        internal int rtt9;
        internal float rttSum1;
        internal Stopwatch sw7;
        internal Region region;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    region.Ping = Attempts * MaxMilliseconsPerPing;
                    fthis.PingsRunning++;
                    if (PhotonHandler.PingImplementation != typeof(PingNativeDynamic))
                    {
                        ping0 = (PhotonPing) Activator.CreateInstance(PhotonHandler.PingImplementation);
                        break;
                    }
                    UnityEngine.Debug.Log("Using constructor for new PingNativeDynamic()");
                    ping0 = new PingNativeDynamic();
                    break;

                case 1:
                    //goto Label_01B9;

                case 2:
                    //goto Label_0265;

                case 3:
                    SPC = -1;
                    goto Label_02B0;

                default:
                    goto Label_02B0;
            }
            rttSum1 = 0f;
            replyCount2 = 0;
            cleanIpOfRegion3 = region.HostAndPort;
            indexOfColon4 = cleanIpOfRegion3.LastIndexOf(':');
            if (indexOfColon4 > 1)
            {
                cleanIpOfRegion3 = cleanIpOfRegion3.Substring(0, indexOfColon4);
            }
            cleanIpOfRegion3 = ResolveHost(cleanIpOfRegion3);
            i5 = 0;
            while (i5 < Attempts)
            {
                overtime6 = false;
                sw7 = new Stopwatch();
                sw7.Start();
                try
                {
                    ping0.StartPing(cleanIpOfRegion3);
                }
                catch (Exception exception)
                {
                    e8 = exception;
                    UnityEngine.Debug.Log("catched: " + e8);
                    fthis.PingsRunning--;
                    break;
                }
            Label_01B9:
                while (!ping0.Done())
                {
                    if (sw7.ElapsedMilliseconds >= MaxMilliseconsPerPing)
                    {
                        overtime6 = true;
                        break;
                    }
                    Scurrent = 0;
                    SPC = 1;
                    goto Label_02B2;
                }
                rtt9 = (int) sw7.ElapsedMilliseconds;
                if ((!IgnoreInitialAttempt || (i5 != 0)) && (ping0.Successful && !overtime6))
                {
                    rttSum1 += rtt9;
                    replyCount2++;
                    region.Ping = (int) (rttSum1 / replyCount2);
                }
                Scurrent = new WaitForSeconds(0.1f);
                SPC = 2;
                goto Label_02B2;
            Label_0265:
                i5++;
            }
            fthis.PingsRunning--;
            Scurrent = null;
            SPC = 3;
            goto Label_02B2;
        Label_02B0:
            return false;
        Label_02B2:
            return true;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }
}

