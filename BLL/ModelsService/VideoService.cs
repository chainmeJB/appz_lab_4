using AutoMapper;
using BLL.DTO;
using BLL.ModelsService;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;
using DAL;
using BLL.Exceptions;
using BLL.IModelServices;

namespace BLL
{
    public class VideoService : IVideoService
    {
        private readonly IContentService _contentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VideoService(IUnitOfWork unitOfWork, IContentService contentService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contentService = contentService;
            _mapper = mapper;
        }
        public void AddVideo(VideoDto video)
        {
            _contentService.AddContent(video);
        }

        public IEnumerable<VideoDto> GetAllVideos()
        {
            var videos = _unitOfWork.ContentRepository.Get().OfType<Video>().ToList();
            return _mapper.Map<IEnumerable<VideoDto>>(videos);
        }

        public VideoDto GetVideoByID(int id)
        {
            var content = _unitOfWork.ContentRepository.GetByID(id) as Video;
            if (content == null)
            {
                throw new ContentNotFoundException();
            }
            return _mapper.Map<VideoDto>(content);
        }
    }
}
