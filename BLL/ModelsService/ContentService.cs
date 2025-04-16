using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class ContentService
    {
        protected readonly IMapper _mapper;
        protected UnitOfWork unitOfWork = new UnitOfWork();

        public ContentService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ContentItemDto GetByID(int id)
        {
            var content = unitOfWork.ContentRepository.GetByID(id);
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            var contentDto = _mapper.Map<ContentItemDto>(content);
            return contentDto;
        }

        public void UpdateContent(ContentItemDto itemDto)
        {
            var existing = unitOfWork.ContentRepository.GetByID(itemDto.ContentItemId);
            if (existing == null)
            {
                throw new ContentNotFoundException();
            }
            _mapper.Map(itemDto, existing);

            unitOfWork.ContentRepository.Update(existing);
            unitOfWork.Save();
        }

        public void DeleteContent(int id)
        {
            unitOfWork.ContentRepository.Delete(id);
            unitOfWork.Save();
        }

        protected void Add(ContentItemDto item)
        {
            var content = _mapper.Map<ContentItem>(item);
            unitOfWork.ContentRepository.Insert(content);
            unitOfWork.Save();
        }
    }
}
