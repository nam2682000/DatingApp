using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using FluentValidation;

namespace Application.Validators
{
    public class UserValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserValidator()
        {
            RuleFor(user => user.Firstname)
                .NotEmpty().WithMessage("Firstname is required.")
                .Length(2, 50).WithMessage("Firstname must be between 2 and 50 characters.");

            RuleFor(user => user.Lastname)
                .NotEmpty().WithMessage("Lastname is required.")
                .Length(2, 50).WithMessage("Lastname must be between 2 and 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}