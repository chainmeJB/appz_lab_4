using System;

namespace BLL.DTO
{
    public class DocumentDto : ContentItemDto
    {
        public DateTime CreationDate { get; set; }
        public string FilePath { get; set; }

        public DocumentDto(string title, string format, int storageId, DateTime creationDate, string filePath)
        {
            Title = title;
            Format = format;
            StorageId = storageId;
            CreationDate = creationDate;
            FilePath = filePath;
        }

        public override string ToString()
        {
            return string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                 ContentItemId, Title, Format, Storage.Name, Storage.Address, CreationDate.ToString("yyyy-MM-dd"), FilePath);
        }
    }
}
