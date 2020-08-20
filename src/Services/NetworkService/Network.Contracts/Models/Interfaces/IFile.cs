namespace Network.Contracts.Models.Interfaces
{
    public interface IFile
    {
        string Name { get; set; }

        string FileName { get; set; }

        byte[] Bytes { get; set; }
    }
}
