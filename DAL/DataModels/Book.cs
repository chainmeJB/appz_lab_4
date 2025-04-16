
namespace DAL.DataModels
{
    public class Book : ContentItem
    {
        public string Author { get; set; }
        public int PageCount { get; set; }
    }
}
