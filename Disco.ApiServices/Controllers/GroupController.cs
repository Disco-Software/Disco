﻿using Disco.Business.Constants;
using Disco.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disco.ApiServices.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = AuthSchema.UserToken)]
    [Route("api/user/groups")]
    public class GroupController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IGroupService _groupService;

        public GroupController(
            IAccountService accountService,
            IGroupService groupService)
        {
            _accountService = accountService;
            _groupService = groupService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] int userId)
        {
            var currentUser = await _accountService.GetAsync(HttpContext.User);
            var user = await _accountService.GetByIdAsync(userId);

            var group = await _groupService.CreateAsync(currentUser.Account, user.Account);

            return Ok(group);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
           [FromQuery] int pageNumber, 
           [FromQuery] int pageSize)
        {
            var user = await _accountService.GetAsync(HttpContext.User);

            var groups = await _groupService.GetAllAsync(user.Id, pageNumber, pageSize);

            return Ok(groups);
        }
    }
}
