using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GGM.Config;
using UnityEngine;

namespace GGM
{
    internal class Logger
    {
        public static string ChatLogPath = Application.dataPath + "/chat.txt";

        public static void Log(string path, string message)
        {
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }

            File.AppendAllText(path, message);
        }

        public static void LogChat(string path, string message, PhotonMessageInfo info)
        {
            if (!Settings.LogChatSetting) return;

            Log(path, $"[{DateTime.Now.ToShortTimeString()}] {info.sender.Name.StripHEX()}: {message}{Environment.NewLine}".StripHTML());
        }

        public static void LogChat(string path, RoomInfo info)
        {
            if (!Settings.LogChatSetting) return;

            const string section = "--------------------------------------------------------------------------------------------------------------------------------------------------------";
            var data = string.Empty;
            for (var i = 0; i < 4; i++) data += PhotonNetwork.room.name.Split('`')[i].ToUpper() + (i < 3 ? "/" : string.Empty);
            Log(path, section + Environment.NewLine + DateTime.Now.ToLongDateString() + Environment.NewLine + info + Environment.NewLine + section + Environment.NewLine);
        }
    }
}