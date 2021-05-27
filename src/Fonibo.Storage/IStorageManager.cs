using System.IO;
using System.Threading.Tasks;

namespace Fonibo.Storage
{
    public interface IStorageManager
    {
        Task<Blob> DownloadBlobAsync(string bucketName, string key);
        Task<Blob> UploadBlobAsync(string bucketName, string key, Stream stream, UploadOptions? options = null);
        Task<Blob> DownloadBlobAsync(string url);
    }
}
