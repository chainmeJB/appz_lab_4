using System;
using System.Linq;
using BLL.DTO;
using BLL.Exceptions;
using BLL.IModelServices;

namespace appz_lab_4
{
    internal class UI
    {
        private readonly IStorageService storageService;
        private readonly IContentService contentService;
        private readonly IBookService bookService;
        private readonly IVideoService videoService;
        private readonly IAudioService audioService;
        private readonly IDocumentService documentService;

        public UI(
            IContentService contentService,
            IStorageService storageService,
            IBookService bookService,
            IAudioService audioService,
            IDocumentService documentService,
            IVideoService videoService)
        {
            this.contentService = contentService;
            this.storageService = storageService;
            this.bookService = bookService;
            this.audioService = audioService;
            this.documentService = documentService;
            this.videoService = videoService;
        }

        public void ShowUi()
        {
            while (true)
            {
                Console.WriteLine("--Меню--");
                Console.WriteLine("1. Робота з контентом");
                Console.WriteLine("2. Робота з сховищами");
                Console.WriteLine("3. Вийти");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("1. Додати контент");
                        Console.WriteLine("2. Отримати весь контент");
                        Console.WriteLine("3. Отримати контент за айді");
                        Console.WriteLine("4. Змінити сховище контенту");
                        Console.WriteLine("5. Видалити контент");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                AddContent(AskContentType());
                                break;
                            case "2":
                                GetAllContents();
                                break;
                            case "3":
                                GetContent(AskContentType());
                                break;
                            case "4":
                                UpdateContent();
                                break;
                            case "5":
                                DeleteContent();
                                break;
                            default:
                                Console.WriteLine("Некоректний ввод");
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("1. Додати сховище");
                        Console.WriteLine("2. Отримати всі сховища");
                        Console.WriteLine("3. Отримати сховище за айді");
                        Console.WriteLine("4. Оновити дані сховища");
                        Console.WriteLine("5. Видалити сховище");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                AddStorage();
                                break;
                            case "2":
                                GetAllStorages();
                                break;
                            case "3":
                                GetStorageById();
                                break;
                            case "4":
                                UpdateStorageData();
                                break;
                            case "5":
                                DeleteStorage();
                                break;
                            default:
                                Console.WriteLine("Некоректний ввод");
                                break;
                        }
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Некоректний ввод");
                        break;
                }
            }
        }

        private enum ContentType
        {
            Book,
            Audio,
            Document,
            Video
        }

        private ContentType AskContentType()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Виберіть тип контенту:");
                    Console.WriteLine("1. Книга");
                    Console.WriteLine("2. Відео");
                    Console.WriteLine("3. Аудіо");
                    Console.WriteLine("4. Документ");

                    switch (Console.ReadLine())
                    {
                        case "1": return ContentType.Book;
                        case "2": return ContentType.Video;
                        case "3": return ContentType.Audio;
                        case "4": return ContentType.Document;
                        default: throw new ArgumentException("Невірний тип контенту");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);

                }
            }
        }

        private void AddContent(ContentType type)
        {
            try
            {
                Console.WriteLine("Введіть назву: ");
                string title = Console.ReadLine();

                Console.WriteLine("Введіть формат: ");
                string format = Console.ReadLine();

                Console.WriteLine("Введіть айді сховища: ");
                int storageId = GetIntType(Console.ReadLine());

                var storage = storageService.GetStorage(storageId);

                if (storage == null)
                {
                    throw new StorageNotFoundException();
                }

                switch (type)
                {
                    case ContentType.Book:
                        Console.WriteLine("Введіть ім'я автора: ");
                        string author = Console.ReadLine();

                        Console.WriteLine("Введіть кількість сторінок: ");
                        int pageCount = GetIntType(Console.ReadLine());

                        bookService.AddBook(new BookDto
                        {
                            Title = title,
                            Format = format,
                            StorageId = storageId,
                            Author = author,
                            PageCount = pageCount
                        });

                        Console.WriteLine("Книгу успішно додано");
                        break;

                    case ContentType.Video:
                        Console.WriteLine("Введіть довжину відео (в секундах): ");
                        int duration = GetIntType(Console.ReadLine());

                        Console.WriteLine("Введіть розширення відео: ");
                        string resolution = Console.ReadLine();

                        videoService.AddVideo(new VideoDto
                        { 
                            Title = title, 
                            Format = format, 
                            StorageId = storageId, 
                            Duration = duration, 
                            Resolution = resolution 
                        });

                        Console.WriteLine("Відео успішно додано");
                        break;

                    case ContentType.Audio:
                        Console.WriteLine("Введіть бітрейт аудіо: ");
                        int bitRate = GetIntType(Console.ReadLine());

                        Console.WriteLine("Введіть кількість каналів аудіо: ");
                        int channels = GetIntType(Console.ReadLine());

                        audioService.AddAudio(new AudioDto
                        { 
                            Title = title, 
                            Format = format, 
                            StorageId = storageId, 
                            BitRate = bitRate, 
                            Channels = channels 
                        });

                        Console.WriteLine("Аудіо успішно додано");
                        break;

                    case ContentType.Document:
                        DateTime creationDate = DateTime.Now;

                        Console.WriteLine("Введіть шлях до файлу: ");
                        string filePath = Console.ReadLine();

                        documentService.AddDocument(new DocumentDto
                        { 
                            Title = title, 
                            Format = format, 
                            StorageId = storageId, 
                            CreationDate = creationDate, 
                            FilePath = filePath 
                        });

                        Console.WriteLine("Документ успішно додано");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        private void GetAllContents()
        {
            var books = bookService.GetAllBooks();
            if (books.Any())
            {
                Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Автор", "Стор."));
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
                Console.WriteLine();
            }

            var videos = videoService.GetAllVideos();
            if (videos.Any())
            {
                Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                "Айді", "Назва", "Формат", "Сховище", "Адреса", "Довжина", "Розширення"));
                foreach (var video in videos)
                {
                    Console.WriteLine(video);
                }
                Console.WriteLine();
            }

            var audios = audioService.GetAllAudios();
            if (audios.Any())
            {
                Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Бітрейт", "Канали"));
                foreach (var audio in audios)
                {
                    Console.WriteLine(audio);
                }
                Console.WriteLine();
            }

            var documents = documentService.GetAllDocuments();
            if (documents.Any())
            {
                Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Дата створення", "Шлях до файлу"));
                foreach (var document in documents)
                {
                    Console.WriteLine(document);
                }
                Console.WriteLine();
            }
        }

        private void GetContent(ContentType type)
        {
            try
            {
                Console.WriteLine("Введіть айді контента: ");
                int id = GetIntType(Console.ReadLine());

                switch (type)
                {
                    case ContentType.Book:
                        var book = bookService.GetBookByID(id);
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                            "Айді", "Назва", "Формат", "Сховище", "Адреса", "Автор", "Стор."));
                        Console.WriteLine(book);
                        break;

                    case ContentType.Video:
                        var video = videoService.GetVideoByID(id);
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Довжина", "Розширення"));
                        Console.WriteLine(video);
                        break;

                    case ContentType.Audio:
                        var audio = audioService.GetAudioByID(id);
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Бітрейт", "Канали"));
                        Console.WriteLine(audio);
                        break;

                    case ContentType.Document:
                        var document = documentService.GetDocumentByID(id);
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Дата створення", "Шлях до файлу"));
                        Console.WriteLine(document);
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        private void UpdateContent()
        {
            try
            {
                Console.WriteLine("Введіть айді контента: ");
                int contentId = GetIntType(Console.ReadLine());

                var content = contentService.GetContent(contentId);
                if (content == null)
                {
                    throw new ContentNotFoundException();
                }

                Console.WriteLine("Введіть айді нового сховища: ");
                int storageId = GetIntType(Console.ReadLine());

                var storage = contentService.GetContent(storageId);
                if (storage == null)
                {
                    throw new StorageNotFoundException();
                }

                content.StorageId = storageId;
                contentService.UpdateContent(content);
                Console.WriteLine("Сховище контента успішно оновлено");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        private void DeleteContent()
        {
            try
            {
                Console.WriteLine("Введіть айді контента: ");
                int id = GetIntType(Console.ReadLine());

                var content = contentService.GetContent(id);
                if (content == null)
                {
                    throw new ContentNotFoundException();
                }

                contentService.DeleteContent(id);

                Console.WriteLine("Контент успішно видалено");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        private void AddStorage()
        {
            Console.WriteLine("Введіть назву: ");
            string name = Console.ReadLine();

            Console.WriteLine("Введіть адресу: ");
            string address = Console.ReadLine();

            storageService.AddStorage(new StorageDto
            {
                Name = name,
                Address = address
            });

            Console.WriteLine("Сховище успішно додано");
        }

        private void GetAllStorages()
        {
            var storages = storageService.GetAllStorages();

            Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15}", "Айді", "Назва", "Адресса"));
            foreach (var storage in storages)
            {
                Console.WriteLine(storage);
            }
        }

        private void GetStorageById()
        {
            try
            {
                Console.WriteLine("Введіть айді сховища: ");
                int id = GetIntType(Console.ReadLine());

                var storage = storageService.GetStorage(id);

                if (storage == null)
                {
                    throw new StorageNotFoundException();
                }

                Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15}", "Айді", "Назва", "Адресса"));
                Console.WriteLine(storage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        private void UpdateStorageData()
        {
            try
            {
                Console.WriteLine("Введіть айді сховища: ");
                int id = GetIntType(Console.ReadLine());

                var storage = storageService.GetStorage(id);

                Console.WriteLine("Введіть нову назву сховища: ");
                storage.Name = Console.ReadLine();

                Console.WriteLine("Введіть нову адрессу сховища: ");
                storage.Address = Console.ReadLine();

                storageService.UpdateStorage(storage);
                Console.WriteLine("Сховище успішно оновлено");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        private void DeleteStorage()
        {
            try
            {
                Console.WriteLine("Введіть айді сховища: ");
                int id = GetIntType(Console.ReadLine());

                var storage = storageService.GetStorage(id);
                if (storage == null)
                {
                    throw new StorageNotFoundException();
                }

                storageService.DeleteStorage(id);

                Console.WriteLine("Сховище успішно видалено");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        private int GetIntType(string input)
        {
            if (!int.TryParse(input, out int id) || id <= 0)
            {
                throw new ArgumentException("Некоректний ввод. Введіть число більше за 0.");
            }
            return id;
        }
    }
}
