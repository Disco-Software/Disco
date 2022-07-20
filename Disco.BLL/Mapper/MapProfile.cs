﻿using AutoMapper;
using Disco.Business.Dto;
using Disco.Business.Dto;
using Disco.Business.Dto.Authentication;
using Disco.Business.Dto.Facebook;
using Disco.Business.Dto.Friends;
using Disco.Business.Dto.Images;
using Disco.Business.Dto.Posts;
using Disco.Business.Dto.Roles;
using Disco.Business.Dto.Songs;
using Disco.Business.Dto.Stories;
using Disco.Business.Dto.StoryImages;
using Disco.Business.Dto.StoryVideos;
using Disco.Business.Dto.Videos;
using Disco.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Disco.Business.Mapper
{
    public class MapProfile : AutoMapper.Profile
    {
        public MapProfile()
        {
            CreateMap<CreatePostDto, Post>()
                .ForMember(source => source.PostImages, opt => opt.Ignore())
                .ForMember(source => source.PostSongs, opt => opt.Ignore())
                .ForMember(source => source.PostVideos, opt => opt.Ignore());
            CreateMap<Post, PostResponseDto>();
            CreateMap<CreateImageDto, PostImage>()
                .ForMember(source => source.Source, opt => opt.Ignore());
            CreateMap<CreateSongDto, PostSong>()
                .ForMember(source => source.Source, opt => opt.Ignore());
            CreateMap<CreateVideoDto, PostVideo>()
                .ForMember(source => source.VideoSource, opt => opt.Ignore());
            CreateMap<CreateStoryDto,Story>()
                .ForMember(source => source.StoryImages, opt => opt.Ignore())
                .ForMember(source => source.StoryVideos, opt => opt.Ignore())
                .ForMember(source => source.DateOfCreation, opt => opt.Ignore());
            CreateMap<CreateStoryImageDto, StoryImage>()
                .ForMember(source => source.Source, opt => opt.Ignore());
            CreateMap<CreateStoryVideoDto, StoryVideo>()
                .ForMember(source => source.Source, opt => opt.Ignore());
            CreateMap<RegistrationDto, User>();
            CreateMap<CreateFriendDto, Friend>();
            CreateMap<Domain.Models.Profile, ProfileDto>();
            CreateMap<User, UserResponseDto>()
                .ForMember(source => source.RefreshToken, opt => opt.Ignore())
                .ForMember(source => source.AccessToken, opt => opt.Ignore());
            CreateMap<Domain.Models.Profile, ProfileDto>();
            CreateMap<ProfileDto, FriendResponseDto>()
                .ForMember(source => source.UserProfile, opt => opt.Ignore())
                .ForMember(source => source.FriendProfile, opt => opt.Ignore())
                .ForMember(source => source.FriendId, opt => opt.Ignore());
            CreateMap<CreateRoleDto, Role>();
            CreateMap<FacebookDto, User>()
                .ForMember(source => source.UserName, f => f.Ignore())
                .ForMember(source => source.Email, e => e.Ignore());
        }
    }
}
