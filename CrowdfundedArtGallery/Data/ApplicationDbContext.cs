using CrowdfundedArtGallery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrowdfundedArtGallery.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SocialLinks> SocialLinks { get; set; }
        public DbSet<ArtPost> ArtPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between ArtPost and Category
            modelBuilder.Entity<ArtPost>()
                .HasOne(ap => ap.Category)
                .WithMany()
                .HasForeignKey(ap => ap.CategoryId);

            // Configure the relationship between ArtPost and IdentityUser (AspNetUsers table)
            modelBuilder.Entity<ArtPost>()
                .HasOne(ap => ap.User)
                .WithMany()
                .HasForeignKey(ap => ap.UserId)
                .IsRequired();

            // Optionally, if you want to expose the IdentityUser in the ArtPost model
            modelBuilder.Entity<ArtPost>()
                .Navigation(ap => ap.User)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }

        
    }
}