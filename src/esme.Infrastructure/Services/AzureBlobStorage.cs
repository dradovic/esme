using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace esme.Infrastructure.Services
{
    public class AzureBlobStorageOptions
    {
        public string ConnectionString { get; set; }
    }

    public class AzureBlobStorage
    {
        private AzureBlobStorageOptions _options { get; }

        public AzureBlobStorage(IOptions<AzureBlobStorageOptions> options)
        {
            _options = options.Value;
        }

        public async Task<Uri> StoreBytesAsync(int circleId, string fileName, byte[] file, string location)
        {
            var blob = await GetBlockBlobReference(circleId, location, fileName);
            await blob.UploadFromByteArrayAsync(file, 0, file.Length);
            return blob.Uri;
        }

        public async Task DeleteContainerAsync(int circleId)
        {
            var messagesContainer = GetMessagesContainer(circleId);
            await messagesContainer.DeleteIfExistsAsync();
        }

        private CloudBlobContainer GetMessagesContainer(int circleId)
        {
            var storageAccount = CloudStorageAccount.Parse(_options.ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference("Circle" + circleId); // FIXME: da, use Guids for Circles too?
        }

        private async Task<CloudBlockBlob> GetBlockBlobReference(int circleId, string location, string fileName)
        {
            var messagesContainer = GetMessagesContainer(circleId);
            bool containerExisted = await messagesContainer.ExistsAsync();
            await messagesContainer.CreateIfNotExistsAsync(); // always call this to eliminate race conditions
            if (!containerExisted)
            {
                var permissions = await messagesContainer.GetPermissionsAsync();
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                await messagesContainer.SetPermissionsAsync(permissions);
            }
            return messagesContainer.GetBlockBlobReference(location + "/" + fileName);
        }
    }
}