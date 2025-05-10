
namespace BLL.DTO
{
    public class AudioDto : ContentItemDto
    {
        public int BitRate { get; set; }
        public int Channels { get; set; }

        public override string ToString()
        {
            return string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                ContentItemId, Title, Format, Storage.Name, Storage.Address, BitRate, Channels);
        }
    }
}
