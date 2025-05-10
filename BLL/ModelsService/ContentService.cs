using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.IModelServices;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class ContentService : IContentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ContentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ContentItemDto GetContent(int id)
        {
            var content = _unitOfWork.ContentRepository.GetByID(id);
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            var contentDto = _mapper.Map<ContentItemDto>(content);
            return contentDto;
        }

        public void UpdateContent(ContentItemDto item)
        {
            var existing = _unitOfWork.ContentRepository.GetByID(item.ContentItemId);
            if (existing == null)
            {
                throw new ContentNotFoundException();
            }
            _mapper.Map(item, existing);

            _unitOfWork.ContentRepository.Update(existing);
            _unitOfWork.Save();
        }

        public void DeleteContent(int id)
        {
            var existing = _unitOfWork.ContentRepository.GetByID(id);
            if (existing == null)
            {
                throw new ContentNotFoundException();
            }
            _unitOfWork.ContentRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void AddContent(ContentItemDto item)
        {
            var content = _mapper.Map<ContentItem>(item);
            _unitOfWork.ContentRepository.Insert(content);
            _unitOfWork.Save();
        }
    }
}
