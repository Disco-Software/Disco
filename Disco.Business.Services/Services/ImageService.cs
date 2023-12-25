﻿using AutoMapper;
using Azure.Storage.Blobs;
using Disco.Business.Interfaces.Dtos.PostImage.User.CreateImage;
using Disco.Business.Interfaces.Interfaces;
using Disco.Domain.Interfaces;
using Disco.Domain.Models.Models;

namespace Disco.Business.Services.Services
{
    public class ImageService : Interfaces.Interfaces.IImageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public ImageService(
            BlobServiceClient blobServiceClient,
            IMapper mapper, 
            IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
        }


        public async Task<PostImage> CreatePostImage(CreatePostImageRequestDto model)
        {
            var uniqueImageName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName.Replace(' ', '_');

            if (model.ImageFile == null)
                return null;

            if (model.ImageFile.Length == 0)
                return null;

            var containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var blobClient = containerClient.GetBlobClient(uniqueImageName);

            using var imageReader = model.ImageFile.OpenReadStream();
            var blobResult = blobClient.Upload(imageReader);

            var postImage = _mapper.Map<PostImage>(model);
            postImage.Source = blobClient.Uri.AbsoluteUri;

            await _imageRepository.AddAsync(postImage);

            return postImage;
        }

        public async Task RemoveImageAsync(int id)
        {
            var postImage = await _imageRepository.GetAsync(id);

           await _imageRepository.RemoveAsync(postImage);
        }
    }
}
