using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class StorageService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        private readonly IMapper _mapper;

        public StorageService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddStorage(StorageDto storage)
        {
            unitOfWork.StorageRepository.Insert(_mapper.Map<Storage>(storage));
            unitOfWork.Save();
        }

        public StorageDto GetByID(int id)
        {
            var storage = unitOfWork.StorageRepository.GetByID(id);
            if (storage == null)
            {
                throw new StorageNotFoundException();
            }
            return _mapper.Map<StorageDto>(storage);
        }

        public IEnumerable<StorageDto> GetAllStorages()
        {
            var storages = unitOfWork.StorageRepository.Get().ToList();
            return _mapper.Map<IEnumerable<StorageDto>>(storages);
        }

        public void UpdateStorage(StorageDto storageDto)
        {
            var existing = unitOfWork.StorageRepository.GetByID(storageDto.StorageId);
            if (existing == null)
            {
                throw new StorageNotFoundException();
            }
            _mapper.Map(storageDto, existing);

            unitOfWork.StorageRepository.Update(existing);
            unitOfWork.Save();
        }

        public void DeleteStorage(int id)
        {
            unitOfWork.StorageRepository.Delete(id);
            unitOfWork.Save();
        }
    }
}
