using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.IModelServices;
using BLL.ModelsService;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BookService : IBookService
    {
        private readonly IContentService _contentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IContentService contentService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contentService = contentService;
            _mapper = mapper;
        }

        public void AddBook(BookDto book)
        {
            _contentService.AddContent(book);
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = _unitOfWork.ContentRepository.Get().OfType<Book>().ToList();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public BookDto GetBookByID(int id)
        {
            var content = _unitOfWork.ContentRepository.GetByID(id) as Book;
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            return _mapper.Map<BookDto>(content);
        }
    }
}
