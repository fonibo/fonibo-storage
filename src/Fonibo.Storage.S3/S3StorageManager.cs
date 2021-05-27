using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Fonibo.Storage.S3
{
    public class S3StorageManager : IStorageManager
    {
        private readonly IAmazonS3 _s3Client;

        private static readonly Dictionary<BlobAccessType, S3CannedACL> BlobAccessTypeToS3ChannelAcl = new Dictionary<BlobAccessType, S3CannedACL>
        {
            { BlobAccessType.Private, S3CannedACL.Private },
            { BlobAccessType.Public, S3CannedACL.PublicRead },
        };

        private static readonly Dictionary<BlobStorageClass, S3StorageClass> BlobStorageClassToS3S = new Dictionary<BlobStorageClass, S3StorageClass>
        {
            { BlobStorageClass.Standard, S3StorageClass.Standard },
            { BlobStorageClass.InfrequentAccess, S3StorageClass.StandardInfrequentAccess },
            { BlobStorageClass.Archive, S3StorageClass.Glacier },
            { BlobStorageClass.DeepArchive, S3StorageClass.DeepArchive },
        };

        public S3StorageManager(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        private static string S3Url(string bucketName, string key)
        {
            return $"s3://{bucketName}/{key}";
        }

        public async Task<Blob> DownloadBlobAsync(string bucketName, string key)
        {
            var response = await _s3Client.GetObjectAsync(bucketName, key);
            return new Blob(key, bucketName, S3Url(bucketName, key), response.ResponseStream);
        }

        public async Task<Blob> UploadBlobAsync(string bucketName, string key, Stream stream, UploadOptions? options)
        {
            options ??= UploadOptions.Default;

            var uploadRequest = new TransferUtilityUploadRequest
            {
                Key = key,
                InputStream = stream,
                BucketName = bucketName,
                CannedACL = BlobAccessTypeToS3ChannelAcl[options.AccessType],
                StorageClass = BlobStorageClassToS3S[options.StorageClass],
                AutoCloseStream = options.AutoCloseStream,
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            return new Blob(key, bucketName, S3Url(bucketName, key), null);
        }

        public Task<Blob> DownloadBlobAsync(string url)
        {
            var uri = new Uri(url);
            if (uri.Scheme.ToLower() != "s3")
            {
                throw new NotSupportedException("Invalid S3 object url supplied.");
            }

            return DownloadBlobAsync(uri.Host, uri.AbsolutePath[1..]);
        }
    }
}
