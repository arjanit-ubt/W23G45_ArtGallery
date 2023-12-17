using CrowdfundedArtGallery.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace CrowdfundedArtGallery.ViewModels
{
    public class ArtPostViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Address { get; set; }
        public string ImageFolder { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Images")]
        [DataType(DataType.Upload)]
        [MaxFileSize(10 * 1024 * 1024)] // 10 MB
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", "bmp"})]
        public IFormFileCollection Images { get; set; }
    }
}
