using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class ContentService
    {
        protected readonly LibraryContext _context = new LibraryContext();
        protected readonly StorageService storageService = new StorageService();

        public ContentItem GetContentByTitle(string title)
        {
            return _context.Contents.FirstOrDefault(b => b.Title == title);
        }

        public List<ContentItem> GetAllContents()
        {
            return _context.Contents.ToList();
        }

        public void UpdateContentStorage(string title, int storageId)
        {
            var content = GetContentByTitle(title);
            if (content == null)
            {
                throw new ContentNotFoundException();
            }

            var storage = storageService.GetAllStorages()
                                         .FirstOrDefault(s => s.StorageId == storageId);
            if (storage == null)
            {
                throw new StorageNotFoundException();
            }

            content.StorageId = storage.StorageId;
            _context.SaveChanges();
        }

        public void DeleteContent(string title)
        {
            var content = GetContentByTitle(title);
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            _context.Contents.Remove(content);
            _context.SaveChanges();
        }
    }
}
