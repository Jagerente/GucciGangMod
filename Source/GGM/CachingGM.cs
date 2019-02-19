using System.Collections.Generic;
using UnityEngine;

namespace GGM
{
    public static class CachingsGM
    {
        private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();
        private static Dictionary<string, Component> cacheType = new Dictionary<string, Component>();

        public static GameObject Find(string name)
        {
            GameObject obj2;
            string str = name.ToLower().Trim();
            switch (str)
            {
                case "maincamera":
                    if (!cache.ContainsKey(name) || (cache[name] == null))
                    {
                        GameObject obj3;
                        cache[name] = obj3 = GameObject.Find(name);
                        return obj3;
                    }
                    return cache[name];

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
            if (((!cache.ContainsKey(name) || ((obj2 = cache[name]) == null)) || ((!obj2.activeInHierarchy && !str.StartsWith("ui")) && (!str.StartsWith("label") && !str.StartsWith("ngui")))) && ((obj2 = GameObject.Find(name)) != null))
            {
                GameObject obj4;
                cache[name] = obj4 = obj2;
                return obj4;
            }
            return obj2;
        }

        public static T Find<T>(string name) where T : Component
        {
            string key = name + typeof(T).FullName;
            if (cacheType.ContainsKey(key))
            {
                Component component = cacheType[key];
                if (component != null)
                {
                    Component component2;
                    T local = component as T;
                    if (local != null)
                    {
                        return local;
                    }
                    cacheType[key] = component2 = component.GetComponent<T>();
                    return (T)component2;
                }
            }
            GameObject obj2 = Find(name);
            if (obj2 != null)
            {
                Component component3;
                cacheType[key] = component3 = obj2.GetComponent<T>();
                return (T)component3;
            }
            obj2 = GameObject.Find(name);
            if (obj2 != null)
            {
                Component component4;
                cacheType[key] = component4 = obj2.GetComponent<T>();
                return (T)component4;
            }
            return default(T);
        }
    }
}