using System;

namespace BLL.Exceptions
{
    public class StorageNotFoundException : Exception
    {
        public StorageNotFoundException() : base("Сховище не знайдено")
        {
        }
    }
}
