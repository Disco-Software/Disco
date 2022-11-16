﻿using Disco.Domain.EF;
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
    public class CommentRepository : BaseRepository<Comment,int>
    {
        public CommentRepository(ApiDbContext ctx) : base(ctx) { }

        public override async Task AddAsync(Comment item)
        {
            await _ctx.Comments.AddAsync(item);

            await _ctx.SaveChangesAsync();
        }

        public override async Task Remove(int id)
        {
            var comment = await _ctx.Comments
                .Where(s => s.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            _ctx.Comments.Remove(comment);
        }
    }
}
