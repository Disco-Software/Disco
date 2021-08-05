﻿using Disco.BLL.Infrastructure;
using Disco.BLL.Interfaces;
using Disco.BLL.Models.DTO;
using Disco.DAL.EF;
using Disco.DAL.Entities;
using Disco.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disco.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext ctx;
        private readonly ApplicationUserManager manager;
        private readonly SignInManager<DAL.Entities.User> signInManager;
        private readonly IEmailService emailService;
        public AccountService(ApplicationUserManager userManager, SignInManager<DAL.Entities.User> signInManager,ApplicationDbContext ctx, IEmailService emailService)
        {
            this.ctx = ctx;
            this.signInManager = signInManager;
            this.manager = userManager;
            this.emailService = emailService;
        }


        public async Task<UserDTO> Login(LoginDTO login)
        {
            try
            {
                var user = await manager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    var hasherResult = manager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
                    if (hasherResult == PasswordVerificationResult.Success)
                    {
                        await signInManager.CanSignInAsync(user);
                        return new UserDTO { User = user, VarificationResult = Infrastructure.VarificationResults.Ok.ToString() };
                    }
                    else if (hasherResult == PasswordVerificationResult.Failed)
                        return new UserDTO { VarificationResult = Infrastructure.VarificationResults.PasswordNotValid.ToString() };
                }
                else if (user == null)
                    return new UserDTO { VarificationResult = Infrastructure.VarificationResults.UserNotFound.ToString() };
            }
            catch (ArgumentNullException)
            {
                if (login.Email == null)
                    return new UserDTO { VarificationResult = Infrastructure.VarificationResults.EmailCenNotBeNull.ToString() };
                else if (login.Password == null)
                    return new UserDTO { VarificationResult = Infrastructure.VarificationResults.PasswordCanNOtBeNull.ToString() };
            }
            return null;
        }

        public async Task<UserDTO> Register(RegisterDTO register)
        {
            var userCreate = await manager.FindByEmailAsync(register.Email);
            if(userCreate == null)
            {
                var user = new User
                {
                    Email = register.Email,
                    FirstName = register.FirstName,
                    UserName = register.UserName,
                };
                user.PasswordHash = manager.PasswordHasher.HashPassword(user, register.Password);
                ///EmailService service = new EmailService();
                //service.SendToEmail(register.Email, "Confirm email","",true);
                // user.EmailConfirmed = true;
                await manager.CreateAsync(user);
                await signInManager.CanSignInAsync(user);
                await ctx.SaveChangesAsync();
                return new UserDTO { User = user, VarificationResult = VarificationResults.Ok.ToString() };
            }
            return new UserDTO { User = null };
        }

        public async Task<ForgotPasswordResultDTO> ForgotPassword(ForgotPasswordDTO forgotPassword)
        {
            var user = await manager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
                return new ForgotPasswordResultDTO { ResponseMessage = ForgotPasswordResults.UserNotFound.ToString() };
            var result = await manager.ConfirmEmailAsync(user, forgotPassword.Code);
            var code = await manager.GenerateEmailConfirmationTokenAsync(user);
             if (forgotPassword.Code == null)
            {
                await emailService.SendToEmailAsync(
                    user.Email, 
                    subject: "Confirm email", 
                    isHTML: true);
                return new ForgotPasswordResultDTO { ResponseMessage = "Enter your code" };
            }

            return new ForgotPasswordResultDTO { Eamil = forgotPassword.Email, Password = "", ResponseMessage = ForgotPasswordResults.Ok.ToString()};
        }
    }
}
