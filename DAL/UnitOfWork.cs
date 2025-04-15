using DAL.DataModels;
using System;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private LibraryContext context = new LibraryContext();
        private GenericRepository<ContentItem> contentRepository;
        private GenericRepository<Storage> storageRepository;

        public GenericRepository<ContentItem> ContentRepository
        {
            get
            {

                if (this.contentRepository == null)
                {
                    this.contentRepository = new GenericRepository<ContentItem>(context);
                }
                return contentRepository;
            }
        }

        public GenericRepository<Storage> StorageRepository
        {
            get
            {

                if (this.storageRepository == null)
                {
                    this.storageRepository = new GenericRepository<Storage>(context);
                }
                return storageRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
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
