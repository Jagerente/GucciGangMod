using System.Collections.Generic;
using UnityEngine;

public class PhotonStream
{
    private byte currentItem;
    internal List<object> data;
    private bool write;

    public PhotonStream(bool write, object[] incomingData)
    {
        this.write = write;
        if (incomingData == null)
        {
            data = new List<object>();
        }
        else
        {
            data = new List<object>(incomingData);
        }
    }

    public object ReceiveNext()
    {
        if (write)
        {
            Debug.LogError("Error: you cannot read this stream that you are writing!");
            return null;
        }

        var obj2 = data[currentItem];
        currentItem = (byte) (currentItem + 1);
        return obj2;
    }

    public void SendNext(object obj)
    {
        if (!write)
        {
            Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
        }
        else
        {
            data.Add(obj);
        }
    }

    public void Serialize(ref PhotonPlayer obj)
    {
        if (write)
        {
            data.Add(obj);
        }
        else if (data.Count > currentItem)
        {
            obj = (PhotonPlayer) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref bool myBool)
    {
        if (write)
        {
            data.Add(myBool);
        }
        else if (data.Count > currentItem)
        {
            myBool = (bool) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref char value)
    {
        if (write)
        {
            data.Add(value);
        }
        else if (data.Count > currentItem)
        {
            value = (char) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref short value)
    {
        if (write)
        {
            data.Add(value);
        }
        else if (data.Count > currentItem)
        {
            value = (short) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref int myInt)
    {
        if (write)
        {
            data.Add(myInt);
        }
        else if (data.Count > currentItem)
        {
            myInt = (int) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref float obj)
    {
        if (write)
        {
            data.Add(obj);
        }
        else if (data.Count > currentItem)
        {
            obj = (float) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref string value)
    {
        if (write)
        {
            data.Add(value);
        }
        else if (data.Count > currentItem)
        {
            value = (string) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref Quaternion obj)
    {
        if (write)
        {
            data.Add(obj);
        }
        else if (data.Count > currentItem)
        {
            obj = (Quaternion) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref Vector2 obj)
    {
        if (write)
        {
            data.Add(obj);
        }
        else if (data.Count > currentItem)
        {
            obj = (Vector2) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public void Serialize(ref Vector3 obj)
    {
        if (write)
        {
            data.Add(obj);
        }
        else if (data.Count > currentItem)
        {
            obj = (Vector3) data[currentItem];
            currentItem = (byte) (currentItem + 1);
        }
    }

    public object[] ToArray()
    {
        return data.ToArray();
    }

    public int Count
    {
        get { return data.Count; }
    }

    public bool isReading
    {
        get { return !write; }
    }

    public bool isWriting
    {
        get { return write; }
    }
}