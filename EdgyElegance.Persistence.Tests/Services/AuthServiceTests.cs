using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Identity.Entities;
using EdgyElegance.Persistence.Services;
using FluentAssertions;
using Moq;

namespace EdgyElegance.Persistence.Tests.Services {
    public class AuthServiceTests {
        private Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        private ApplicationUser User { get; set; }
        private AuthService AuthService { get; set; }
        private readonly RefreshToken _refreshToken;

        public AuthServiceTests() {
            User = new() {
                FirstName = "Test",
                LastName = "Test",
                Email = "email@email.com",
                PasswordHash = "onepasswordhash",
                Id = Guid.NewGuid().ToString()
                //RefreshTokens = new List<RefreshToken>()
            };

            UnitOfWorkMock = new();
            AuthService = new(UnitOfWorkMock.Object);

            _refreshToken = new() {
                ExpiresIn = DateTime.UtcNow.AddDays(1),
                Id = 1,
                IsRevoked = false,
                UserId = User.Id,
                User = User,
                IssuedAt = DateTime.UtcNow,
                Token = Guid.NewGuid().ToString()
            };
        }


        [Fact]
        public async void Should_Create_User_Auth_Token() {
            // Arrange            
            IList<string> roles = new List<string>() { "Admin" };

            UnitOfWorkMock.Setup(uowm => uowm.UserRepository.GetUserRoles(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(roles);

            Environment.SetEnvironmentVariable("JwtSecret", Guid.NewGuid().ToString());

            //Act
            var result = await AuthService.CreateTokenAsync(User);

            // Assert
            result.Should().NotBeNull();
            result.Length.Should().BeGreaterThan(0);
            UnitOfWorkMock.Verify(uowm => uowm.UserRepository.GetUserRoles(User), Times.Once);
            UnitOfWorkMock.Verify(uowm => uowm.Commit(), Times.Never);
        }

        [Fact]
        public void Should_Create_And_Store_User_Refresh_Token() {
            // Arrange
            UnitOfWorkMock.Setup(uowm => uowm.AuthRepository)
                .Returns(new Mock<IAuthRepository>().Object);

            // Act
            var response = AuthService.CreateRefreshToken(User);

            // Assert
            UnitOfWorkMock.Verify(uowm => uowm.AuthRepository.CreateRefreshToken(User, It.IsAny<RefreshToken>()), Times.Once);
            UnitOfWorkMock.Verify(uowm => uowm.Commit(), Times.Once);
        }
    }
}
