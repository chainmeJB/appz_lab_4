
namespace BLL.DTO
{
    public class BookDto : ContentItemDto
    {
        public string Author { get; set; }
        public int PageCount { get; set; }

        public BookDto(string title, string format, int storageId, string author, int pageCount)
        {
            Title = title;
            Format = format;
            StorageId = storageId;
            Author = author;
            PageCount = pageCount;
        }

        public override string ToString()
        {
            return string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                ContentItemId, Title, Format, Storage.Name, Storage.Address, Author, PageCount);
        }
    }
}
