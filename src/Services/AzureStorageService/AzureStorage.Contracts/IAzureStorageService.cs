using System.IO;
using System.Threading.Tasks;

namespace AzureStorage.Contracts
{
    public interface IAzureStorageService
    {
        string ConnectionString { get; set; }

        string ContainerName { get; set; }

        Task<string> PushFile(Stream stream);
    }
}
