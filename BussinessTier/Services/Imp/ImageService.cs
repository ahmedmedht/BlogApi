using Microsoft.AspNetCore.Http;
using Models.Model;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Dto;

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

        public async Task<ImageDTO> UploadImageAsync(IFormFile file)
        {
            var imageGuid = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_imageFolderPath, $"{imageGuid}{extension}");

            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
            // Save the image to the file system
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create the image entity
            var image = new ImageModel
            {
                Id = imageGuid,
                Path = filePath,
                ImageType = extension,
            };

            await _imageRepository.AddAsync(image);

            // Return the DTO for the newly uploaded image
            return new ImageDTO
            {
                Id = image.Id,
                Path = image.Path,
                ImageType = image.ImageType,
                Url = $"/images/{imageGuid}{extension}" // Example URL generation
            };
        }

        public async Task DeleteImageAsync(Guid imageId)
        {
            var image = await _imageRepository.GetByIdAsync(imageId);

            if (image != null)
            {
                // Delete the image file from the file system
                if (File.Exists(image.Path))
                {
                    File.Delete(image.Path);
                }

                // Delete the image record from the database
                await _imageRepository.DeleteAsync(imageId);
            }
        }

        public async Task<byte[]> GetImageFile(Guid? imageId)
        {
            var imageInfo= await GetImageByIdAsync(imageId ?? Guid.Empty);
            if(imageInfo == null)
            {
                return [0];
            }
            var filePath = Path.Combine(_imageFolderPath, $"{imageInfo.Id + imageInfo.ImageType}");
            if (!File.Exists(filePath))
                return [0];

            var imageBytes = await File.ReadAllBytesAsync(filePath);
            return imageBytes;

        }
    }
}
