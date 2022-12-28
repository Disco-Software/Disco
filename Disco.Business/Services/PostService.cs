﻿using AutoMapper;
using Disco.Business.Interfaces;
using Disco.Business.Dtos.Posts;
using Disco.Business.Dtos.Songs;
using Disco.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disco.Domain.Interfaces;

namespace Disco.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostService(
            IMapper mapper,
            IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task CreatePostAsync(Post post)
        {            
            post.DateOfCreation = DateTime.UtcNow;
            post.Account.Posts.Add(post);

            await _postRepository.AddAsync(post);
        }

        public async Task DeletePostAsync(int postId)
        {
            await _postRepository.Remove(postId);
        }

        public async Task<List<Post>> GetAllUserPosts(User user,GetAllPostsDto dto)
        {
            return await Task.FromResult(_postRepository.GetUserPostsAsync(user.Id)
                .Result
                .OrderByDescending(post => post.DateOfCreation)
                .ToList());
        }

        public async Task<List<Post>> GetAllPostsAsync(User user, GetAllPostsDto dto)
        {
            var posts = await _postRepository.GetUserPostsAsync(user.Id);

            posts.AddRange(await _postRepository.GetFollowersPostsAsync(user.Account.Followers));
            posts.AddRange(await _postRepository.GetFollowingPostsAsync(user.Account.Following));

            posts.OrderByDescending(p => p.DateOfCreation)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();

            return posts;
        }
        public async Task<List<Post>> GetAllPostsAsync(User user)
        {
            var posts = await _postRepository.GetUserPostsAsync(user.Id);

            posts.AddRange(await _postRepository.GetFollowersPostsAsync(user.Account.Followers));
            posts.AddRange(await _postRepository.GetFollowingPostsAsync(user.Account.Following));

            posts.OrderByDescending(p => p.DateOfCreation)
                .ToList();

            return posts;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _postRepository.GetAsync(id);
        }

        public async Task<List<Post>> GetPostsByDescriptionAsync(string search)
        {
            return await _postRepository.GetPostsByDescriptionAsync(search);
        }

        public async Task<List<Post>> GetAllUserPosts(User user)
        {
            return await _postRepository.GetUserPostsAsync(user.Id);
        }
    }
}
