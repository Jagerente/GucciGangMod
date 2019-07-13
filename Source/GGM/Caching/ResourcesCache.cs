using System.Collections.Generic;
using UnityEngine;

namespace GGM.Caching
{
    internal static class ResourcesCache
    {
        private static Dictionary<string, Object> cache = new Dictionary<string, Object>();
        private static Dictionary<string, GameObject> cacheRC_GO = new Dictionary<string, GameObject>();
        private static Dictionary<string, Texture2D> cacheRC_T2D = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Material> cacheRC_M = new Dictionary<string, Material>();
        private static Dictionary<string, Component> cacheType = new Dictionary<string, Component>();

        internal static void ClearCache()
        {
            cache = new Dictionary<string, Object>();
            cache.Clear();
            cacheType = new Dictionary<string, Component>();
            cacheType.Clear();
        }

        internal static Object Load(string path)
        {
            cache.TryGetValue(path, out var obj);
            if (obj != null) return obj;
            if (cache.ContainsKey(path)) cache[path] = Resources.Load(path);
            else cache.Add(path, Resources.Load(path));
            return cache[path];
        }

        internal static T Load<T>(string path) where T : Component
        {
            cacheType.TryGetValue(path, out var obj);
            if (obj != null) return obj as T;
            var go = (GameObject)Load(path);
            if (go.GetComponent<T>() != null)
            {
                if (cacheType.ContainsKey(path)) cacheType[path] = go.GetComponent<T>();
                else cacheType.Add(path, go.GetComponent<T>());
                return go.GetComponent<T>();
            }
            return default(T);
        }

        public static GameObject RCLoadGO(string _name)
        {
            var name = _name.StartsWith("RCAsset/") ? _name.Remove(0, 8) : _name;
            if (!cacheRC_GO.ContainsKey(name))
            {
                return cacheRC_GO[name] = (GameObject)FengGameManagerMKII.RCassets.Load(name);
            }
            return cacheRC_GO[name];
        }

        public static Texture2D RCLoadT2D(string _name)
        {
            var name = _name.StartsWith("RCAsset/") ? _name.Remove(0, 8) : _name;
            if (!cacheRC_T2D.ContainsKey(name))
            {
                return cacheRC_T2D[name] = (Texture2D)FengGameManagerMKII.RCassets.Load(name);
            }
            return cacheRC_T2D[name];
        }

        public static Material RCLoadM(string _name)
        {
            var name = _name.StartsWith("RCAsset/") ? _name.Remove(0, 8) : _name;
            if (!cacheRC_M.ContainsKey(name))
            {
                return cacheRC_M[name] = (Material)FengGameManagerMKII.RCassets.Load(name);
            }
            return cacheRC_M[name];
        }
    }
}