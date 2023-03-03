﻿using AutoMapper;
using Azure.Storage.Blobs;
using Disco.Business.Constants;
using Disco.Business.Interfaces;
using Disco.Business.Interfaces.Dtos.Apple;
using Disco.Business.Interfaces.Dtos.Account;
using Disco.Business.Interfaces.Dtos.EmailNotifications;
using Disco.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Disco.Domain.Interfaces;
using Disco.Business.Interfaces.Dtos.Google;
using System.Security.Claims;
using System.Collections.Generic;
using Disco.Business.Exceptions;
using Disco.Business.Interfaces.Interfaces;
using Disco.Domain.Models.Models;

namespace Disco.Business.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountService(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IAccountRepository accountRepository,
            IAccountStatusRepository accountStatusRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _accountStatusRepository = accountStatusRepository;
        }


        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? 
                throw new UserNotFoundException($"User with this Email -> {email} not found");

            user.Account = await _accountRepository.GetAsync(user.AccountId);
            user.RoleName = _userRepository.GetUserRole(user);
            user.Account.AccountStatus = await _accountStatusRepository.GetStatusByFollowersCountAsync(user.Account.Following.Count);
            user.Account.Connections = await _accountRepository.GetAllAccountConnectionsAsync(user.Account.Id);

            return user;
        }

        public async Task SaveRefreshTokenAsync(User user, string refreshToken)
        {
            await _userRepository.SaveRefreshTokenAsync(user, refreshToken);
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
        }

        public async Task<User> GetAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal) ??
                throw new UserNotFoundException("User not found");

            user.Account = await _accountRepository.GetAsync(user.AccountId);
            user.RoleName = _userRepository.GetUserRole(user);
            user.Account.AccountStatus = await _accountStatusRepository.GetStatusByFollowersCountAsync(user.Account.Followers.Count);
            user.Account.AccountStatus.Account = user.Account;
            user.Account.AccountStatus.AccountId = user.AccountId;
            user.Account.Connections = await _accountRepository.GetAllAccountConnectionsAsync(user.Account.Id);

            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString()) ?? 
                throw new UserNotFoundException($"User with this id -> {id}, not found");

            user.Account = await _accountRepository.GetAsync(user.AccountId);
            user.RoleName = _userRepository.GetUserRole(user);
            user.Account.AccountStatus = await _accountStatusRepository.GetStatusByFollowersCountAsync(user.Account.Followers.Count);
            user.Account.Connections = await _accountRepository.GetAllAccountConnectionsAsync(user.Account.Id);

            return user;
        }

        public async Task CreateAsync(User user)
        {
           user.NormalizedEmail = _userManager.NormalizeEmail(user.Email);
           user.NormalizedUserName = _userManager.NormalizeName(user.UserName);
           user.Account.AccountStatus = await _accountStatusRepository.GetStatusByFollowersCountAsync(user.Account.Followers.Count);

           var identityReesult = await _userManager.CreateAsync(user);
           if(identityReesult.Errors.Count() > 0)
           {
               throw new Exception();
           }

           user.DateOfRegister = DateTime.UtcNow;
           user.AccountId = user.Account.Id;

           await _userManager.AddToRoleAsync(user, UserRole.User);
           user.RoleName = _userRepository.GetUserRole(user);
         }

        public async Task<User> GetByLogInProviderAsync(string loginProvider, string providerKey)
        {
            var user = await _userManager.FindByLoginAsync(loginProvider, providerKey) 
                ?? null;
            
            user.Account = await _accountRepository.GetAsync(user.AccountId);
            user.RoleName = _userRepository.GetUserRole(user);
            user.Account.AccountStatus = await _accountStatusRepository.GetStatusByFollowersCountAsync(user.Account.Followers.Count);
            user.Account.AccountStatus.Account = user.Account;
            user.Account.AccountStatus.AccountId = user.AccountId;
            user.Account.Connections = await _accountRepository.GetAllAccountConnectionsAsync(user.Account.Id);

            return user;
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name) ?? 
                throw new UserNotFoundException($"User with this name -> {name}, not found");

            user.Account = await _accountRepository.GetAsync(user.AccountId);
            user.RoleName = _userRepository.GetUserRole(user);
            user.Account.AccountStatus = await _accountStatusRepository.GetStatusByFollowersCountAsync(user.Account.Followers.Count);
            user.Account.Connections = await _accountRepository.GetAllAccountConnectionsAsync(user.Account.Id);

            return user;
        }

        public async Task<IEnumerable<User>> GetAccountsByPeriotAsync(int periot)
        {
            return await _userRepository.GetUsersByPeriotIntAsync(periot);
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
           return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task RemoveAsync(User user)
        {
            await _userManager.DeleteAsync(user);
        }
    }
}
