﻿using Disco.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Disco.Business.Dto.Friends
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Friends { get; set; }
        public int Posts { get; set; }
        public int UserId { get; set; }
        public UserDto UserModel { get; set; }
    }
}
