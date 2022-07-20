﻿using Disco.Business.Dto.Authentication;
using Disco.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Disco.Business.Interfaces
{
    public interface IAdminUserService
    {
        Task<IActionResult> CreateUserAsync(RegistrationDto model);
        Task<IActionResult> RemoveUserAsync(int id);
        Task<ActionResult<List<User>>> GetAllUsers(int pageNumber, int pageSize);
    }
}
