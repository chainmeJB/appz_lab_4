
namespace DAL.DataModels
{
    public abstract class ContentItem
    {
        public int ContentItemId { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }

        public int StorageId { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
