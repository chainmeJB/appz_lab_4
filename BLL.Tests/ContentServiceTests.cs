using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.ModelsService;
using DAL.DataModels;
using DAL;
using NSubstitute;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using BLL.IModelServices;

namespace BLL.Tests
{
    public class ContentServiceTests : BaseServiceTests
    {
        protected readonly IContentService _contentService;

        public ContentServiceTests()
        {
            _contentService = new ContentService(_mockUnitOfWork, _mockMapper);
        }

        [Fact]
        public void GetByID_WithExistingId_ShouldReturnContentDto()
        {
            // Arrange
            var contentEntity = _fixture.Create<ContentItem>();
            var expectedDto = _fixture.Create<ContentItemDto>();

            _mockContentRepository.GetByID(contentEntity.ContentItemId).Returns(contentEntity);
            _mockMapper.Map<ContentItemDto>(contentEntity).Returns(expectedDto);

            // Act
            var result = _contentService.GetContent(contentEntity.ContentItemId);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockContentRepository.Received(1).GetByID(contentEntity.ContentItemId);
        }

        [Fact]
        public void GetByID_WithNonExistingId_ShouldThrowContentNotFoundException()
        {
            // Arrange
            var id = 999;
            _mockContentRepository.GetByID(id).Returns((ContentItem)null!);

            // Act & Assert
            Assert.Throws<ContentNotFoundException>(() => _contentService.GetContent(id));
            _mockContentRepository.Received(1).GetByID(id);
        }

        [Fact]
        public void UpdateContent_WithExistingContent_ShouldUpdateAndSave()
        {
            // Arrange
            var contentDto = _fixture.Create<ContentItemDto>();
            var existingContent = _fixture.Create<ContentItem>();

            _mockContentRepository.GetByID(contentDto.ContentItemId).Returns(existingContent);

            // Act
            _contentService.UpdateContent(contentDto);

            // Assert
            _mockMapper.Received(1).Map<ContentItemDto, ContentItem>(contentDto, existingContent);
            _mockContentRepository.Received(1).Update(existingContent);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void UpdateContent_WithNonExistingContent_ShouldThrowContentNotFoundException()
        {
            // Arrange
            var contentDto = _fixture.Create<ContentItemDto>();

            _mockContentRepository.GetByID(contentDto.ContentItemId).Returns((ContentItem)null!);

            // Act & Assert
            Assert.Throws<ContentNotFoundException>(() => _contentService.UpdateContent(contentDto));

            _mockContentRepository.Received(1).GetByID(contentDto.ContentItemId);
            _mockContentRepository.DidNotReceive().Update(Arg.Any<ContentItem>());
            _mockUnitOfWork.DidNotReceive().Save();
        }

        [Fact]
        public void DeleteContent_ShouldDeleteContentAndSave()
        {
            // Arrange
            var contentEntity = _fixture.Create<ContentItem>();

            _mockContentRepository.GetByID(contentEntity.ContentItemId).Returns(contentEntity);

            // Act
            _contentService.DeleteContent(contentEntity.ContentItemId);

            // Assert
            _mockContentRepository.Received(1).Delete(contentEntity.ContentItemId);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void DeleteContent_WithNonExistingContent_ShouldThrowContentNotFoundException()
        {
            // Arrange
            var id = 999;
            _mockContentRepository.GetByID(id).Returns((ContentItem)null!);

            // Act & Assert
            Assert.Throws<ContentNotFoundException>(() => _contentService.DeleteContent(id));

            _mockContentRepository.Received(1).GetByID(id);
            _mockContentRepository.DidNotReceive().Delete(id);
            _mockUnitOfWork.DidNotReceive().Save();
        }
    }
}