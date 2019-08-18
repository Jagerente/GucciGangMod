//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using UnityEngine;
//namespace GGM
//{
//    internal class LocationSkins
//    {
//        public Locations Location;
//        public string Settings;
//        public string Title;
//        public string URL;
        
//        private readonly string path = Application.dataPath + "/Skins/Locations";
//        private string filePath;

//        private Dictionary<string, string> Preset;

//        private List<Dictionary<string, string>> Presets;

//        private int Count { get; set; }

//        public LocationSkins(Locations location)
//        {
//            Location = location;

//            Load();
//        }

//        public enum Locations { City, Forest };

//        public void Load()
//        {
//            var storage = new Storage.JsonStorage(GetPath(Location));

//            Presets = new List<Dictionary<string, string>>();

//            foreach (var fileName in Directory.GetFiles(GetPath(Location)))
//            {
//                var file = File.ReadAllLines($"{GetPath(Location)}/{fileName}");

//                Preset = new Dictionary<string, string>();

//                Preset.Add("URL", File.ReadAllLines(fileName)[0]);
//                Preset.Add("Settings", Settings);
//                Presets.Add(Preset);
//            }
//        }

//        public void Save(string name, Locations location)
//        {
//            for (var i = 0; i < Presets.Count; i++)
//            {
//                var data = string.Empty;
//                data += Presets./*Val*/;
//            }
//        }

//        private string GetPath(Locations location)
//        {
//            switch (location)
//            {
//                case Locations.City:
//                    return path + "/City";
//                case Locations.Forest:
//                    return path + "/Forest";
//                default:
//                    return string.Empty;
//            }
//        }
//    }
//}