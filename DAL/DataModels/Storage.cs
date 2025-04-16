using System.Collections.Generic;

namespace DAL.DataModels
{
    public class Storage
    {
        public Storage() 
        {
            Items = new List<ContentItem>();
        }
        public int StorageId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<ContentItem> Items { get; set; }
    }
}
