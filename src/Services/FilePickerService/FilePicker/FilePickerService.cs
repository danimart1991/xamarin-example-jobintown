using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FilePicker.Contracts;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;

namespace FilePicker
{
    public class FilePickerService : IFilePickerService
    {
        public Task<FileData> PickFile()
        {
            return CrossFilePicker.Current.PickFile();
        }

        public async Task<FileData> PickFile(params string[] fileExtensions)
        {
            var file = await PickFile();
            var extension = Path.GetExtension(file?.FileName);

            if (!fileExtensions.Contains(extension))
            {
                throw new ArgumentException($"File is not a valid file type. Valid types {string.Join(",", fileExtensions)}");
            }

            return file;
        }

        public Task<FileData> PickImage()
        {
            return PickFile(".jpg", ".png", ".JPG", ".PNG");
        }

        public Task<bool> SaveFile(FileData fileToSave)
        {
            return CrossFilePicker.Current.SaveFile(fileToSave);
        }

        public void OpenFile(string fileToOpen)
        {
            CrossFilePicker.Current.OpenFile(fileToOpen);
        }

        public void OpenFile(FileData fileToOpen)
        {
            CrossFilePicker.Current.OpenFile(fileToOpen);
        }
    }
}
