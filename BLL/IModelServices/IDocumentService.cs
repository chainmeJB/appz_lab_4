using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IModelServices
{
    public interface IDocumentService
    {
        void AddDocument(DocumentDto document);
        IEnumerable<DocumentDto> GetAllDocuments();
        DocumentDto GetDocumentByID(int id);
    }
}
