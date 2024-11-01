using Microsoft.AspNetCore.Http;
using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IImageService
    {
        Task<IEnumerable<ImageModel>> GetAllImagesAsync();
        Task<ImageModel> GetImageByIdAsync(Guid id);
        Task<ImageDTO> UploadImageAsync(IFormFile file);
        Task DeleteImageAsync(Guid id);
        Task<Byte[]> GetImageFile (Guid? imageId);
    }
}
