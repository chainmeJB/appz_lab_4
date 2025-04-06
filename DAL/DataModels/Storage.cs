using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataModels
{
    public class Storage
    {
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
