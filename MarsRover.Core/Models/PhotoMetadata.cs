namespace MarsRover.Core.Models
{
    public class PhotoMetadata
    {      
        public int sol { get; set; }
        public string[] cameras { get; set; }
        public int total_photos { get; set; }
        public string earth_date { get; set; }
    }
}
