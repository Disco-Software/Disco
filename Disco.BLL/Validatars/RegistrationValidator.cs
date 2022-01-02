﻿using Disco.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Disco.BLL.Validatars
{
    public class RegistrationValidator : AbstractValidator<RegistrationModel>
    {
        public static RegistrationValidator instance = new RegistrationValidator();
        private RegistrationValidator()
        {
            RuleFor(f => f.FullName)
                .NotEmpty()
                .WithMessage("Full name is requared");
            
            RuleFor(f => f.UserName)
                .NotEmpty()
                .WithMessage("User name is requared");

            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address");

            RuleFor(m => m.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Length(6)
                .WithMessage("Password mast have 6 leters");
           
            RuleFor(m => m.ConfirmPassword)
                .Equal(s => s.Password)
                .WithMessage("Password and confirm password will be equal")
                .Length(6)
                .WithMessage("Confirm password mast have 6 leters")
                .NotEmpty()
                .WithMessage("Confirm password is required");
        }
    }
}
