using System.Collections.Generic;
using UnityEngine;

namespace GGM
{
    public static class GameObjectsCache
    {
        private static readonly Dictionary<string, GameObject> _cache = new Dictionary<string, GameObject>();
        private static readonly Dictionary<string, Component> _cacheType = new Dictionary<string, Component>();

        public static GameObject Find(string name)
        {
            GameObject obj2;
            var str = name.ToLower().Trim();
            switch (str)
            {
                case "maincamera":
                    if (!_cache.ContainsKey(name) || _cache[name] == null)
                    {
                        GameObject obj3;
                        _cache[name] = obj3 = GameObject.Find(name);
                        return obj3;
                    }
                    return _cache[name];

                case "aottg_hero1":
                case "aottg_hero1(clone)":
                case "colossal_titan":
                case "femaletitan":
                case "female_titan":
                case "crawler":
                case "punk":
                case "abberant":
                case "jumper":
                case "titan":
                case "tree":
                case "cube001":
                    return GameObject.Find(name);
            }
            if ((!_cache.ContainsKey(name) || (obj2 = _cache[name]) == null || !obj2.activeInHierarchy && !str.StartsWith("ui") && !str.StartsWith("label") && !str.StartsWith("ngui")) && (obj2 = GameObject.Find(name)) != null)
            {
                GameObject obj4;
                _cache[name] = obj4 = obj2;
                return obj4;
            }
            return obj2;
        }

        public static T Find<T>(string name) where T : Component
        {
            var key = name + typeof(T).FullName;
            if (_cacheType.ContainsKey(key))
            {
                var component = _cacheType[key];
                if (component != null)
                {
                    Component component2;
                    var local = component as T;
                    if (local != null)
                    {
                        return local;
                    }
                    _cacheType[key] = component2 = component.GetComponent<T>();
                    return (T)component2;
                }
            }
            var obj2 = Find(name);
            if (obj2 != null)
            {
                Component component3;
                _cacheType[key] = component3 = obj2.GetComponent<T>();
                return (T)component3;
            }
            obj2 = GameObject.Find(name);
            if (obj2 != null)
            {
                Component component4;
                _cacheType[key] = component4 = obj2.GetComponent<T>();
                return (T)component4;
            }
            return default(T);
        }
    }
}