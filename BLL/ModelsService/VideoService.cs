using BLL.ModelsService;
using DAL;
using DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VideoService : ContentService
    {
        public void AddVideo(string title, string format, int storageId, double duration, string resolution)
        {
            var video = new Video()
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                Duration = duration,
                Resolution = resolution
            };

            _context.Contents.Add(video);
            _context.SaveChanges();
        }

        public Video GetVideoByTitle(string title)
        {
            return _context.Set<Video>().FirstOrDefault(b => b.Title == title);
        }

        public List<Video> GetAllVideos()
        {
            return _context.Set<Video>().ToList();
        }
    }
}
