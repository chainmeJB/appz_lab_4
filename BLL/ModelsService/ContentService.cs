using BLL.Exceptions;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class ContentService
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        public ContentItem GetByID(int id)
        {
            var content = unitOfWork.ContentRepository.GetByID(id);
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            return content;
        }

        public void UpdateContent(ContentItem contentItem)
        {
            unitOfWork.ContentRepository.Update(contentItem);
            unitOfWork.Save();
        }

        public void DeleteContent(int id)
        {
            unitOfWork.ContentRepository.Delete(id);
            unitOfWork.Save();
        }

        protected void Add(ContentItem item)
        {
            unitOfWork.ContentRepository.Insert(item);
            unitOfWork.Save();
        }
    }
}
