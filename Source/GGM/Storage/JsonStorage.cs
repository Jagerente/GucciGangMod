using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace GGM.Storage
{
    class JsonStorage
    {
        private string Path;

        public JsonStorage(string path)
        {
            Path = path;
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        public T GetValue<T>(string file, string key)
        {
            var data = RestoreObject<Dictionary<string, T>>(file);

            return data[key];
        }

        public T GetValue<T>(string file, int path)
        {
            var data = RestoreObject<List<T>>(file);

            return data[path];
        }

        public bool KeyExists(string key)
        {
            return LocalFileExists(key);
        }

        public bool LocalFileExists(string file)
        {
            string filePath = string.Concat(Path, "/", file);
            return File.Exists(filePath);
        }

        public T RestoreObject<T>(string file)
        {
            string json = GetOrCreateFileContents(file);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void SetValue<T>(string file, string key, T value)
        {
            var data = RestoreObject<Dictionary<string, T>>(file);

            if (data.ContainsKey(key)) data[key] = value;
            else data.Add(key, value);

            StoreObject(data, file);
        }

        public void StoreObject(object obj, string file)
        {
            string json = JsonConvert.SerializeObject(obj);
            string filePath = string.Concat(Path, "/", file);
            File.WriteAllText(filePath, json);
        }

        private string GetOrCreateFileContents(string file)
        {
            string filePath = string.Concat(Path, "/", file);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
                return string.Empty;
            }
            return File.ReadAllText(filePath);
        }
    }
}
