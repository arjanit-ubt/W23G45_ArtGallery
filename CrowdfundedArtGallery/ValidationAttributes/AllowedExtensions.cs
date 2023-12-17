using System.ComponentModel.DataAnnotations;

namespace CrowdfundedArtGallery.ValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IFormFileCollection;

            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(fileExtension) || !_allowedExtensions.Contains(fileExtension))
                    {
                        return new ValidationResult($"The file {file.FileName} has an invalid extension.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
