using System;

namespace DAL.DataModels
{
    public class Document : ContentItem
    {
        public DateTime CreationDate { get; set; }
        public string FilePath { get; set; }

        public override string ToString()
        {
            return string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                 ContentItemId, Title, Format, Storage.Name, Storage.Address, CreationDate.ToString("yyyy-MM-dd"), FilePath);
        }
    }
}
