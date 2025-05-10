using DAL.DataModels;
using BLL.ModelsService;
using BLL.DTO;
using BLL.Exceptions;
using AutoFixture;
using NSubstitute;
using BLL.IModelServices;

namespace BLL.Tests
{
    public class DocumentServiceTests : BaseServiceTests
    {
        private readonly IDocumentService _documentService;
        private readonly IContentService _contentService;

        public DocumentServiceTests() : base()
        {
            _contentService = new ContentService(_mockUnitOfWork, _mockMapper);
            _documentService = new DocumentService(_mockUnitOfWork, _contentService, _mockMapper);
        }

        [Fact]
        public void AddDocument_ShouldInsertDocumentAndSave()
        {
            // Arrange
            var documentDto = _fixture.Create<DocumentDto>();
            var documentEntity = _fixture.Create<Document>();

            _mockMapper.Map<ContentItem>(documentDto).Returns(documentEntity);

            // Act
            _documentService.AddDocument(documentDto);

            // Assert
            _mockContentRepository.Received(1).Insert(documentEntity);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void GetAllDocuments_ShouldReturnAllDocumentDtos()
        {
            // Arrange
            var documentEntities = _fixture.CreateMany<Document>(2).ToList();
            var expectedDtos = _fixture.CreateMany<DocumentDto>(2).ToList();

            _mockContentRepository.Get().Returns(documentEntities);
            _mockMapper.Map<IEnumerable<DocumentDto>>(Arg.Any<IEnumerable<Document>>()).Returns(expectedDtos);

            // Act
            var result = _documentService.GetAllDocuments();

            // Assert
            Assert.Equal(expectedDtos, result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetAllDocuments_WithEmptyRepository_ShouldReturnEmptyCollection()
        {
            // Arrange
            var emptyList = new List<Document>();
            var emptyDtoList = new List<DocumentDto>();

            _mockContentRepository.Get().Returns(emptyList.AsQueryable());
            _mockMapper.Map<IEnumerable<DocumentDto>>(emptyList).Returns(emptyDtoList);

            // Act
            var result = _documentService.GetAllDocuments();

            // Assert
            Assert.Empty(result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetDocumentByID_WithExistingId_ShouldReturnDocumentDto()
        {
            // Arrange
            var documentEntity = _fixture.Create<Document>();
            var expectedDto = _fixture.Create<DocumentDto>();

            _mockContentRepository.GetByID(documentEntity.ContentItemId).Returns(documentEntity);
            _mockMapper.Map<DocumentDto>(documentEntity).Returns(expectedDto);

            // Act
            var result = _documentService.GetDocumentByID(documentEntity.ContentItemId);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockContentRepository.Received(1).GetByID(documentEntity.ContentItemId);
        }

        [Fact]
        public void GetDocumentByID_WithNonExistingId_ShouldThrowContentNotFoundException()
        {
            // Arrange
            var id = 999;
            _mockContentRepository.GetByID(id).Returns((Document)null!);

            // Act & Assert
            Assert.Throws<ContentNotFoundException>(() => _documentService.GetDocumentByID(id));
            _mockContentRepository.Received(1).GetByID(id);
        }
    }
}