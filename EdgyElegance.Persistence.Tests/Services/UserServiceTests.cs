using AutoMapper;
using EdgyElegance.Application.Interfaces;
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
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ApplicationUser _request;
        private readonly UserModel _userModel;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserServiceTests() {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = 
                new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            _userRepository = new UserRepository(_userManagerMock.Object);
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository)
                .Returns(_userRepository);

            _userService = new UserService(_mapperMock.Object, _unitOfWorkMock.Object);

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

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.CreateAsync(It.IsAny<ApplicationUser>(), _userModel.Password!))
                .ReturnsAsync(IdentityResult.Success);

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.GetByEmailAsync(_userModel.Email!))
                .ReturnsAsync(_request);

            // Act
            _userService.CreateUserAsync(_userModel, "Admin");

            // Assert
            _mapperMock.Verify(mm => mm.Map<ApplicationUser>(_userModel), Times.Once);
            _unitOfWorkMock.Verify(uowm => uowm.UserRepository.CreateAsync(_request, _userModel.Password!), Times.Once);
            _unitOfWorkMock.Verify(uowm => uowm.Commit(), Times.Once);
            _unitOfWorkMock.Verify(uowm => uowm.Rollback(), Times.Never);
        }

        [Theory]
        [InlineData("Customer")]
        [InlineData("Admin")]
        public async void Should_Create_User_With_Specified_Role(string role) {
            // Arrange
            _mapperMock.Setup(m => m.Map<ApplicationUser>(_userModel))
                .Returns(_request);

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.CreateAsync(_request, _userModel.Password!))
                .ReturnsAsync(IdentityResult.Success);

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.GetByEmailAsync(_userModel.Email!))
                .ReturnsAsync(_request);

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.AddToRoleAsync(_request, role))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.CreateUserAsync(_userModel, role);

            // Assert
            result.Success = true;
            _unitOfWorkMock.Verify(uowm => uowm.Commit(), Times.Once);
            _unitOfWorkMock.Verify(uowm => uowm.Rollback(), Times.Never);
        }

        [Fact]
        public async void Should_Return_Errors_When_User_Is_Not_Created() {
            // Arrange
            _mapperMock.Setup(m => m.Map<ApplicationUser>(_userModel))
                .Returns(_request);

            _userManagerMock.Setup(umm => umm.CreateAsync(_request, _userModel.Password!))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = "User exits on database" }));

            // Act
            var result = await _userService.CreateUserAsync(_userModel, "Customer");

            // Assert
            result.Success = true;
            result.Errors.Should().NotBeNull();
            result.Errors!.First().Should().Be("User exits on database");
            _unitOfWorkMock.Verify(uowm => uowm.Commit(), Times.Never);
            _unitOfWorkMock.Verify(uowm => uowm.Rollback(), Times.Once);
        }

        [Theory]
        [InlineData("Customer", true)]
        [InlineData("Admin", false)]
        public async void Should_Add_To_Role_By_Looking_Through_Users_Email(string role, bool success) {
            // Arrange
            IdentityResult expectedResult = success
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError { Description = "User already has the role" });

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.GetByEmailAsync(_userModel.Email!))
                .ReturnsAsync(_request);

            _unitOfWorkMock.Setup(uowm => uowm.UserRepository.AddToRoleAsync(_request, role))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _userService.AddToRoleByEmailAsync(_userModel.Email!, role, true);

            // Assert
            result.Should().NotBeNull();
            _unitOfWorkMock.Verify(uowm => uowm.UserRepository.GetByEmailAsync(_userModel.Email!), Times.Once);
            result.Success.Should().Be(success);
            if (!success) {
                result.Errors.Should().NotBeNull();
                result.Errors!.First().Should().Be("User already has the role");
                _unitOfWorkMock.Verify(uowm => uowm.Rollback(), Times.Once);
            } else {
                _unitOfWorkMock.Verify(uowm => uowm.UserRepository.AddToRoleAsync(_request, role), Times.Once);
                _unitOfWorkMock.Verify(uowm => uowm.Commit(), Times.Once);
            }
        }
    }
}
