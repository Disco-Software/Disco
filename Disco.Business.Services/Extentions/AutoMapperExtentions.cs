﻿using AutoMapper;
using Disco.Business.Services.Mapper;
using Disco.Business.Services.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Business.Services.Extentions
{
    public static class AutoMapperExtentions
    {
        public static void AddAutoMapper(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddSingleton(conf => new MapperConfiguration(options =>
            {
                options.AddProfile(new AccountMapProfile());
                options.AddProfile(new GlobalSearchMapProfile());
                options.AddProfile(new PostMapProfile());
                options.AddProfile(new StoryMapProfile());
                options.AddProfile(new RoleMapProfile());
                options.AddProfile(new FollowerMapProfile());
                options.AddProfile(new MessageMapProfile());
                options.AddProfile(new CommentMapProfile());
                options.AddProfile(new GroupMapProfile());
                options.AddProfile(new StatisticsMapProfile());
                options.AddProfile(new ErrorMapProfile());
            }).CreateMapper());
        }
    }
}
