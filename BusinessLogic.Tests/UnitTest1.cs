using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using System.Threading.Tasks;
using System;
using Xunit;
using BusinessLogic.Services;

namespace BusinessLogic.Tests
{

    public class UnitTest1
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;
        private readonly Mock<IUserService> userServiceMoq; 

        [Fact]
        public async Task CreateAsync_NullUser_ShouldThrowNullArgumentException()
        {
            
            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewUser()
        {
            var newUser = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                MiddleName = "Test",
                BirthDate = DateTime.MaxValue,
                Username = "Test",
                Password = ""
            };

            // act
            await service.Create(newUser);

            // assert
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }


    }
}