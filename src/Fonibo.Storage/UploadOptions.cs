namespace Fonibo.Storage
{
    public class UploadOptions
    {
        public static readonly UploadOptions Default = new UploadOptions();

        public static readonly UploadOptions Public = new UploadOptions
        {
            AccessType = BlobAccessType.Public,
            StorageClass = BlobStorageClass.Standard,
            AutoCloseStream = true,
        };

        public BlobStorageClass StorageClass { get; set; } = BlobStorageClass.Standard;
        public BlobAccessType AccessType { get; set; } = BlobAccessType.Private;
        public bool AutoCloseStream { get; set; } = true;
    }
}
