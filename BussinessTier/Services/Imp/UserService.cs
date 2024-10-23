using DataAccess.Repositories;
using FluentValidation;
using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Services.Imp
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly IValidator<UserDTO> _validator;

        public UserService(IUserRepository userRepository, IImageService imageService, IValidator<UserDTO> validator)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _validator = validator;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ImageId = user.ImageId,
                ImageUrl = user.UserImage != null ? $"/images/{user.ImageId}{Path.GetExtension(user.UserImage.ImageType)}" : null
            }).ToList();
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ImageId = user.ImageId,
                ImageUrl = user.UserImage != null ? $"/images/{user.ImageId}{Path.GetExtension(user.UserImage.ImageType)}" : null
            };
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            // Validate the DTO using FluentValidation
            var validationResult = await _validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            ImageDTO imageDto = null;
            if (userDto.ImageFile != null)
            {
                imageDto = await _imageService.UploadImageAsync(userDto.ImageFile);
            }

            var user = new UserModel
            {
                Id = Guid.NewGuid(),
                UserName = userDto.UserName,
                Email = userDto.Email,
                ImageId = imageDto?.Id
            };

            await _userRepository.AddAsync(user);

            userDto.Id = user.Id;
            userDto.ImageUrl = imageDto?.Url;
            return userDto;
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDto)
        {
            // Validate the DTO using FluentValidation
            var validationResult = await _validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;

            if (userDto.ImageFile != null)
            {
                if (user.ImageId.HasValue)
                {
                    await _imageService.DeleteImageAsync(user.ImageId.Value);
                }

                var imageDto = await _imageService.UploadImageAsync(userDto.ImageFile);
                user.ImageId = imageDto.Id;
            }

            await _userRepository.UpdateAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ImageId = user.ImageId,
                ImageUrl = user.ImageId.HasValue ? $"/images/{user.ImageId}{Path.GetExtension(user.UserImage.ImageType)}" : null
            };
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.ImageId.HasValue)
            {
                await _imageService.DeleteImageAsync(user.ImageId.Value);
            }

            await _userRepository.DeleteAsync(id);
        }
    }
}
