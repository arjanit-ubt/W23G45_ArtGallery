using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace CrowdfundedArtGallery.Models
{
    public class ArtPost
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }

        // Store only the image folder path
        [AllowNull]
        public string ImageFolder { get; set; }

        // Navigation property for Category
        public Category Category { get; set; }

        // Navigation property for IdentityUser (AspNetUsers table)
        public IdentityUser User { get; set; }


    }
}