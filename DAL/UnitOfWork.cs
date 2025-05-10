using DAL.DataModels;
using System;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LibraryContext _context;
        private IRepository<Storage> _storageRepository;
        private IRepository<ContentItem> _contentRepository;

        public UnitOfWork()
        {
            _context = new LibraryContext();
        }

        public IRepository<Storage> StorageRepository
        {
            get
            {
                return _storageRepository = new GenericRepository<Storage>(_context);
            }
        }

        public IRepository<ContentItem> ContentRepository
        {
            get
            {
                return _contentRepository = new GenericRepository<ContentItem>(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
