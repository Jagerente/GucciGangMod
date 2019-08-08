using UnityEngine;

public class TitanSpawner
{
    public TitanSpawner()
    {
        name = string.Empty;
        location = new Vector3(0f, 0f, 0f);
        time = 30f;
        endless = false;
        delay = 30f;
    }

    public void resetTime()
    {
        time = delay;
    }

    public float delay { get; set; }

    public bool endless { get; set; }

    public Vector3 location { get; set; }

    public string name { get; set; }

    public float time { get; set; }
}