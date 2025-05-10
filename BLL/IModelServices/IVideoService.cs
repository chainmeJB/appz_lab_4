using BLL.DTO;
using System.Collections.Generic;

namespace BLL.IModelServices
{
    public interface IVideoService
    {
        void AddVideo(VideoDto video);
        IEnumerable<VideoDto> GetAllVideos();
        VideoDto GetVideoByID(int id);
    }
}
