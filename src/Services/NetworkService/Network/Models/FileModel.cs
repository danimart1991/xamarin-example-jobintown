using Network.Contracts.Models.Interfaces;

namespace Network.Models
{
    public class FileModel : IFile
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public byte[] Bytes { get; set; }
    }
}
