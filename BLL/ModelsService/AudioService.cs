using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using AutoMapper;
using DAL;
using BLL.Exceptions;
using BLL.IModelServices;

namespace BLL.ModelsService
{
    public class AudioService : IAudioService
    {
        private readonly IContentService _contentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AudioService(IUnitOfWork unitOfWork, IContentService contentService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contentService = contentService;
            _mapper = mapper;
        }

        public void AddAudio(AudioDto audio)
        {
            _contentService.AddContent(audio);
        }

        public IEnumerable<AudioDto> GetAllAudios()
        {
            var audios = _unitOfWork.ContentRepository.Get().OfType<Audio>().ToList();
            return _mapper.Map<IEnumerable<AudioDto>>(audios);
        }

        public AudioDto GetAudioByID(int id)
        {
            var content = _unitOfWork.ContentRepository.GetByID(id) as Audio;
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            return _mapper.Map<AudioDto>(content);
        }
    }
}
