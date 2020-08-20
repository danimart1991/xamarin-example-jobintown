using System;
using System.IO;
using System.Threading.Tasks;
using AzureStorage.Contracts;
using Microsoft.WindowsAzure.Storage;

namespace AzureStorage
{
    public class AzureStorageService : IAzureStorageService
    {
        public string ConnectionString { get; set; }

        public string ContainerName { get; set; }

        public async Task<string> PushFile(Stream stream)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new NullReferenceException("ConnectionString property must be configured.");
            }

            if (string.IsNullOrEmpty(ContainerName))
            {
                throw new NullReferenceException("ContainerName property must be configured.");
            }

            // Conectar con la cuenta Azure Storage.
            // NOTA: Se deben utilizar tokens SAS en lugar de Shared Keys en aplicaciones en producción.
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Crear el contenedor blob si no existe.
            var container = blobClient.GetContainerReference(ContainerName);
            await container.CreateIfNotExistsAsync();

            // Subimos el blob a Azure Storage.
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.Properties.ContentType = "image/png";
            await blob.UploadFromStreamAsync(stream);

            return blob.Uri.ToString();
        }
    }
}
