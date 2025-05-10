using BLL.DTO;
using System.Collections.Generic;

namespace BLL.IModelServices
{
    public interface IBookService
    {
        void AddBook(BookDto book);
        IEnumerable<BookDto> GetAllBooks();
        BookDto GetBookByID(int id);
    }
}
