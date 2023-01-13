﻿using Disco.Domain.EF;
using Disco.Domain.Interfaces;
using Disco.Domain.Models;
using Disco.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disco.Domain.Repositories
{
    public class AccountGroupRepository : BaseRepository<AccountGroup, int>, IAccountGroupRepository
    {
        public AccountGroupRepository(ApiDbContext context) : base(context) { }

        public async Task CreateAsync(AccountGroup accountGroup)
        {
            await _ctx.AccountGroups.AddAsync(accountGroup);

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccountGroup accountGroup)
        {
            _ctx.AccountGroups.Remove(accountGroup);

            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountGroup>> GetAllAsync(int id)
        {
            return await _ctx.AccountGroups
                .Include(ag => ag.Group)
                .Include(ag => ag.Account)
                .Where(a => a.Id == id)
                .ToListAsync();
        }
    }
}
