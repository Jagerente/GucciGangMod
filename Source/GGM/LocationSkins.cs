using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace GGM
{
    internal class LocationSkins
    {
        public Locations Location;
        public string Settings;
        public string Title;
        public string URL;
        private static readonly string[] expandedSettings = 
            {
                "Ambient",
                "AmbientR", "AmbientG", "AmbientB",
                "Fog",
                "FogR", "FogG", "FogB",
                "Light",
                "LightR", "LightG", "LightB",
                "Particles",
                "ParticlesCount", "ParticlesHeight",
                "ParticlesLMin", "ParticlesLMax",
                "ParticlesGravity",
                "ParticlesR", "ParticlesG", "ParticlesB"
            };
        private readonly string path = Application.dataPath + "/Skins/Locations";
        private string filePath;

        private Dictionary<string, string> Preset;

        private Dictionary<string, Dictionary<string, string>> Presets;

        public LocationSkins(Locations location)
        {
            Location = location;

            Load();
        }

        public enum Locations { City, Forest };

        public void Load()
        {
            var storage = new Storage.JsonStorage(GetPath(Location));
            if (!File.Exists(GetPath(Location) + "/Sakura.txt"))
            {
                Preset = new Dictionary<string, string>(); 

                Preset.Add("URL", "`````````" + "https://i.imgur.com/tAzxZjG.png`" + "https://i.imgur.com/p4lwfdl.png`" + "https://i.imgur.com/rilg26V.png`" + "https://i.imgur.com/tAzxZjG.png`" + "https://i.imgur.com/p4lwfdl.png`" + "https://i.imgur.com/rilg26V.png`" + "https://i.imgur.com/tAzxZjG.png`" + "https://i.imgur.com/p4lwfdl.png`" + "https://i.imgur.com/fxbU9wh.jpg`" + "https://i.imgur.com/SASIAcM.jpg`" + "https://i.imgur.com/V5dey1B.jpg`" + "https://i.imgur.com/lRBZmja.jpg`" + "https://i.imgur.com/PhjVKO4.jpg`" + "https://i.imgur.com/i7mzHHN.jpg");
                Preset.Add("Settings", "1`0.85`0.5`0.81`1`0.865`0.6`0.775`0`650");
                storage.StoreObject(Preset, "Sakura.txt");
            }

            foreach (var file in Directory.GetFiles(GetPath(Location)))
            {
                Preset = new Dictionary<string, string>();

                var data = storage.RestoreObject<Dictionary<string, string>>(file + ".txt");

                foreach (var key in GetKeys(Location))
                {
                    URL += data[key] + ",";

                }

                foreach (var key in expandedSettings)
                {
                    Settings += data[key] + ",";
                }

                Preset.Add("URL", URL);
                Preset.Add("Settings", Settings);
                Presets.Add(file, Preset);
            }
        }

        public void Save(string name, Locations location)
        {
            var storage = new Storage.JsonStorage(GetPath(location));

            for (var i = 0; i < Presets.Count; i++)
            {
                storage.StoreObject(Presets.Values.ElementAt(i), Presets.Keys.ElementAt(i));
            }
        }

        private static IEnumerable<string> GetKeys(Locations location)
        {
            return location == Locations.City ? new []
            {
                "Ground", "Wall", "Gate",
                "Houses #1", "Houses #2", "Houses #3", "Houses #4",
                "Houses #5", "Houses #6", "Houses #7", "Houses #8",
                "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right", "Skybox Up", "Skybox Down"
            } : new []
            {
                "Ground",
                "Forest Trunk #1", "Forest Trunk #2", "Forest Trunk #3", "Forest Trunk #4",
                "Forest Trunk #5", "Forest Trunk #6", "Forest Trunk #7", "Forest Trunk #8",
                "Forest Leave #1", "Forest Leave #2", "Forest Leave #3", "Forest Leave #4",
                "Forest Leave #5", "Forest Leave #6", "Forest Leave #7", "Forest Leave #8",
                "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right", "Skybox Up", "Skybox Down"
            };
        }

        private string GetPath(Locations location)
        {
            switch (location)
            {
                case Locations.City:
                    return path + "/City";
                case Locations.Forest:
                    return path + "/Forest";
                default:
                    return string.Empty;
            }
        }
    }
}