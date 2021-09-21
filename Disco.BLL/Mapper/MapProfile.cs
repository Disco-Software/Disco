﻿using AutoMapper;
using Disco.BLL.DTO;
using Disco.BLL.Models;
using Disco.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Disco.BLL.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreatePostModel, Post>();
            CreateMap<Post, CreatePostModel>();
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
        }
    }
}
