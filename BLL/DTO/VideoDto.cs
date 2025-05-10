
namespace BLL.DTO
{
    public class VideoDto : ContentItemDto
    {
        public double Duration { get; set; }
        public string Resolution { get; set; }

        public override string ToString()
        {
            return string.Format(string.Format("{0, -5} {1, -15} {2, -15} {3, -15} {4, -20} {5, -15} {6, -15}",
                ContentItemId, Title, Format, Storage.Name, Storage.Address, Duration, Resolution));
        }
    }
}
