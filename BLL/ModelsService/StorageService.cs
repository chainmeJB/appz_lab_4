using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.IModelServices;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class StorageService : IStorageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StorageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddStorage(StorageDto storage)
        {
            _unitOfWork.StorageRepository.Insert(_mapper.Map<Storage>(storage));
            _unitOfWork.Save();
        }

        public StorageDto GetStorage(int id)
        {
            var storage = _unitOfWork.StorageRepository.GetByID(id);
            if (storage == null)
            {
                throw new StorageNotFoundException();
            }
            return _mapper.Map<StorageDto>(storage);
        }

        public IEnumerable<StorageDto> GetAllStorages()
        {
            var storages = _unitOfWork.StorageRepository.Get().ToList();
            return _mapper.Map<IEnumerable<StorageDto>>(storages);
        }

        public void UpdateStorage(StorageDto storage)
        {
            var existing = _unitOfWork.StorageRepository.GetByID(storage.StorageId);
            if (existing == null)
            {
                throw new StorageNotFoundException();
            }
            _mapper.Map(storage, existing);

            _unitOfWork.StorageRepository.Update(existing);
            _unitOfWork.Save();
        }

        public void DeleteStorage(int id)
        {
            var existing = _unitOfWork.StorageRepository.GetByID(id);
            if (existing == null)
            {
                throw new StorageNotFoundException();
            }
            _unitOfWork.StorageRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
