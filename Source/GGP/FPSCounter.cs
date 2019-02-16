using UnityEngine;

namespace GGP
{
    class FPSCounter : MonoBehaviour
    {
        int fps = 0;
        internal static int FPS { get; private set; }
        float time = 1f;

        internal void SetActive(bool stat)
        {
            gameObject.SetActive(stat);
        }

        private void Update()
        {
            time -= Time.deltaTime;
            fps++;
            if (time <= 0f)
            {
                time = 1f;
                FPS = fps;
                fps = 0;
            }
        }
    }
}
