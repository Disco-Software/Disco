﻿using Disco.DAL.EF;
using Disco.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Disco.DAL.Repositories
{
    public class PostRepository : Base.BaseRepository<Post, int>
    {
        private readonly UserManager<User> userManager;

        public PostRepository(ApiDbContext _ctx) : base(_ctx) { }

        public PostRepository(ApiDbContext _ctx, UserManager<User> _userManager) : base(_ctx)
        {
            ctx = _ctx;
            userManager = _userManager;
        }

        public async Task<Post> AddAsync(Post post, User user)
        {
            if(user == null)
                throw new ArgumentNullException("user");
            
            await ctx.Posts.AddAsync(post);
            user.Profile.Posts.Add(post);
            await ctx.SaveChangesAsync();

            return post;
        }

        public async override Task Remove(int id)
        {
            var post = await ctx.Posts
                .Include(v => v.PostVideos)
                .Include(s => s.PostSongs)
                .Include(i => i.PostImages)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            ctx.Remove(post);
        }

        public async Task<List<Post>> GetAll(int userId)
        {
            var user = await ctx.Users
                .Include(p => p.Profile)
                .ThenInclude(f => f.Friends)
                .Where(s => s.Id == userId)
                .FirstOrDefaultAsync();
            foreach (var friend in user.Profile.Friends)
            {
                friend.ProfileFriend = await ctx.Profiles
                    .Include(p => p.Posts)
                    .ThenInclude(i => i.PostImages)
                    .Include(p => p.Posts)
                    .ThenInclude(s => s.PostSongs)
                    .Include(p => p.Posts)
                    .ThenInclude(v => v.PostVideos)
                    .Where(f => f.Id == friend.FriendProfileId)
                    .FirstOrDefaultAsync();
                var posts = friend.ProfileFriend.Posts.ToList();
                return posts;
            }
            return null;
        }

        public override Task<Post> Get(int id)
        {
           return ctx.Posts
                .Include(i => i.PostImages)
                .Include(s => s.PostSongs)
                .Include(v => v.PostVideos)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
