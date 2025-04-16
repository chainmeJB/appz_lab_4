using System;

namespace DAL.DataModels
{
    public class Document : ContentItem
    {
        public DateTime CreationDate { get; set; }
        public string FilePath { get; set; }
    }
}
