﻿namespace Disco.Business.Interfaces.Dtos.Friends
{
    public class GetAllFollowersDto
    {
        public int UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
