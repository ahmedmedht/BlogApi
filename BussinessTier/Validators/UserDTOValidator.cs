using FluentValidation;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(150).WithMessage("Username must be less than 150 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.ImageFile)
                .Must(file => file == null || file.Length > 0).WithMessage("Image file must not be empty if provided.");
        }
    }
}
