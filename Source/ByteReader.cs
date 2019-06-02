using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ByteReader
{
    private byte[] mBuffer;
    private int mOffset;

    public ByteReader(byte[] bytes)
    {
        mBuffer = bytes;
    }

    public ByteReader(TextAsset asset)
    {
        mBuffer = asset.bytes;
    }

    public Dictionary<string, string> ReadDictionary()
    {
        var dictionary = new Dictionary<string, string>();
        char[] separator = { '=' };
        while (canRead)
        {
            var str = ReadLine();
            if (str == null)
            {
                return dictionary;
            }
            if (!str.StartsWith("//"))
            {
                var strArray = str.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 2)
                {
                    var str2 = strArray[0].Trim();
                    var str3 = strArray[1].Trim().Replace(@"\n", "\n");
                    dictionary[str2] = str3;
                }
            }
        }
        return dictionary;
    }

    public string ReadLine()
    {
        string str;
        var length = mBuffer.Length;
        while (this.mOffset < length && mBuffer[this.mOffset] < 0x20)
        {
            this.mOffset++;
        }
        var mOffset = this.mOffset;
        if (mOffset >= length)
        {
            this.mOffset = length;
            return null;
        }
        while (mOffset < length)
        {
            switch (mBuffer[mOffset++])
            {
                case 10:
                case 13:
                    goto Label_007E;
            }
        }
        mOffset++;
    Label_007E:
        str = ReadLine(mBuffer, this.mOffset, mOffset - this.mOffset - 1);
        this.mOffset = mOffset;
        return str;
    }

    private static string ReadLine(byte[] buffer, int start, int count)
    {
        return Encoding.UTF8.GetString(buffer, start, count);
    }

    public bool canRead
    {
        get
        {
            return mBuffer != null && mOffset < mBuffer.Length;
        }
    }
}

