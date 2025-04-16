using AutoMapper;
using BLL.DTO;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BLL.ModelsService
{
    public class DocumentService : ContentService
    {
        public DocumentService(IMapper mapper) : base(mapper) { }
        public void AddDocument(DocumentDto document)
        {
            Add(document);
        }

        public IEnumerable<DocumentDto> GetAllDocuments()
        {
            var documents = unitOfWork.ContentRepository.Get().OfType<Document>().ToList();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public DocumentDto GetDocumentByID(int id)
        {
            var contentDto = GetByID(id);
            return GetAllDocuments().FirstOrDefault(doc => doc.ContentItemId == contentDto.ContentItemId);
        }
    }
}
