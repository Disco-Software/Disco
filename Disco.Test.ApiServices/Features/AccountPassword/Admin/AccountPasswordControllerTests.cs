namespace Disco.Test.ApiServices.Features.AccountPassword.Admin
{
    using System;
    using System.Threading.Tasks;
    using Disco.ApiServices.Features.AccountPassword.Admin;
    using Disco.Business.Interfaces.Dtos.Account;
    using MediatR;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class AccountPasswordControllerTests
    {
        private AccountPasswordController _testClass;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _testClass = new AccountPasswordController(_mediator);
        }

        [Test]
        public void CanConstruct()
        {
            // Act
            var instance = new AccountPasswordController(_mediator);

            // Assert
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CannotConstructWithNullMediator()
        {
            Assert.Throws<ArgumentNullException>(() => new AccountPasswordController(default(IMediator)));
        }

        [Test]
        public async Task CanCallForgotPassword()
        {
            // Arrange
            var dto = new ForgotPasswordDto { Email = "TestValue297667379" };

            // Act
            var result = await _testClass.ForgotPassword(dto);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallForgotPasswordWithNullDto()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.ForgotPassword(default(ForgotPasswordDto)));
        }

        [Test]
        public async Task CanCallResetPassword()
        {
            // Arrange
            var dto = new ResetPasswordDto
            {
                Email = "TestValue857413143",
                ConfirmationToken = "TestValue1297495088",
                Password = "TestValue206804330",
                ConfirmPassword = "TestValue1685026160"
            };

            // Act
            var result = await _testClass.ResetPassword(dto);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallResetPasswordWithNullDto()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.ResetPassword(default(ResetPasswordDto)));
        }
    }
}