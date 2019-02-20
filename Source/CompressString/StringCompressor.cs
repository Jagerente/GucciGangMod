namespace CompressString
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    internal static class StringCompressor
    {
        public static string CompressString(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var stream = new MemoryStream();
            using (var stream2 = new GZipStream(stream, CompressionMode.Compress, true))
            {
                stream2.Write(bytes, 0, bytes.Length);
            }
            stream.Position = 0L;
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            var dst = new byte[buffer.Length + 4];
            Buffer.BlockCopy(buffer, 0, dst, 4, buffer.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, dst, 0, 4);
            return Convert.ToBase64String(dst);
        }

        public static string DecompressString(string compressedText)
        {
            var buffer = Convert.FromBase64String(compressedText);
            using (var stream = new MemoryStream())
            {
                var num = BitConverter.ToInt32(buffer, 0);
                stream.Write(buffer, 4, buffer.Length - 4);
                var array = new byte[num];
                stream.Position = 0L;
                using (var stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    stream2.Read(array, 0, array.Length);
                }
                return Encoding.UTF8.GetString(array);
            }
        }
    }
}

