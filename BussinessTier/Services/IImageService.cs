﻿using Microsoft.AspNetCore.Http;
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
        Task<ImageModel> UploadImageAsync(IFormFile file);
        Task DeleteImageAsync(Guid id);
    }
}
