﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Disco.Business.Dto.StoryVideos
{
    public class CreateStoryVideoDto
    {
        public IFormFile VideoFile { get; set; }
        public int StoryId { get; set; }
    }
}
