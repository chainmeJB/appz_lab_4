using System.Collections.Generic;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class StorageService
    {
        private readonly IRepository<Storage> storageRepository;
        public StorageService() 
        {
            this.storageRepository = new StorageRepository(new LibraryContext());
        }

        public void AddStorage(string name, string address)
        {
            var storage = new Storage()
            {
                Name = name,
                Address = address
            };
            storageRepository.Insert(storage);
            storageRepository.Save();
        }

        public Storage GetById(int id)
        {
            return storageRepository.GetById(id);
        }

        public IEnumerable<Storage> GetAllStorages()
        {
            return storageRepository.GetAll();
        }

        public void UpdateStorage(Storage storage)
        {
            storageRepository.Update(storage);
            storageRepository.Save();
        }

        public void DeleteStorage(int id)
        {
            storageRepository.Delete(id);
            storageRepository.Save();
        }
    }
}
