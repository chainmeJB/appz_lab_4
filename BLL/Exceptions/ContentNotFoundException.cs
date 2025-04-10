using System;

namespace BLL.Exceptions
{
    public class ContentNotFoundException : Exception
    {
        public ContentNotFoundException() : base("Контент не знайдено")
        {
        }
    }
}
