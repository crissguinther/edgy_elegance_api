using AutoMapper;
using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models;
using EdgyElegance.Identity.Entities;
using EdgyElegance.Persistence.Repositories;
using EdgyElegance.Persistence.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EdgyElegance.Persistence.Tests.Services {
    public class UserServiceTests {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ApplicationUser _request;
        private readonly UserModel _userModel;

        public UserServiceTests() {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = 
                new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            _userRepository = new UserRepository(_userManagerMock.Object);
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_mapperMock.Object, _userRepository);

            _request = new ApplicationUser {
                FirstName = "Test",
                LastName = "Test",
                Email = "email@email.com",
                PasswordHash = "onepasswordhash"
            };

            _userModel = new UserModel {
                FirstName = "Test",
                LastName = "Test",
                Email = "email@email.com",
                Password = "123456",
                PasswordConfirmation = "123456"
            };
        }

        [Fact]
        public void Create_User_Should_Map_And_Call_Repository() {
            // Arrange
            _mapperMock.Setup(m => m.Map<ApplicationUser>(_userModel))
                .Returns(_request);

            _userManagerMock.Setup(umm => umm.CreateAsync(_request))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            _userService.CreateUserAsync(_userModel);

            // Assert
            _mapperMock.Verify(mm => mm.Map<ApplicationUser>(_userModel), Times.Once);
            _userManagerMock.Verify(umm => umm.CreateAsync(_request, _userModel.Password!), Times.Once);
        }

        [Fact]
        public async void Should_Return_Success_When_User_Is_Created() {
            // Arrange
            _mapperMock.Setup(m => m.Map<ApplicationUser>(_userModel))
                .Returns(_request);

            _userManagerMock.Setup(umm => umm.CreateAsync(_request, _userModel.Password!))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.CreateUserAsync(_userModel);

            // Assert
            result.Success = true;
        }

        [Fact]
        public async void Should_Return_Errors_When_User_Is_Not_Created() {
            // Arrange
            _mapperMock.Setup(m => m.Map<ApplicationUser>(_userModel))
                .Returns(_request);

            _userManagerMock.Setup(umm => umm.CreateAsync(_request, _userModel.Password!))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = "User exits on database" }));

            // Act
            var result = await _userService.CreateUserAsync(_userModel);

            // Assert
            result.Success = true;
            result.Errors.Should().NotBeNull();
            result.Errors!.First().Should().Be("User exits on database");
        }

        [Theory]
        [InlineData("Customer", true)]
        [InlineData("Admin", false)]
        public async void Should_Add_To_Role_By_Looking_Through_Users_Email(string role, bool success) {
            // Arrange
            IdentityResult expectedResult = success
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError { Description = "User already has the role" });

            _userManagerMock.Setup(umm => umm.AddToRoleAsync(It.IsAny<ApplicationUser>(), role))
                .ReturnsAsync(expectedResult);

            _userManagerMock.Setup(umm => umm.FindByEmailAsync(_userModel.Email!))
                .ReturnsAsync(_request);

            // Act
            var result = await _userService.AddToRoleByEmailAsync(_userModel.Email!, role);

            // Assert
            result.Should().NotBeNull();
            _userManagerMock.Verify(umm => umm.FindByEmailAsync(_userModel.Email!), Times.Once);
            _userManagerMock.Verify(umm => umm.AddToRoleAsync(It.IsAny<ApplicationUser>(), role), Times.Once);
            result.Success.Should().Be(success);
            if (!success) result.Errors!.First().Should().Be("User already has the role");
        }
    }
}
