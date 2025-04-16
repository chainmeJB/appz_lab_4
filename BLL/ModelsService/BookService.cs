using AutoMapper;
using BLL.DTO;
using BLL.ModelsService;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BookService : ContentService
    {
        public BookService(IMapper mapper) : base(mapper) { }

        public void AddBook(BookDto book)
        {
            Add(book);
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = unitOfWork.ContentRepository.Get().OfType<Book>().ToList();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public BookDto GetBookByID(int id)
        {
            var contentDto = GetByID(id);
            return GetAllBooks().FirstOrDefault(book => book.ContentItemId == contentDto.ContentItemId);
        }
    }
}
