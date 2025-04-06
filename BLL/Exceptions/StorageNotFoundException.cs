using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class StorageNotFoundException : Exception
    {
        public StorageNotFoundException() : base("Сховище не знайдено")
        {
        }
    }
}
