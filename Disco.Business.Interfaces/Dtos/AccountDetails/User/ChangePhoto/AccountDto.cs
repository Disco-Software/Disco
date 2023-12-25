﻿namespace Disco.Business.Interfaces.Dtos.AccountDetails.User.ChangePhoto
{
    public class AccountDto
    {
        public AccountDto(
            string photo, 
            string cread)
        {
            Photo = photo;
            Cread = cread;
        }

        public string Photo { get; set; }
        public string Cread { get; set; }
    }
}
