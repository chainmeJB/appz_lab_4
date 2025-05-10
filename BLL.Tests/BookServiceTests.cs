using AutoFixture;
using BLL.DTO;
using BLL.Exceptions;
using BLL.IModelServices;
using BLL.ModelsService;
using DAL.DataModels;
using NSubstitute;

namespace BLL.Tests
{
    public class BookServiceTests : BaseServiceTests
    {
        private readonly IBookService _bookService;
        private readonly IContentService _contentService;

        public BookServiceTests() : base()
        {
            _contentService = new ContentService(_mockUnitOfWork, _mockMapper);
            _bookService = new BookService(_mockUnitOfWork, _contentService, _mockMapper);
        }

        [Fact]
        public void AddBook_ShouldInsertBookAndSave()
        {
            // Arrange
            var bookDto = _fixture.Create<BookDto>();
            var bookEntity = _fixture.Create<Book>();

            _mockMapper.Map<ContentItem>(bookDto).Returns(bookEntity);

            // Act
            _bookService.AddBook(bookDto);

            // Assert
            _mockContentRepository.Received(1).Insert(bookEntity);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBookDtos()
        {
            // Arrange
            var bookEntities = _fixture.CreateMany<Book>(2).ToList();
            var expectedDtos = _fixture.CreateMany<BookDto>(2).ToList();

            _mockContentRepository.Get().Returns(bookEntities);
            _mockMapper.Map<IEnumerable<BookDto>>(Arg.Any<IEnumerable<Book>>()).Returns(expectedDtos);

            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            Assert.Equal(expectedDtos, result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetAllBooks_WithEmptyRepository_ShouldReturnEmptyCollection()
        {
            // Arrange
            var emptyEntities = new List<Book>();
            var emptyDtos = new List<BookDto>();

            _mockContentRepository.Get().Returns(emptyEntities.AsQueryable());
            _mockMapper.Map<IEnumerable<BookDto>>(emptyEntities).Returns(emptyDtos);

            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            Assert.Empty(result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetBookByID_WithExistingId_ShouldReturnBookDto()
        {
            // Arrange
            var bookEntity = _fixture.Create<Book>();
            var expectedDto = _fixture.Create<BookDto>();

            _mockContentRepository.GetByID(bookEntity.ContentItemId).Returns(bookEntity);
            _mockMapper.Map<BookDto>(bookEntity).Returns(expectedDto);

            // Act
            var result = _bookService.GetBookByID(bookEntity.ContentItemId);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockContentRepository.Received(1).GetByID(bookEntity.ContentItemId);
        }

        [Fact]
        public void GetBookByID_WithNonExistingId_ShouldThrowContentNotFoundException()
        {
            // Arrange
            var id = 999;
            _mockContentRepository.GetByID(id).Returns((Book)null!);

            // Act & Assert
            Assert.Throws<ContentNotFoundException>(() => _bookService.GetBookByID(id));
            _mockContentRepository.Received(1).GetByID(id);
        }
    }
}