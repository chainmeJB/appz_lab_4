using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class StorageRepository : IRepository<Storage>
    {
        private readonly LibraryContext context;

        public StorageRepository(LibraryContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var storage = context.Storages.Find(id);
            context.Storages.Remove(storage);
        }

        public IEnumerable<Storage> GetAll()
        {
            return context.Storages
                .ToList();
        }

        public Storage GetById(int id)
        {
            return context.Storages.Find(id);
        }

        public void Insert(Storage storage)
        {
            context.Storages.Add(storage);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Storage storage)
        {
            context.Entry(storage).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
