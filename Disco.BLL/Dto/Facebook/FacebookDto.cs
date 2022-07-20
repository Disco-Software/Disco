﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Disco.Business.Dto.Facebook
{
    public class FacebookDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = "";
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        public Picture Picture { get; set; }

    }
}
