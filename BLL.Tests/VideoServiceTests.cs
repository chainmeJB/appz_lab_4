using BLL.DTO;
using BLL.Exceptions;
using DAL.DataModels;
using AutoFixture;
using NSubstitute;
using BLL.IModelServices;
using BLL.ModelsService;

namespace BLL.Tests
{
    public class VideoServiceTests : BaseServiceTests
    {
        private readonly IVideoService _videoService;
        private readonly IContentService _contentService;

        public VideoServiceTests() : base()
        {
            _contentService = new ContentService(_mockUnitOfWork, _mockMapper);
            _videoService = new VideoService(_mockUnitOfWork, _contentService, _mockMapper);
        }

        [Fact]
        public void AddVideo_ShouldInsertVideoAndSave()
        {
            // Arrange
            var videoDto = _fixture.Create<VideoDto>();
            var videoEntity = _fixture.Create<Video>();

            _mockMapper.Map<ContentItem>(videoDto).Returns(videoEntity);

            // Act
            _videoService.AddVideo(videoDto);

            // Assert
            _mockContentRepository.Received(1).Insert(videoEntity);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void GetAllVideos_ShouldReturnAllVideoDtos()
        {
            // Arrange
            var videoEntities = _fixture.CreateMany<Video>(2).ToList();
            var expectedDtos = _fixture.CreateMany<VideoDto>(2).ToList();

            _mockContentRepository.Get().Returns(videoEntities);
            _mockMapper.Map<IEnumerable<VideoDto>>(Arg.Any<IEnumerable<Video>>()).Returns(expectedDtos);

            // Act
            var result = _videoService.GetAllVideos();

            // Assert
            Assert.Equal(expectedDtos, result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetAllVideos_WithEmptyRepository_ShouldReturnEmptyCollection()
        {
            // Arrange
            var emptyList = new List<Video>();
            var emptyDtoList = new List<VideoDto>();

            _mockContentRepository.Get().Returns(emptyList.AsQueryable());
            _mockMapper.Map<IEnumerable<VideoDto>>(emptyList).Returns(emptyDtoList);

            // Act
            var result = _videoService.GetAllVideos();

            // Assert
            Assert.Empty(result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetVideoByID_WithExistingId_ShouldReturnVideoDto()
        {
            // Arrange
            var videoEntity = _fixture.Create<Video>();
            var expectedDto = _fixture.Create<VideoDto>();

            _mockContentRepository.GetByID(videoEntity.ContentItemId).Returns(videoEntity);
            _mockMapper.Map<VideoDto>(videoEntity).Returns(expectedDto);

            // Act
            var result = _videoService.GetVideoByID(videoEntity.ContentItemId);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockContentRepository.Received(1).GetByID(videoEntity.ContentItemId);
        }

        [Fact]
        public void GetVideoByID_WithNonExistingId_ShouldThrowContentNotFoundException()
        {
            // Arrange
            var id = 999;
            _mockContentRepository.GetByID(id).Returns((Video)null!);

            // Act & Assert
            Assert.Throws<ContentNotFoundException>(() => _videoService.GetVideoByID(id));
            _mockContentRepository.Received(1).GetByID(id);
        }
    }
}