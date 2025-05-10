using BLL.DTO;
using System.Collections.Generic;

namespace BLL.IModelServices
{
    public interface IAudioService
    {
        void AddAudio(AudioDto audio);
        IEnumerable<AudioDto> GetAllAudios();
        AudioDto GetAudioByID(int id);
    }
}
