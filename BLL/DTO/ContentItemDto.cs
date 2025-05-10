using System;

namespace BLL.DTO
{
    public abstract class ContentItemDto
    {
        public int ContentItemId { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }

        public int StorageId { get; set; }
        public virtual StorageDto Storage { get; set; }

    }
}
