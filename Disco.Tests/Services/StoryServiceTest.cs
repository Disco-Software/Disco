﻿using AutoMapper;
using Azure.Storage.Blobs;
using Disco.Business.Constants;
using Disco.Business.Dtos.Images;
using Disco.Business.Dtos.Stories;
using Disco.Business.Dtos.StoryImages;
using Disco.Business.Dtos.StoryVideos;
using Disco.Business.Interfaces;
using Disco.Business.Mapper;
using Disco.Business.Services;
using Disco.Domain.EF;
using Disco.Domain.Models;
using Disco.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disco.Domain.Interfaces;
using Account = Disco.Domain.Models.Account;

namespace Disco.Tests.Services
{
    [TestClass]
    public class StoryServiceTest
    {
        [TestMethod]
        public async Task CreateStoryWithImage_ReturnsSuccessResponse()
        {            
            var mockedStoryRepository = new Mock<IStoryRepository>();

            mockedStoryRepository
                .Setup(s => s.AddAsync(It.IsAny<Story>()))
                .Returns(Task.CompletedTask);

            var story = new Story
            {
                StoryImages = new List<StoryImage>(),
                StoryVideos = new List<StoryVideo>(),
                Account = new Account(),
                AccountId = 1,
            };

            var service = new StoryService(mockedStoryRepository.Object);
            var response = service.CreateStoryAsync(story);

            Assert.IsNotNull(response.IsFaulted);
        }
    }
}
