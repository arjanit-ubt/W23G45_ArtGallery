using System.ComponentModel.DataAnnotations;

namespace CrowdfundedArtGallery.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IFormFileCollection;

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > _maxFileSize)
                    {
                        return new ValidationResult($"The file {file.FileName} exceeds the maximum allowed file size.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}