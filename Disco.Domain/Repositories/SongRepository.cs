﻿using Disco.Domain.EF;
using Disco.Domain.Interfaces;
using Disco.Domain.Models;
using Disco.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.Domain.Repositories
{
    public class SongRepository : BaseRepository<PostSong, int>, ISongRepository
    {
        public SongRepository(ApiDbContext ctx) : base(ctx) { }

        public override async Task AddAsync(PostSong song)
        {
            await _ctx.PostSongs.AddAsync(song);
        }

        public override async Task Remove(int id)
        {
            var song = await _ctx.PostSongs
                .Include(p => p.Post)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            song.Post.PostSongs.Remove(song);
            _ctx.PostSongs.Remove(song);

            await _ctx.SaveChangesAsync();
        }
    }
}
