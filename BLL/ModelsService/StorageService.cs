using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
using DAL;
using DAL.DataModels;

namespace BLL.ModelsService
{
    public class StorageService
    {
        private readonly LibraryContext _context = new LibraryContext();

        public void AddStorage(string name, string address)
        {
            var storage = new Storage()
            {
                Name = name,
                Address = address
            };
            _context.Storages.Add(storage);
            _context.SaveChanges();
        }

        public Storage GetStorageByName(string name)
        {
            return _context.Storages.FirstOrDefault(b => b.Name == name);
        }

        public List<Storage> GetAllStorages()
        {
            return _context.Storages.ToList();
        }

        public void UpdateStorage(string name, string newName, string address)
        {
            var storage = GetStorageByName(name);
            if (storage == null)
            {
                throw new StorageNotFoundException();
            }

            storage.Name = newName;
            storage.Address = address;
            _context.SaveChanges();
        }

        public void DeleteStorage(string name)
        {
            var storage = GetStorageByName(name);
            if (storage == null)
            {
                throw new StorageNotFoundException();
            }
            _context.Storages.Remove(storage);
            _context.SaveChanges();
        }
    }
}
