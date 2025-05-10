using AutoMapper;
using BLL.DTO;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;
using DAL;
using BLL.Exceptions;
using BLL.IModelServices;

namespace BLL.ModelsService
{
    public class DocumentService : IDocumentService
    {
        private readonly IContentService _contentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentService(IUnitOfWork unitOfWork, IContentService contentService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contentService = contentService;
            _mapper = mapper;
        }

        public void AddDocument(DocumentDto document)
        {
            _contentService.AddContent(document);
        }

        public IEnumerable<DocumentDto> GetAllDocuments()
        {
            var documents = _unitOfWork.ContentRepository.Get().OfType<Document>().ToList();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public DocumentDto GetDocumentByID(int id)
        {
            var content = _unitOfWork.ContentRepository.GetByID(id) as Document;
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            return _mapper.Map<DocumentDto>(content);
        }
    }
}
