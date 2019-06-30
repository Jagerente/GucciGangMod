﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SimpleAES
{
    private ICryptoTransform decryptor;
    private UTF8Encoding encoder;
    private ICryptoTransform encryptor;

    private static byte[] key =
    {
        123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209,
        241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209
    };

    private static byte[] vector =
        {146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156};

    public SimpleAES()
    {
        var managed = new RijndaelManaged();
        encryptor = managed.CreateEncryptor(key, vector);
        decryptor = managed.CreateDecryptor(key, vector);
        encoder = new UTF8Encoding();
    }

    public string Decrypt(string encrypted)
    {
        return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
    }

    public byte[] Decrypt(byte[] buffer)
    {
        return Transform(buffer, decryptor);
    }

    public string Encrypt(string unencrypted)
    {
        return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
    }

    public byte[] Encrypt(byte[] buffer)
    {
        return Transform(buffer, encryptor);
    }

    protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
    {
        var stream = new MemoryStream();
        using (var stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
        {
            stream2.Write(buffer, 0, buffer.Length);
        }

        return stream.ToArray();
    }
}