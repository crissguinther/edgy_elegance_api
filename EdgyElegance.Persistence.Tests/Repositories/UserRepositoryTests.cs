using EdgyElegance.Identity.Entities;
using EdgyElegance.Persistence.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EdgyElegance.Persistence.Tests.Repositories {
    public class UserRepositoryTests {
        private readonly ApplicationUser _request;
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;

        public UserRepositoryTests() {
            _userStoreMock = new Mock<IUserStore<ApplicationUser>>();

            _request = new ApplicationUser {
                FirstName = "Test",
                LastName = "Test",
                Email = "email@email.com"
            };
        }

        [Fact]
        public async void Should_Not_Be_Empty_On_Completion() {
            // Arrange
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(umm => umm.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var userRepository = new UserRepository(userManagerMock.Object);

            // Act
            var result = await userRepository.CreateAsync(_request, "123456");

            // Assert
            result.Should().NotBeNull();
        }
    }
}
