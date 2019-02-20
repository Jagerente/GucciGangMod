namespace ExitGames.Client.Photon
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Security;
    using System.Threading;

    internal class SocketUdp : IPhotonSocket
    {
        private Socket sock;
        private readonly object syncer;

        public SocketUdp(PeerBase npeer) : base(npeer)
        {
            syncer = new object();
            if (ReportDebugOfLevel(DebugLevel.ALL))
            {
                Listener.DebugReturn(DebugLevel.ALL, "CSharpSocket: UDP, Unity3d.");
            }
            Protocol = ConnectionProtocol.Udp;
            PollReceive = false;
        }

        public override bool Connect()
        {
            var syncer = this.syncer;
            lock (syncer)
            {
                if (!base.Connect())
                {
                    return false;
                }
                State = PhotonSocketState.Connecting;
                new Thread(new ThreadStart(DnsAndConnect)) { Name = "photon dns thread", IsBackground = true }.Start();
                return true;
            }
        }

        public override bool Disconnect()
        {
            if (ReportDebugOfLevel(DebugLevel.INFO))
            {
                EnqueueDebugReturn(DebugLevel.INFO, "CSharpSocket.Disconnect()");
            }
            State = PhotonSocketState.Disconnecting;
            var syncer = this.syncer;
            lock (syncer)
            {
                if (sock != null)
                {
                    try
                    {
                        sock.Close();
                        sock = null;
                    }
                    catch (Exception exception)
                    {
                        EnqueueDebugReturn(DebugLevel.INFO, "Exception in Disconnect(): " + exception);
                    }
                }
            }
            State = PhotonSocketState.Disconnected;
            return true;
        }

        internal void DnsAndConnect()
        {
            try
            {
                var syncer = this.syncer;
                lock (syncer)
                {
                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    var ipAddress = GetIpAddress(ServerAddress);
                    sock.Connect(ipAddress, ServerPort);
                    State = PhotonSocketState.Connected;
                }
            }
            catch (SecurityException exception)
            {
                if (ReportDebugOfLevel(DebugLevel.ERROR))
                {
                    Listener.DebugReturn(DebugLevel.ERROR, "Connect() failed: " + exception.ToString());
                }
                HandleException(StatusCode.SecurityExceptionOnConnect);
                return;
            }
            catch (Exception exception2)
            {
                if (ReportDebugOfLevel(DebugLevel.ERROR))
                {
                    Listener.DebugReturn(DebugLevel.ERROR, "Connect() failed: " + exception2.ToString());
                }
                HandleException(StatusCode.ExceptionOnConnect);
                return;
            }
            new Thread(new ThreadStart(ReceiveLoop)) { Name = "photon receive thread", IsBackground = true }.Start();
        }

        public override PhotonSocketError Receive(out byte[] data)
        {
            data = null;
            return PhotonSocketError.NoData;
        }

        public void ReceiveLoop()
        {
            var buffer = new byte[MTU];
            while (State == PhotonSocketState.Connected)
            {
                try
                {
                    var length = sock.Receive(buffer);
                    HandleReceivedDatagram(buffer, length, true);
                    continue;
                }
                catch (Exception exception)
                {
                    if ((State != PhotonSocketState.Disconnecting) && (State != PhotonSocketState.Disconnected))
                    {
                        if (ReportDebugOfLevel(DebugLevel.ERROR))
                        {
                            EnqueueDebugReturn(DebugLevel.ERROR, string.Concat(new object[] { "Receive issue. State: ", State, " Exception: ", exception }));
                        }
                        HandleException(StatusCode.ExceptionOnReceive);
                    }
                    continue;
                }
            }
            Disconnect();
        }

        public override PhotonSocketError Send(byte[] data, int length)
        {
            var syncer = this.syncer;
            lock (syncer)
            {
                if (!sock.Connected)
                {
                    return PhotonSocketError.Skipped;
                }
                try
                {
                    sock.Send(data, 0, length, SocketFlags.None);
                }
                catch
                {
                    return PhotonSocketError.Exception;
                }
            }
            return PhotonSocketError.Success;
        }
    }
}

