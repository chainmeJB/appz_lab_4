using System;
using BLL;
using BLL.ModelsService;

namespace appz_lab_4
{
    internal class UI
    {
        StorageService storageService;
        ContentService contentService;
        BookService bookService;
        VideoService videoService;
        AudioService audioService;
        DocumentService documentService;

        public UI()
        {
            this.storageService = new StorageService();
            this.bookService = new BookService();
            this.videoService = new VideoService();
            this.contentService = new ContentService();
            this.documentService = new DocumentService();
            this.audioService = new AudioService();
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
                        Console.WriteLine("1. Додати до бібліотеки");
                        Console.WriteLine("2. Отримати все");
                        Console.WriteLine("3. Отримати за назвою");
                        Console.WriteLine("4. Оновити сховище");
                        Console.WriteLine("5. Видалити контент");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                ShowContentOptions();

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        AddContent(ContentType.Book);
                                        break;
                                    case "2":
                                        AddContent(ContentType.Video);
                                        break;
                                    case "3":
                                        AddContent(ContentType.Audio);
                                        break;
                                    case "4":
                                        AddContent(ContentType.Document);
                                        break;
                                    default:
                                        Console.WriteLine("Некоректний ввод");
                                        break;
                                }
                                break;
                            case "2":
                                ShowContentOptions();
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        GetAllContents(ContentType.Book);
                                        break;
                                    case "2":
                                        GetAllContents(ContentType.Video);
                                        break;
                                    case "3":
                                        GetAllContents(ContentType.Audio);
                                        break;
                                    case "4":
                                        GetAllContents(ContentType.Document);
                                        break;
                                    default:
                                        Console.WriteLine("Некоректний ввод");
                                        break;
                                }
                                break;
                            case "3":
                                ShowContentOptions();
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        GetContent(ContentType.Book);
                                        break;
                                    case "2":
                                        GetContent(ContentType.Video);
                                        break;
                                    case "3":
                                        GetContent(ContentType.Audio);
                                        break;
                                    case "4":
                                        GetContent(ContentType.Document);
                                        break;
                                    default:
                                        Console.WriteLine("Некоректний ввод");
                                        break;
                                }
                                break;
                            case "4":
                                try
                                {
                                    Console.WriteLine("Введіть назву контенту: ");
                                    string title = Console.ReadLine();

                                    Console.WriteLine("Введіть айді нового сховища: ");
                                    if (!int.TryParse(Console.ReadLine(), out int newStorageId) || newStorageId <= 0)
                                    {
                                        Console.WriteLine("Некортектний ввод. Введіть число більше за 0.");
                                        continue;
                                    }
                                    contentService.UpdateContentStorage(title, newStorageId);
                                    Console.WriteLine("Контент успішно оновлено");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Помилка: " + ex.Message);
                                }
                                break;
                            case "5":
                                try
                                {
                                    Console.WriteLine("Введіть назву контенту: ");
                                    contentService.DeleteContent(Console.ReadLine());
                                    Console.WriteLine("Контент успішно видалено");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Помилка: " + ex.Message);
                                }
                                break;
                            default:
                                Console.WriteLine("Некоректний ввод");
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("1. Додати");
                        Console.WriteLine("2. Отримати все");
                        Console.WriteLine("3. Отримати за назвою");
                        Console.WriteLine("4. Оновити дані");
                        Console.WriteLine("5. Видалити");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                AddStorage();
                                break;
                            case "2":
                                GetAllStorages();
                                break;
                            case "3":
                                GetStorageByName();
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

        private static void ShowContentOptions()
        {
            Console.WriteLine("Виберіть тип контенту: ");
            Console.WriteLine("1. Книги");
            Console.WriteLine("2. Відео");
            Console.WriteLine("3. Аудіо");
            Console.WriteLine("4. Документ");
        }

        private void AddContent(ContentType type)
        {
            switch (type)
            {
                case ContentType.Book:
                    Console.WriteLine("Введіть назву: ");
                    string bookTitle = Console.ReadLine();

                    Console.WriteLine("Введіть формат: ");
                    string bookFormat = Console.ReadLine();

                    Console.WriteLine("Введіть назву сховища: ");
                    var bookStorage = storageService.GetStorageByName(Console.ReadLine());

                    if (bookStorage == null)
                    {
                        Console.WriteLine("Сховища не існує. Спочатку додайте його.");
                        break;
                    }

                    Console.WriteLine("Введіть ім'я автора: ");
                    string author = Console.ReadLine();

                    Console.WriteLine("Введіть кількість сторінок: ");
                    if (!int.TryParse(Console.ReadLine(), out int pageCount) || pageCount <= 0)
                    {
                        Console.WriteLine("Некортектний ввод. Введіть число більше за 0.");
                        break;
                    }

                    bookService.AddBook(bookTitle, bookFormat, bookStorage.StorageId, author, pageCount);
                    Console.WriteLine("Книгу успішно додано");
                    break;
                case ContentType.Video:
                    Console.WriteLine("Введіть назву: ");
                    string videoTitle = Console.ReadLine();

                    Console.WriteLine("Введіть формат: ");
                    string videoFormat = Console.ReadLine();

                    Console.WriteLine("Введіть назву сховища: ");
                    var videoStorage = storageService.GetStorageByName(Console.ReadLine());

                    if (videoStorage == null)
                    {
                        Console.WriteLine("Сховища не існує. Спочатку додайте його.");
                        break;
                    }

                    Console.WriteLine("Введіть довжину відео (в секундах): ");
                    if (!int.TryParse(Console.ReadLine(), out int duration) || duration <= 0)
                    {
                        Console.WriteLine("Некортектний ввод. Введіть число більше за 0.");
                        break;
                    }

                    Console.WriteLine("Введіть розширення відео: ");
                    string resolution = Console.ReadLine();

                    videoService.AddVideo(videoTitle, videoFormat, videoStorage.StorageId, duration, resolution);
                    Console.WriteLine("Відео успішно додано");
                    break;

                case ContentType.Audio:
                    Console.WriteLine("Введіть назву: ");
                    string audioTitle = Console.ReadLine();

                    Console.WriteLine("Введіть формат: ");
                    string audioFormat = Console.ReadLine();

                    Console.WriteLine("Введіть назву сховища: ");
                    var audioStorage = storageService.GetStorageByName(Console.ReadLine());

                    if (audioStorage == null)
                    {
                        Console.WriteLine("Сховища не існує. Спочатку додайте його.");
                        break;
                    }

                    Console.WriteLine("Введіть бітрейт аудіо: ");
                    if (!int.TryParse(Console.ReadLine(), out int bitRate) || bitRate <= 0)
                    {
                        Console.WriteLine("Некоректний ввід. Введіть число більше за 0.");
                        break;
                    }

                    Console.WriteLine("Введіть кількість каналів аудіо: ");
                    if (!int.TryParse(Console.ReadLine(), out int channels) || channels <= 0)
                    {
                        Console.WriteLine("Некоректний ввід. Введіть число більше за 0.");
                        break;
                    }

                    audioService.AddAudio(audioTitle, audioFormat, audioStorage.StorageId, bitRate, channels);
                    Console.WriteLine("Аудіо успішно додано");
                    break;

                case ContentType.Document:
                    Console.WriteLine("Введіть назву: ");
                    string documentTitle = Console.ReadLine();

                    Console.WriteLine("Введіть формат: ");
                    string documentFormat = Console.ReadLine();

                    Console.WriteLine("Введіть назву сховища: ");
                    var documentStorage = storageService.GetStorageByName(Console.ReadLine());

                    if (documentStorage == null)
                    {
                        Console.WriteLine("Сховища не існує. Спочатку додайте його.");
                        break;
                    }

                    DateTime creationDate = DateTime.Now;

                    Console.WriteLine("Введіть шлях до файлу: ");
                    string filePath = Console.ReadLine();

                    documentService.AddDocument(documentTitle, documentFormat, documentStorage.StorageId, creationDate, filePath);
                    Console.WriteLine("Документ успішно додано");
                    break;
            }
        }

        private void GetAllContents(ContentType type)
        {
            switch (type)
            {
                case ContentType.Book:
                    var books = bookService.GetAllBooks();
                    Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Автор", "Стор."));
                    foreach (var book in books)
                    {
                        Console.WriteLine(book);
                    }
                    break;
                case ContentType.Video:
                    var videos = videoService.GetAllVideos();
                    Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Довжина", "Розширення"));
                    foreach (var video in videos)
                    {
                        Console.WriteLine(video);
                    }
                    break;

                case ContentType.Audio:
                    var audios = audioService.GetAllAudios();
                    Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Бітрейт", "Канали"));
                    foreach (var audio in audios)
                    {
                        Console.WriteLine(audio);
                    }
                    break;

                case ContentType.Document:
                    var documents = documentService.GetAllDocuments();
                    Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                        "Айді", "Назва", "Формат", "Сховище", "Адреса", "Дата створення", "Шлях до файлу"));
                    foreach (var document in documents)
                    {
                        Console.WriteLine(document);
                    }
                    break;
            }
        }

        private void GetContent(ContentType type)
        {
            switch (type)
            {
                case ContentType.Book:
                    Console.WriteLine("Введіть назву книги: ");
                    var book = bookService.GetBookByTitle(Console.ReadLine());
                    if (book != null)
                    {
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                            "Айді", "Назва", "Формат", "Сховище", "Адреса", "Автор", "Стор."));
                        Console.WriteLine(book);
                    }
                    else
                    {
                        Console.WriteLine("Книга не знайдена.");
                    }
                    break;

                case ContentType.Video:
                    Console.WriteLine("Введіть назву відео: ");
                    var video = videoService.GetVideoByTitle(Console.ReadLine());
                    if (video != null)
                    {
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                            "Айді", "Назва", "Формат", "Сховище", "Адреса", "Довжина", "Розширення"));
                        Console.WriteLine(video);
                    }
                    else
                    {
                        Console.WriteLine("Відео не знайдено.");
                    }
                    break;

                case ContentType.Audio:
                    Console.WriteLine("Введіть назву аудіо: ");
                    var audio = audioService.GetAudioByTitle(Console.ReadLine());
                    if (audio != null)
                    {
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                            "Айді", "Назва", "Формат", "Сховище", "Адреса", "Бітрейт", "Канали"));
                        Console.WriteLine(audio);
                    }
                    else
                    {
                        Console.WriteLine("Аудіо не знайдено.");
                    }
                    break;

                case ContentType.Document:
                    Console.WriteLine("Введіть назву документа: ");
                    var document = documentService.GetDocumentByTitle(Console.ReadLine());
                    if (document != null)
                    {
                        Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                            "Айді", "Назва", "Формат", "Сховище", "Адреса", "Дата створення", "Шлях до файлу"));
                        Console.WriteLine(document);
                    }
                    else
                    {
                        Console.WriteLine("Документ не знайдено.");
                    }
                    break;
            }
        }

        private void AddStorage()
        {
            Console.WriteLine("Введіть назву: ");
            string name = Console.ReadLine();

            Console.WriteLine("Введіть адресу: ");
            string address = Console.ReadLine();

            storageService.AddStorage(name, address);

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

        private void GetStorageByName()
        {
            Console.WriteLine("Введіть назву сховища: ");
            var storage = storageService.GetStorageByName(Console.ReadLine());
            Console.WriteLine(string.Format("{0, -5} {1, -15} {2, -15}", "Айді", "Назва", "Адресса"));
            Console.WriteLine(storage);
        }

        private void UpdateStorageData()
        {
            try
            {
                Console.WriteLine("Введіть назву сховища: ");
                string name = Console.ReadLine();

                Console.WriteLine("Введіть нову назву сховища: ");
                string newName = Console.ReadLine();

                Console.WriteLine("Введіть нову адрессу сховища: ");
                string newAddress = Console.ReadLine();

                storageService.UpdateStorage(name, newName, newAddress);
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
                Console.WriteLine("Введіть назву сховища: ");
                storageService.DeleteStorage(Console.ReadLine());
                Console.WriteLine("Сховище успішно видалено");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
