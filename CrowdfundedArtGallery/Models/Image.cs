namespace CrowdfundedArtGallery.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public int ArtPostId { get; set; }
        public ArtPost ArtPost { get; set; }
    }
}
