using BLL.ModelsService;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class VideoService : ContentService
    {
        public void AddVideo(string title, string format, int storageId, double duration, string resolution)
        {
            Add(new Video
            {
                Title = title,
                Format = format,
                StorageId = storageId,
                Duration = duration,
                Resolution = resolution
            });
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return unitOfWork.ContentRepository.Get().OfType<Video>().ToList();
        }

        public Video GetVideoByID(int id)
        {
            var content = GetByID(id);
            var videos = GetAllVideos();

            foreach (var video in videos)
            {
                if (video.ContentItemId == content.ContentItemId)
                {
                    return video;
                }
            }
            return null;
        }
    }
}
