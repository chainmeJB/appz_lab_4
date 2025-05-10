using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IModelServices
{
    public interface IContentService
    {
        void AddContent(ContentItemDto item);
        ContentItemDto GetContent(int id);
        void UpdateContent(ContentItemDto item);
        void DeleteContent(int id);
    }
}
