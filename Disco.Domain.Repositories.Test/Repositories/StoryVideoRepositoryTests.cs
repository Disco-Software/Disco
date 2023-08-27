namespace Disco.Domain.Repositories.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Disco.Domain.EF;
    using Disco.Domain.Models.Models;
    using Disco.Domain.Repositories.Repositories;
    using NUnit.Framework;

    [TestFixture]
    public class StoryVideoRepositoryTests
    {
        private StoryVideoRepository _testClass;
        private ApiDbContext _ctx;

        [SetUp]
        public void SetUp()
        {
            _ctx = new ApiDbContext();
            _testClass = new StoryVideoRepository(_ctx);
        }

        [Test]
        public void CanConstruct()
        {
            // Act
            var instance = new StoryVideoRepository(_ctx);

            // Assert
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CannotConstructWithNullCtx()
        {
            Assert.Throws<ArgumentNullException>(() => new StoryVideoRepository(default(ApiDbContext)));
        }

        [Test]
        public async Task CanCallAddAsync()
        {
            // Arrange
            var storyVideo = new StoryVideo
            {
                Source = "TestValue1108443858",
                DateOfCreation = DateTime.UtcNow,
                StoryId = 868766530,
                Story = new Story
                {
                    StoryImages = new List<StoryImage>(),
                    StoryVideos = new List<StoryVideo>(),
                    DateOfCreation = DateTime.UtcNow,
                    AccountId = 449781444,
                    Account = new Account
                    {
                        AccountStatus = new AccountStatus
                        {
                            LastStatus = "TestValue1923250786",
                            FollowersCount = 1356442916,
                            NextStatusId = 89922661,
                            UserTarget = 1203297054,
                            AccountId = 1418111917,
                            Account = default(Account)
                        },
                        Cread = "TestValue43728868",
                        Photo = "TestValue178822172",
                        AccountGroups = new List<AccountGroup>(),
                        Connections = new List<Connection>(),
                        Messages = new List<Message>(),
                        Posts = new List<Post>(),
                        Comments = new List<Comment>(),
                        Likes = new List<Like>(),
                        Followers = new List<UserFollower>(),
                        Following = new List<UserFollower>(),
                        Stories = new List<Story>(),
                        UserId = 927935762,
                        User = new User
                        {
                            RoleName = "TestValue259831655",
                            RefreshToken = "TestValue359121794",
                            RefreshTokenExpiress = DateTime.UtcNow,
                            DateOfRegister = DateTime.UtcNow,
                            AccountId = 187550445,
                            Account = default(Account)
                        }
                    }
                }
            };

            // Act
            await _testClass.AddAsync(storyVideo);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallAddAsyncWithNullStoryVideo()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.AddAsync(default(StoryVideo)));
        }

        [Test]
        public async Task CanCallRemove()
        {
            // Arrange
            var id = 378375814;

            // Act
            await _testClass.Remove(id);

            // Assert
            Assert.Fail("Create or modify test");
        }
    }
}