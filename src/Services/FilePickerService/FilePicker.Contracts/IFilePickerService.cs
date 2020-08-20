using System.Threading.Tasks;
using Plugin.FilePicker.Abstractions;

namespace FilePicker.Contracts
{
    public interface IFilePickerService
    {
        Task<FileData> PickFile();

        Task<FileData> PickFile(params string[] fileExtensions);

        Task<FileData> PickImage();

        Task<bool> SaveFile(FileData fileToSave);

        void OpenFile(string fileToOpen);

        void OpenFile(FileData fileToOpen);
    }
}
