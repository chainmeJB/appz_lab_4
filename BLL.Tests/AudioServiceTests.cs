using BLL.DTO;
using BLL.Exceptions;
using BLL.ModelsService;
using DAL.DataModels;
using AutoFixture;
using NSubstitute;
using BLL.IModelServices;

namespace BLL.Tests
{
    public class AudioServiceTests : BaseServiceTests
    {
        private readonly IAudioService _audioService;
        private readonly IContentService _contentService;

        public AudioServiceTests() : base()
        {
            _contentService = new ContentService(_mockUnitOfWork, _mockMapper);
            _audioService = new AudioService(_mockUnitOfWork, _contentService, _mockMapper);
        }

        [Fact]
        public void AddAudio_ShouldInsertAudioAndSave()
        {
            // Arrange
            var audioDto = _fixture.Create<AudioDto>();
            var audioEntity = _fixture.Create<Audio>();

            _mockMapper.Map<ContentItem>(audioDto).Returns(audioEntity);

            // Act
            _audioService.AddAudio(audioDto);

            // Assert
            _mockContentRepository.Received(1).Insert(audioEntity);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void GetAllAudios_ShouldReturnAllAudioDtos()
        {
            // Arrange
            var audioEntities = _fixture.CreateMany<Audio>(2).ToList();
            var expectedDtos = _fixture.CreateMany<AudioDto>(2).ToList();

            _mockContentRepository.Get().Returns(audioEntities);
            _mockMapper.Map<IEnumerable<AudioDto>>(Arg.Any<IEnumerable<Audio>>()).Returns(expectedDtos);

            // Act
            var result = _audioService.GetAllAudios();

            // Assert
            Assert.Equal(expectedDtos, result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetAllAudios_WithEmptyRepository_ShouldReturnEmptyCollection()
        {
            // Arrange
            var emptyList = new List<Audio>();
            var emptyDtoList = new List<AudioDto>();

            _mockContentRepository.Get().Returns(emptyList.AsQueryable());
            _mockMapper.Map<IEnumerable<AudioDto>>(emptyList).Returns(emptyDtoList);

            // Act
            var result = _audioService.GetAllAudios();

            // Assert
            Assert.Empty(result);
            _mockContentRepository.Received(1).Get();
        }

        [Fact]
        public void GetAudioByID_WithExistingId_ShouldReturnAudioDto()
        {
            // Arrange
            var audioEntity = _fixture.Create<Audio>();
            var expectedDto = _fixture.Create<AudioDto>();

            _mockContentRepository.GetByID(audioEntity.ContentItemId).Returns(audioEntity);
            _mockMapper.Map<AudioDto>(audioEntity).Returns(expectedDto);

            // Act
            var result = _audioService.GetAudioByID(audioEntity.ContentItemId);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockContentRepository.Received(1).GetByID(audioEntity.ContentItemId);
        }

        [Fact]
        public void GetAudioByID_WithNonExistingId_ShouldThrowContentNotFoundException()
        {
            // Arrange
            var id = 999;
            _mockContentRepository.GetByID(id).Returns((Audio)null!);

            // Act & Assert
            Assert.Throws<ContentNotFoundException>(() => _audioService.GetAudioByID(id));
            _mockContentRepository.Received(1).GetByID(id);
        }
    }
}