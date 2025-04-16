using AutoMapper;
using DAL.DataModels;

namespace BLL.DTO
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Storage, StorageDto>().ReverseMap();

            CreateMap<ContentItem, ContentItemDto>()
                .Include<Book, BookDto>()
                .Include<Audio, AudioDto>()
                .Include<Document, DocumentDto>()
                .Include<Video, VideoDto>();

            CreateMap<ContentItemDto, ContentItem>()
                .Include<BookDto, Book>()
                .Include<AudioDto, Audio>()
                .Include<DocumentDto, Document>()
                .Include<VideoDto, Video>();

            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Audio, AudioDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<Video, VideoDto>().ReverseMap();
        }
    }
}
