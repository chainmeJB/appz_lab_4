using System.Collections.Generic;

namespace DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T Item);
        void Update(T Item);
        void Delete(int id);
        void Save();
    }
}
