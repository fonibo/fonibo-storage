namespace Fonibo.Storage
{
    public enum BlobStorageClass
    {
        /// <summary>
        /// Highest availability and reliability
        /// </summary>
        Standard,

        /// <summary>
        /// Same reliability as standard but a bit less availability
        /// </summary>
        InfrequentAccess,

        /// <summary>
        /// Has maximum durability but less availability
        /// </summary>
        Archive,

        /// <summary>
        /// Has maximum durability but very less availability
        /// </summary>
        DeepArchive,
    }
}
