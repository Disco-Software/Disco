﻿namespace Disco.Business.Interfaces.Dtos.Friends
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Friends { get; set; }
        public int Posts { get; set; }
        public int UserId { get; set; }
        public UserDto UserModel { get; set; }
    }
}
