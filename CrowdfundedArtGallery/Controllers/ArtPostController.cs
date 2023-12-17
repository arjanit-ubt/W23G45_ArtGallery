using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CrowdfundedArtGallery.Data;
using CrowdfundedArtGallery.Models;
using CrowdfundedArtGallery.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrowdfundedArtGallery.Controllers
{
    public class ArtPostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtPostController(ApplicationDbContext context)
        {
            _context = context;
        }

         // GET: /ArtPost/Create
        public IActionResult Create()
        {
            // Provide the necessary data for dropdowns, e.g., categories
            ViewBag.Categories = new SelectList(_context.Categories, "ID", "CategoryName");
            return View();
        }


        // GET: /ArtPost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArtPostViewModel model)
        {
            
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                        var artPost = new ArtPost
                        {
                            UserId = userId,
                            Title = model.Title,
                            Description = model.Description,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Price = model.Price,
                            CategoryId = model.CategoryId,
                            ImageFolder = null
                        };

                        _context.ArtPosts.Add(artPost);
                        _context.SaveChanges(); // Save changes to get the generated ID

                        try
                        {
                            SaveImages(model.Images, artPost.Id, userId);
                        }
                        catch (Exception ex)
                        {
                            // Log the exception
                            Console.WriteLine($"Error saving images: {ex.Message}");
                            // You might also want to rethrow the exception here if needed
                            throw;
                        }

                        // Update ImageFolder with the correct path
                        artPost.ImageFolder = Path.Combine("wwwroot", "ArtData", userId, artPost.Id.ToString());
                        _context.SaveChanges(); // Save changes to ImageFolder

                        transaction.Commit();

                        // Log success
                        Console.WriteLine($"ArtPost created successfully: {artPost.Id}");

                        return RedirectToAction("Details", new { id = artPost.Id });
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine($"Error creating ArtPost: {ex.Message}");
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the ArtPost.");
                    }
                }
            

            // Log ModelState errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"ModelState error: {error.ErrorMessage}");
            }

            // Provide the necessary data for dropdowns, e.g., categories
            ViewBag.Categories = new SelectList(_context.Categories, "ID", "CategoryName");
            return View(model);
        }

        private void SaveImages(IFormFileCollection images, int artPostId, string userId)
        {
            // Define the folder path for storing images
            var userFolderPath = Path.Combine("wwwroot", "ArtData", userId);
            var artPostFolderPath = Path.Combine(userFolderPath, artPostId.ToString());

            if (!Directory.Exists(userFolderPath))
            {
                Directory.CreateDirectory(userFolderPath);
            }

            if (!Directory.Exists(artPostFolderPath))
            {
                Directory.CreateDirectory(artPostFolderPath);
            }

            // Save each image to the server and update ImageFolder
            foreach (var imageFile in images)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(artPostFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

            }

        }
    }
}
        