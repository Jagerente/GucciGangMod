using System;
using UnityEngine;

namespace GGM.GUI.Pages
{
    public abstract class Page<T> : MonoBehaviour where T : MonoBehaviour
    {
        internal static T Instance;


        private void Awake()
        {
            Instance = this as T;
        }

        internal void Enable()
        {
            Instance.gameObject.SetActive(true);
        }

        internal void Disable()
        {
            Instance.gameObject.SetActive(false);
        }
    }
}