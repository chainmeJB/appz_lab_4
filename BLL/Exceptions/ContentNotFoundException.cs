using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class ContentNotFoundException : Exception
    {
        public ContentNotFoundException() : base("Контент не знайдено")
        {
        }
    }
}
