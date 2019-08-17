using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
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

        public LocationSkins(Locations location, string title)
        {
            Location = location;

            Title = title;

            URL = string.Empty;

            Settings = string.Empty;

            Load();
        }

        public enum Locations { City, Forest };

        public void Load()
        {
            Preset = new Dictionary<string, string>();

            var storage = new Storage.JsonStorage(GetPath(Location));

            var data = storage.RestoreObject<Dictionary<string, string>>(Title + ".skin");

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
        }

        public void Save(string name, Locations location)
        {
            var storage = new Storage.JsonStorage(GetPath(location));

            storage.StoreObject(Preset, Title);
        }

        private static string[] GetKeys(Locations location)
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