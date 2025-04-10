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

        public override string ToString()
        {
            return string.Format("{0, -5} {1, -15} {2, -15}", StorageId, Name, Address);
        }
    }
}
