﻿using Disco.Business.Interfaces.Dtos.Account.User.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disco.Business.Services.Test.Account.Dtos.User.LogIn
{
    [TestFixture]
    public class AccountDtoTest
    {
        [Test]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            string photo = "photo.jpg";
            string cread = "abcd1234";

            // Act
            var accountDto = new AccountDto(photo, cread);

            // Assert
            Assert.AreEqual(photo, accountDto.Photo);
            Assert.AreEqual(cread, accountDto.Cread);
        }

        [Test]
        public void Properties_SetAndGetCorrectly()
        {
            // Arrange
            var accountDto = new AccountDto("photo.jpg", "abcd1234");

            // Assert
            Assert.AreEqual("photo.jpg", accountDto.Photo);
            Assert.AreEqual("abcd1234", accountDto.Cread);
        }
    }
}
