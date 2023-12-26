﻿using Disco.ApiServices.Controllers;
using Disco.ApiServices.Features.AccountDetails.Admin.RequestHandlers.CreateAccount;
using Disco.ApiServices.Features.AccountDetails.Admin.RequestHandlers.DeleteAccount;
using Disco.ApiServices.Features.AccountDetails.Admin.RequestHandlers.GetAccountsByPeriot;
using Disco.ApiServices.Features.AccountDetails.Admin.RequestHandlers.GetAllAccounts;
using Disco.ApiServices.Features.AccountDetails.Admin.RequestHandlers.SearchAccountEmails;
using Disco.Business.Interfaces.Dtos.Account.User.Register;
using Disco.Business.Interfaces.Dtos.AccountDetails.Admin.CreateAccount;
using Disco.Business.Interfaces.Dtos.AccountDetails.Admin.GetAccountsByPeriot;
using Disco.Business.Interfaces.Dtos.AccountDetails.Admin.GetAllAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disco.ApiServices.Features.AccountDetails.Admin
{

    [Route("api/admin/account")]
    public class AccountDetailsController : AdminController
    {
        private readonly IMediator _mediator;

        public AccountDetailsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateAccountResponseDto>> Create([FromBody] CreateAccountRequestDto dto) =>
            await _mediator.Send(new CreateAccountRequest(dto));

        [HttpDelete("{id:int}")]
        public async Task Remove([FromRoute] int id) =>
            await _mediator.Send(new DeleteAccountRequest(id));

        [HttpGet]
        public async Task<IEnumerable<GetAllAccountsResponseDto>> GetAllAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize) =>
            await _mediator.Send(new GetAllAccountsRequest(pageNumber, pageSize));

        [HttpGet("periot")]
        public async Task<ActionResult<List<GetAccountsByPeriotResponseDto>>> GetAccountsByPeriotAsync(int periot) =>
            await _mediator.Send(new GetAccountsByPeriotRequest(periot));

        [HttpGet("emails/search")]
        public async Task<IEnumerable<string>> GetEmailsBySearchAsync([FromQuery] string search) =>
            await _mediator.Send(new SearchAccountEmailsRequest(search));
    }
}
