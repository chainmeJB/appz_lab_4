using BLL.ModelsService;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BookService : ContentService
    {
        public void AddBook(string title, string format, int storageId, string author, int pageCount)
        {
            Add(new Book
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                Author = author,
                PageCount = pageCount
            });
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return contentRepository.GetAll().OfType<Book>().ToList();
        }

        public Book GetBookById(int id)
        {
            var content = contentRepository.GetById(id);
            var books = GetAllBooks();

            foreach (var book in books)
            {
                if (book.ContentItemId == content.ContentItemId)
                {
                    return book;
                }
            }
            return null;
        }
    }
}
