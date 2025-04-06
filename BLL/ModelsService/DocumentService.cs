using DAL;
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
            var document = new Document()
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                CreationDate = creationDate,
                FilePath = filePath
            };

            _context.Contents.Add(document);
            _context.SaveChanges();
        }

        public Document GetDocumentByTitle(string title)
        {
            return _context.Set<Document>().FirstOrDefault(d => d.Title == title);
        }

        public List<Document> GetAllDocuments()
        {
            return _context.Set<Document>().ToList();
        }
    }

}
