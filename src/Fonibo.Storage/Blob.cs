using System;
using System.IO;

namespace Fonibo.Storage
{
    public class Blob : IDisposable
    {
        public Blob(string key, string bucketName, string publicUrl, Stream? stream)
        {
            Key = key;
            BucketName = bucketName;
            Stream = stream;
            Url = publicUrl;
        }

        public string Key { get; }
        public string BucketName { get; }
        public string Url { get; }
        public Stream? Stream { get; }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}
