using Microsoft.Azure.Storage.Blob;

namespace PlatePath.API.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly CloudBlobContainer _container;

        public BlobStorageService(CloudBlobContainer container)
        {
            _container = container;
        }

        public async Task<string> UploadAsync(string blobName, Stream content)
        {
            var blob = _container.GetBlockBlobReference(blobName);
            blob.Properties.ContentType = "image/jpg";
            await blob.UploadFromStreamAsync(content);
            return blob.Uri.AbsoluteUri;
        }

        public async Task<Stream> DownloadAsync(string blobName)
        {
            var blob = _container.GetBlockBlobReference(blobName);
            var stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);
            stream.Position = 0;
            return stream;
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            BlobContinuationToken continuationToken = null;
            var results = new List<string>();
            do
            {
                var response = await _container.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results.Select(b => b.Uri.ToString()));
            } while (continuationToken != null);
            return results;
        }

        public async Task DeleteAsync(string blobName)
        {
            var blob = _container.GetBlockBlobReference(blobName);
            await blob.DeleteIfExistsAsync();
        }
    }

}
