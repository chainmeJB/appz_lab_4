using DAL;
using DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelsService
{
    public class AudioService : ContentService
    {
        public void AddAudio(string title, string format, int storageId, int bitRate, int channels)
        {
            var audio = new Audio()
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                BitRate = bitRate,
                Channels = channels
            };

            _context.Contents.Add(audio);
            _context.SaveChanges();
        }

        public Audio GetAudioByTitle(string title)
        {
            return _context.Set<Audio>().FirstOrDefault(a => a.Title == title);
        }

        public List<Audio> GetAllAudios()
        {
            return _context.Set<Audio>().ToList();
        }
    }
}
