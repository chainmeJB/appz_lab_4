using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BLL.ModelsService
{
    public class AudioService : ContentService
    {
        public void AddAudio(string title, string format, int storageId, int bitRate, int channels)
        {
            Add(new Audio
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                BitRate = bitRate,
                Channels = channels
            });
        }

        public IEnumerable<Audio> GetAllAudios()
        {
            return contentRepository.GetAll().OfType<Audio>().ToList();
        }

        public Audio GetAudioById(int id)
        {
            var content = contentRepository.GetById(id);
            var audios = GetAllAudios();

            foreach (var audio in audios)
            {
                if (audio.ContentItemId == content.ContentItemId)
                {
                    return audio;
                }
            }
            return null;
        }
    }
}
