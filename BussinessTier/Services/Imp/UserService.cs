using DataAccess.Repositories;
using FluentValidation;
using Models.Dto;
using Models.Dto.ShowData;
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

        public async Task<IEnumerable<UserDtoShow>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return (IEnumerable<UserDtoShow>)users.Select(async user => new UserDtoShow
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                ImageUser = await _imageService.GetImageFile(user.ImageId)
            }).ToList();
        }

        public async Task<UserDtoShow> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDtoShow
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                ImageUser = await _imageService.GetImageFile(user.ImageId)

            };
        }

        public async Task<UserModel> CreateUserAsync(UserDTO userDto)
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
                PasswordHash = userDto.PasswordHash,
                ImageId = imageDto?.Id
            };

            await _userRepository.AddAsync(user);

            
            return user;
        }

        public async Task<UserModel> UpdateUserAsync(UserDTO userDto)
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

            return user;
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
