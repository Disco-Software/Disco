﻿namespace Disco.Business.Interfaces.Dtos.Account
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
