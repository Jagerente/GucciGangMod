using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscordRPC;

namespace GGM
{
    class RichPresence
    {
        public static bool DetailsUpdate = false;
        public static bool StateUpdate = false;
        public static bool Quit = false;

        public static string Details = string.Empty;
        public static string State = string.Empty;

        static bool IsRunning = false;
        /// <summary>
        /// The pipe Discord is located on. If set to -1, the client will scan for the first available pipe.
        /// </summary>
        private static int DiscordPipe = -1;
        /// <summary>
        /// ID of the client
        /// </summary>
        private static string ClientID = "546067288093097994";
        /// <summary>
        /// The current presence to send to discord.
        /// </summary>
        private static DiscordRPC.RichPresence presence = new DiscordRPC.RichPresence()
        {
            Details = "GucciGangMod",
            State = "In Game",
            Assets = new Assets()
            {
                LargeImageKey = "image_large",
                LargeImageText = "github.com/JustlPain/GucciGangMod",
                SmallImageKey = "image_large",
                SmallImageText = "v4"
            }
        };
        /// <summary>
        /// The discord client
        /// </summary>
        private static DiscordRpcClient client;

        public static void Connection()
        {
            //Creates a new Discord RPC Client.
            using (client = new DiscordRpcClient(ClientID, false, DiscordPipe))
            {
                if (!IsRunning)
                {
                    presence.Timestamps = new Timestamps()
                    {
                        Start = DateTime.UtcNow,
                        End = DateTime.UtcNow + TimeSpan.FromSeconds(15)
                    };

                    //Set some new presence to tell Discord we are in a game.
                    client.SetPresence(presence);

                    //Initialize the connection.
                    client.Initialize();
                    IsRunning = true;

                    while (client != null && IsRunning)
                    {
                        //We will invoke the client events. 
                        // In a game situation, you would do this in the Update.
                        if (client != null)
                            client.Invoke();

                        //Try to read any keys if available
                    }
                }
                if (DetailsUpdate)
                {
                    client.UpdateDetails(Details);
                    DetailsUpdate = false;
                }
                if (StateUpdate)
                {
                    client.UpdateState(State);
                    StateUpdate = false;
                }
                if (Quit)
                {
                    client.Dispose();
                }
            }
        }
    }
}
