using System.Collections.Generic;
using BLL.Exceptions;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class StorageService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public void AddStorage(string name, string address)
        {
            var storage = new Storage()
            {
                Name = name,
                Address = address
            };
            unitOfWork.StorageRepository.Insert(storage);
            unitOfWork.Save();
        }

        public Storage GetByID(int id)
        {
            var storage = unitOfWork.StorageRepository.GetByID(id);
            if (storage == null)
            {
                throw new StorageNotFoundException();
            }
            return storage;
        }

        public IEnumerable<Storage> GetAllStorages()
        {
            return unitOfWork.StorageRepository.Get();
        }

        public void UpdateStorage(Storage storage)
        {
            unitOfWork.StorageRepository.Update(storage);
            unitOfWork.Save();
        }

        public void DeleteStorage(int id)
        {
            unitOfWork.StorageRepository.Delete(id);
            unitOfWork.Save();
        }
    }
}
