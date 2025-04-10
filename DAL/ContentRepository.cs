using DAL.DataModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class ContentRepository : IRepository<ContentItem>
    {
        private readonly LibraryContext context;

        public ContentRepository(LibraryContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var content = context.Contents.Find(id);
            context.Contents.Remove(content);
        }

        public IEnumerable<ContentItem> GetAll()
        {
            return context.Contents
                .Include(c => c.Storage)
                .AsNoTracking()
                .ToList();
        }

        public ContentItem GetById(int id)
        {
            return context.Contents.Find(id);
        }

        public void Insert(ContentItem contentItem)
        {
            context.Contents.Add(contentItem);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(ContentItem contentItem)
        {
            context.Entry(contentItem).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
