using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataModels;
namespace DAL
{
    public interface IUnitOfWork
    {
        IRepository<Storage> StorageRepository { get; }
        IRepository<ContentItem> ContentRepository { get; }
        void Save();
    }
}
