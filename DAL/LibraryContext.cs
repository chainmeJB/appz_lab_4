using DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibraryContext")
        { }

        public DbSet<ContentItem> Contents { get; set; }
        public DbSet<Storage> Storages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<Audio>().ToTable("Audios");
            modelBuilder.Entity<Document>().ToTable("Documents");
            base.OnModelCreating(modelBuilder);
        }
    }
}
