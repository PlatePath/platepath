namespace PlatePath.API.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(string blobName, Stream content);
        Task<Stream> DownloadAsync(string blobName);
        Task<IEnumerable<string>> ListBlobsAsync();
        Task DeleteAsync(string blobName);
    }

}
