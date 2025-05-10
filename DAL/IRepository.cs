using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository <TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Delete(object id);
        void Update(TEntity entity);
        IEnumerable<TEntity> Get();
        TEntity GetByID(object id);
    }
}
