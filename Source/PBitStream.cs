using System;
using System.Collections.Generic;

public class PBitStream
{
    private List<byte> streamBytes;
    private int totalBits;

    public PBitStream()
    {
        streamBytes = new List<byte>(1);
    }

    public PBitStream(int bitCount)
    {
        streamBytes = new List<byte>(BytesForBits(bitCount));
    }

    public PBitStream(IEnumerable<byte> bytes, int bitCount)
    {
        streamBytes = new List<byte>(bytes);
        BitCount = bitCount;
    }

    public void Add(bool val)
    {
        var num = totalBits / 8;
        if (num > streamBytes.Count - 1 || totalBits == 0)
        {
            streamBytes.Add(0);
        }
        if (val)
        {
            var currentByteBits = 7 - totalBits % 8;
            streamBytes[num] |= (byte)(1 << currentByteBits);
        }
        totalBits++;
    }

    public static int BytesForBits(int bitCount)
    {
        if (bitCount <= 0)
        {
            return 0;
        }
        return (bitCount - 1) / 8 + 1;
    }

    public bool Get(int bitIndex)
    {
        var num = bitIndex / 8;
        var num2 = 7 - bitIndex % 8;
        return (streamBytes[num] & (byte)(1 << num2)) > 0;
    }

    public bool GetNext()
    {
        int num;
        if (Position > totalBits)
        {
            throw new Exception("End of PBitStream reached. Can't read more.");
        }
        Position = (num = Position) + 1;
        return Get(num);
    }

    public void Set(int bitIndex, bool value)
    {
        var byteIndex = bitIndex / 8;
        var bitInByIndex = 7 - bitIndex % 8;
        streamBytes[byteIndex] |= (byte)(1 << bitInByIndex);
    }

    public byte[] ToBytes()
    {
        return streamBytes.ToArray();
    }

    public int BitCount
    {
        get
        {
            return totalBits;
        }
        private set
        {
            totalBits = value;
        }
    }

    public int ByteCount
    {
        get
        {
            return BytesForBits(totalBits);
        }
    }

    public int Position { get; set; }
}