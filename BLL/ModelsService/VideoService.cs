using AutoMapper;
using BLL.DTO;
using BLL.ModelsService;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class VideoService : ContentService
    {
        public VideoService(IMapper mapper) : base(mapper) { }
        public void AddVideo(VideoDto video)
        {
            Add(video);
        }

        public IEnumerable<VideoDto> GetAllVideos()
        {
            var videos = unitOfWork.ContentRepository.Get().OfType<Video>().ToList();
            return _mapper.Map<IEnumerable<VideoDto>>(videos);
        }

        public VideoDto GetVideoByID(int id)
        {
            var contentDto = GetByID(id);
            return GetAllVideos().FirstOrDefault(vid => vid.ContentItemId == contentDto.ContentItemId);
        }
    }
}
