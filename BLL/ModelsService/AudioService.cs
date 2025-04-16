using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using AutoMapper;

namespace BLL.ModelsService
{
    public class AudioService : ContentService
    {
        public AudioService(IMapper mapper) : base(mapper) { }
        public void AddAudio(AudioDto audio)
        {
            Add(audio);
        }

        public IEnumerable<AudioDto> GetAllAudios()
        {
            var audios = unitOfWork.ContentRepository.Get().OfType<Audio>().ToList();
            return _mapper.Map<IEnumerable<AudioDto>>(audios);
        }

        public AudioDto GetAudioByID(int id)
        {
            var contentDto = GetByID(id);
            return GetAllAudios().FirstOrDefault(audio => audio.ContentItemId == contentDto.ContentItemId);
        }
    }
}
