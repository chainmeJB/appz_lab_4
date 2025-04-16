
namespace BLL.DTO
{
    public class StorageDto
    {
        public int StorageId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public StorageDto(string name, string address) 
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return string.Format("{0, -5} {1, -15} {2, -15}", StorageId, Name, Address);
        }
    }
}
