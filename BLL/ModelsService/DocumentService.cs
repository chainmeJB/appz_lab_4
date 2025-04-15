using DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.ModelsService
{
    public class DocumentService : ContentService
    {
        public void AddDocument(string title, string format, int storageId, DateTime creationDate, string filePath)
        {
            Add(new Document
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                CreationDate = creationDate,
                FilePath = filePath
            });
        }

        public IEnumerable<Document> GetAllDocuments()
        {
            return unitOfWork.ContentRepository.Get().OfType<Document>().ToList();
        }

        public Document GetDocumentByID(int id)
        {
            var content = GetByID(id);
            var documents = GetAllDocuments();

            foreach (var document in documents)
            {
                if (document.ContentItemId == content.ContentItemId)
                {
                    return document;
                }
            }
            return null;
        }
    }
}
