using AutoFixture.AutoNSubstitute;
using AutoFixture;
using AutoMapper;
using DAL.DataModels;
using DAL;
using NSubstitute;

namespace BLL.Tests
{
    public abstract class BaseServiceTests
    {
        public IUnitOfWork _mockUnitOfWork { get; }
        public IRepository<ContentItem> _mockContentRepository { get; }
        public IRepository<Storage> _mockStorageRepository { get; }
        public IMapper _mockMapper { get; }
        public IFixture _fixture { get; }

        protected BaseServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _mockContentRepository = Substitute.For<IRepository<ContentItem>>();
            _mockStorageRepository = Substitute.For<IRepository<Storage>>();
            _mockUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockMapper = Substitute.For<IMapper>();

            _mockUnitOfWork.ContentRepository.Returns(_mockContentRepository);
            _mockUnitOfWork.StorageRepository.Returns(_mockStorageRepository);
        }
    }
}
