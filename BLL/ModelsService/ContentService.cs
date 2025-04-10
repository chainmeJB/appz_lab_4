
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class ContentService
    {
        protected readonly IRepository<ContentItem> contentRepository;

        public ContentService()
        {
            this.contentRepository = new ContentRepository(new LibraryContext());
        }

        public ContentItem GetById(int id)
        {
            return contentRepository.GetById(id);
        }

        public void UpdateContent(ContentItem contentItem)
        {
            contentRepository.Update(contentItem);
            contentRepository.Save();
        }

        public void DeleteContent(int id)
        {
            contentRepository.Delete(id);
            contentRepository.Save();
        }

        protected void Add(ContentItem item)
        {
            contentRepository.Insert(item);
            contentRepository.Save();
        }
    }
}
