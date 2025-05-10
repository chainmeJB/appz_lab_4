using System;
using System.Text;
using BLL;
using BLL.ModelsService;
using DAL;
using Autofac;
using AutoMapper;
using BLL.DTO;
using BLL.IModelServices;

namespace appz_lab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var builder = new ContainerBuilder();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LibraryProfile>();
            });

            builder.RegisterInstance(config.CreateMapper()).As<IMapper>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<StorageService>().As<IStorageService>();
            builder.RegisterType<ContentService>().As<IContentService>();
            builder.RegisterType<DocumentService>().As<IDocumentService>();
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<VideoService>().As<IVideoService>();
            builder.RegisterType<AudioService>().As<IAudioService>();
            builder.RegisterType<UI>().AsSelf();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var ui = scope.Resolve<UI>();
                ui.ShowUi();
            }
        }
    }
}