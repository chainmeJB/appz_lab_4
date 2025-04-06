using BLL.ModelsService;
using DAL;
using DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BookService : ContentService
    {
        public void AddBook(string title, string format, int storageId, string author, int pageCount)
        {
            var book = new Book()
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                Author = author,
                PageCount = pageCount
            };

            _context.Contents.Add(book);
            _context.SaveChanges();
        }

        public Book GetBookByTitle(string title)
        {
            return _context.Set<Book>().FirstOrDefault(b => b.Title == title);
        }

        public List<Book> GetAllBooks()
        {
            return _context.Set<Book>().ToList();
        }
    }
}
