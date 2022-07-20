﻿using AutoMapper;
using Azure.Storage.Blobs;
using Disco.Business.Interfaces;
using Disco.Business.Dto.Images;
using Disco.Domain.Models;
using Disco.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Disco.Business.Services
{
    public class ImageService : IImageService
    {
        private readonly PostRepository postRepository;
        private readonly ImageRepository imageRepository;
        private readonly BlobServiceClient blobServiceClient;
        private readonly IMapper mapper;

        public ImageService(
            PostRepository _postRepository,
            ImageRepository _imageRepository,
            BlobServiceClient _blobServiceClient,
            IMapper _mapper)
        {
            postRepository = _postRepository;
            imageRepository = _imageRepository;
            blobServiceClient = _blobServiceClient;
            mapper = _mapper;
        }


        public async Task<PostImage> CreatePostImage(CreateImageDto model)
        {
            var uniqueImageName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName.Replace(' ', '_');

            if (model.ImageFile == null)
                return null;

            if (model.ImageFile.Length == 0)
                return null;

            var containerClient = blobServiceClient.GetBlobContainerClient("images");
            var blobClient = containerClient.GetBlobClient(uniqueImageName);

            using var imageReader = model.ImageFile.OpenReadStream();
            var blobResult = blobClient.Upload(imageReader);

            var postImage = mapper.Map<PostImage>(model);
            postImage.Source = blobClient.Uri.AbsoluteUri;

            await imageRepository.Add(postImage);

            return postImage;
        }

        public async Task RemoveImage(int id)
        {
           await imageRepository.Remove(id);
        }
    }
}
