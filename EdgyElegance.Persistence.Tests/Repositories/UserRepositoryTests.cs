using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Identity.Entities;
using EdgyElegance.Persistence.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;

namespace EdgyElegance.Persistence.Tests.Repositories {
    public class UserRepositoryTests {
        private readonly ApplicationUser _request;
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;
        private readonly Mock<IBaseRepository<ApplicationUser>> _baseRepositoryMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests() {
            _userStoreMock = new();
            _userManagerMock = new(_userStoreMock.Object, null, null, null, null, null, null, null, null);
            _baseRepositoryMock = new();
            _userRepository = new UserRepository(_userManagerMock.Object, _baseRepositoryMock.Object);

            _request = new ApplicationUser {
                FirstName = "Test",
                LastName = "Test",
                Email = "email@email.com"
            };
        }

        [Fact]
        public async void Should_Not_Be_Empty_On_Completion() {
            // Arrange
            _userManagerMock.Setup(umm => umm.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userRepository.CreateAsync(_request, "123456");

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_Return_Value_Accordingly_To_Expression(bool exists) {
            // Arrange
            _baseRepositoryMock.Setup(brm => brm.Exists(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .Returns(exists);

            // Act
            bool result = _userRepository.UserExists(u => u.Email == "testmail@email.com");

            // Asserrt
            _baseRepositoryMock.Verify(rm => rm.Exists(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
            result.Should().Be(exists);
        }
    }
}
