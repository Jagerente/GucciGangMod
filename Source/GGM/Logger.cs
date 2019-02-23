using System;
using System.IO;

namespace GGM
{
    internal class Logger
    {
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
            Log(path, ($"[{DateTime.Now.ToShortTimeString()}] {info.sender.Name}: {message}{Environment.NewLine}").stripHTML());
        }
    }
}
