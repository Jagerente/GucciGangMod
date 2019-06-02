using UnityEngine;

public class SupportLogger : MonoBehaviour
{
    public bool LogTrafficStats = true;

    public void Start()
    {
        if (GameObject.Find("PunSupportLogger") == null)
        {
            var target = new GameObject("PunSupportLogger");
            DontDestroyOnLoad(target);
            target.AddComponent<SupportLogging>().LogTrafficStats = LogTrafficStats;
        }
    }
}