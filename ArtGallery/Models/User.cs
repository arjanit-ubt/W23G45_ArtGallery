
using System.ComponentModel.DataAnnotations;


namespace ArtGallery.Models
{

    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Additional properties as needed

        // You can also add navigation properties if you are working with relationships
    }
}
