using Microsoft.AspNetCore.Http;
using Models.Model;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Imp
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<ImageModel>> GetAllImagesAsync()
        {
            return await _imageRepository.GetAllAsync();
        }

        public async Task<ImageModel> GetImageByIdAsync(Guid id)
        {
            return await _imageRepository.GetByIdAsync(id);
        }

        public async Task<ImageModel> UploadImageAsync(IFormFile file)
        {
            // Generate a new GUID for the image
            var imageGuid = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_imageFolderPath, $"{imageGuid}{extension}");

            // Save the file to the file system
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create a new ImageModel
            var image = new ImageModel
            {
                Id = imageGuid,
                Path = filePath,
                ImageType = extension,
    //            UploadedAt = DateTime.UtcNow
            };

            // Save image metadata to the database
            await _imageRepository.AddAsync(image);

            return image;
        }

        public async Task DeleteImageAsync(Guid id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            if (image != null)
            {
                // Delete the file from the file system
                if (File.Exists(image.Path))
                {
                    File.Delete(image.Path);
                }

                // Delete the image record from the database
                await _imageRepository.DeleteAsync(id);
            }
        }
    }
}
