using BLL.ModelsService;
using BLL.DTO;
using AutoMapper;
using DAL.DataModels;
using DAL;
using BLL.Exceptions;
using NSubstitute;
using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace BLL.Tests
{
    public class StorageServiceTests : BaseServiceTests
    {
        private readonly StorageService _storageService;

        public StorageServiceTests()
        {
            _storageService = new StorageService(_mockUnitOfWork, _mockMapper);
        }

        [Fact]
        public void AddStorage_ValidDto_ShouldInsertAndSave()
        {
            // Arrange
            var dto = _fixture.Create<StorageDto>();
            var entity = _fixture.Create<Storage>();

            _mockMapper.Map<Storage>(dto).Returns(entity);

            // Act
            _storageService.AddStorage(dto);

            // Assert
            _mockStorageRepository.Received(1).Insert(entity);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void GetByID_ExistingId_ShouldReturnMappedDto()
        {
            // Arrange
            var entity = _fixture.Create<Storage>();
            var expectedDto = _fixture.Create<StorageDto>();

            _mockStorageRepository.GetByID(entity.StorageId).Returns(entity);
            _mockMapper.Map<StorageDto>(entity).Returns(expectedDto);

            // Act
            var result = _storageService.GetStorage(entity.StorageId);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockStorageRepository.Received(1).GetByID(entity.StorageId);
        }

        [Fact]
        public void GetByID_NonExistingId_ShouldThrowException()
        {
            // Arrange
            var id = 999;
            _mockStorageRepository.GetByID(id).Returns((Storage)null!);

            // Act & Assert
            Assert.Throws<StorageNotFoundException>(() => _storageService.GetStorage(id));
            _mockStorageRepository.Received(1).GetByID(id);
        }

        [Fact]
        public void GetAllStorages_RepositoryNotEmpty_ShouldReturnMappedDtos()
        {
            // Arrange
            var entities = _fixture.CreateMany<Storage>(2);
            var dtos = _fixture.CreateMany<StorageDto>(2);

            _mockStorageRepository.Get().Returns(entities);
            _mockMapper.Map<IEnumerable<StorageDto>>(Arg.Any<IEnumerable<Storage>>()).Returns(dtos);

            // Act
            var result = _storageService.GetAllStorages();

            // Assert
            Assert.Equal(dtos, result);
            _mockStorageRepository.Received(1).Get();
        }

        [Fact]
        public void GetAllStorages_EmptyRepository_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyEntities = new List<Storage>();
            var emptyDtos = new List<StorageDto>();

            _mockStorageRepository.Get().Returns(emptyEntities);
            _mockMapper.Map<IEnumerable<StorageDto>>(emptyEntities).Returns(emptyDtos);

            // Act
            var result = _storageService.GetAllStorages();

            // Assert
            Assert.Empty(result);
            _mockStorageRepository.Received(1).Get();
        }

        [Fact]
        public void UpdateStorage_ExistingStorage_ShouldMapUpdateAndSave()
        {
            // Arrange
            var dto = _fixture.Create<StorageDto>();
            var entity = _fixture.Create<Storage>();

            _mockStorageRepository.GetByID(dto.StorageId).Returns(entity);

            // Act
            _storageService.UpdateStorage(dto);

            // Assert
            _mockMapper.Received(1).Map<StorageDto, Storage>(dto, entity);
            _mockStorageRepository.Received(1).Update(entity);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void UpdateStorage_NonExistingStorage_ShouldThrowException()
        {
            // Arrange
            var dto = _fixture.Create<StorageDto>();

            _mockStorageRepository.GetByID(dto.StorageId).Returns((Storage)null!);

            // Act & Assert
            Assert.Throws<StorageNotFoundException>(() => _storageService.UpdateStorage(dto));
            _mockStorageRepository.Received(1).GetByID(dto.StorageId);
            _mockStorageRepository.DidNotReceive().Update(Arg.Any<Storage>());
            _mockUnitOfWork.DidNotReceive().Save();
        }

        [Fact]
        public void DeleteStorage_ExistingStorage_ShouldDeleteAndSave()
        {
            // Arrange
            var entity = _fixture.Create<Storage>();

            _mockStorageRepository.GetByID(entity.StorageId).Returns(entity);

            // Act
            _storageService.DeleteStorage(entity.StorageId);

            // Assert
            _mockStorageRepository.Received(1).Delete(entity.StorageId);
            _mockUnitOfWork.Received(1).Save();
        }

        [Fact]
        public void DeleteStorage_NonExistingStorage_ShouldThrowException()
        {
            // Arrange
            var id = 999;
            _mockStorageRepository.GetByID(id).Returns((Storage)null!);

            // Act & Assert
            Assert.Throws<StorageNotFoundException>(() => _storageService.DeleteStorage(id));

            _mockStorageRepository.Received(1).GetByID(id);
            _mockStorageRepository.DidNotReceive().Delete(id);
            _mockUnitOfWork.DidNotReceive().Save();
        }
    }
}