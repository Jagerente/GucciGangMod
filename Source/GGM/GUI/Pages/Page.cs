using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.GUI.Pages
{
    public abstract class Page : MonoBehaviour
    {
        private static Dictionary<Type, Page> pageCache = new Dictionary<Type, Page>();
        internal Page Instance;

        private void Awake()
        {
            Instance = this;
        }

        internal void Disable()
        {
            gameObject.SetActive(false);
        }

        internal void Enable()
        {
            gameObject.SetActive(true);
        }

        internal static T GetInstance<T>() where T : Page
        {
            if (pageCache.TryGetValue(typeof(T), out var result))
            {
                return result as T;
            }
            var gos = FindObjectsOfType<T>();
            if (gos.Length > 1)
            {
                throw new Exception($"Instance of class {typeof(T).Name} is not only one!");
            }
            switch (gos.Length)
            {
                case 0:
                    result = new GameObject(typeof(T).Name + "Instance").AddComponent<T>();
                    DontDestroyOnLoad(result);
                    pageCache.Add(typeof(T), result);
                    break;

                case 1:
                    result = gos[0];
                    pageCache.Add(typeof(T), result);
                    break;
            }

            return (T)result;
        }
    }
}