using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Features.Commands.Gender.Commands.UpdateGenderCommand;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Tests.Mocks;
using EdgyElegance.Domain.Entities;

namespace EdgyElegance.Application.Tests.FeaturesTests.CommandsTests.UpdateGenderCommandTests;

public class UpdateGenderCommandTests {
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly UpdateGenderCommandHandler _handler;

    public UpdateGenderCommandTests() {
        _unitOfWorkMock = IUnitOfWorkMock.GetMock();
        _mapperMock = new Mock<IMapper>();

        _unitOfWork = _unitOfWorkMock.Object;
        _mapper = _mapperMock.Object;

        _handler = new UpdateGenderCommandHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_WithNonExistingCategory_ShouldRaiseNotFoundExceptionAsync() {
        // Arrange
        var command = new UpdateGenderCommand();

        _unitOfWorkMock.Setup(x => x.GenderRepository.FindByIdAsync(1))
            .ReturnsAsync((Gender?) null);

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WithEmptyGenderName_ShouldRaiseBadExceptionAsync() {
        // Arrange
        var command = new UpdateGenderCommand { Id = 1, Name = ""};
        var gender = new Gender { Id = 1, Name = Guid.NewGuid().ToString() };

        _unitOfWorkMock.Setup(x => x.GenderRepository.FindByIdAsync(1))
            .ReturnsAsync(gender);

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Handle_WithExistingCategoryName_ShouldRaiseBadExceptionAsync() {
        // Arrange
        var genderName = Guid.NewGuid().ToString();
        var command = new UpdateGenderCommand { Id = 1, Name = genderName };
        var gender = new Gender { Id = command.Id, Name = command.Name };

        _unitOfWorkMock.Setup(x => x.GenderRepository.FindByIdAsync(command.Id))
            .ReturnsAsync(gender);

        _unitOfWorkMock.Setup(x => x.GenderRepository.ExistsAsync(genderName))
            .ReturnsAsync(true);

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldProperlyCallDatabase() {
        // Arrange
        var genderName = Guid.NewGuid().ToString();
        var command = new UpdateGenderCommand { Id = 1, Name = "NewGender" };
        var gender = new Gender { Id = command.Id, Name = genderName };

        _unitOfWorkMock.Setup(x => x.GenderRepository.FindByIdAsync(command.Id))
            .ReturnsAsync(gender);

        _unitOfWorkMock.Setup(x => x.GenderRepository.ExistsAsync(genderName))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(x => x.Map(command, gender), Times.Once);
        _unitOfWorkMock.Verify(x => x.GenderRepository.Update(It.IsAny<Gender>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }
}
