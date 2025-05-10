using BLL.DTO;
using System.Collections.Generic;

namespace BLL.IModelServices
{
    public interface IStorageService
    {
        void AddStorage(StorageDto storage);
        StorageDto GetStorage(int id);
        IEnumerable<StorageDto> GetAllStorages();
        void UpdateStorage(StorageDto storage);
        void DeleteStorage(int id);
    }
}
