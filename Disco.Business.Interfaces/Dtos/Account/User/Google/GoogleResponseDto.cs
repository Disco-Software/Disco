﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disco.Business.Interfaces.Dtos.Account.User.Google
{
    public class GoogleResponseDto
    {
        public GoogleResponseDto(
            UserDto user,
            string accessToken,
            string refreshToken)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public UserDto User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken {  get; set; }
    }
}
