using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Features.Commands.Gender.Commands.DeleteGenderCommand;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Tests.Mocks;
using EdgyElegance.Domain.Entities;
using MediatR;

namespace EdgyElegance.Application.Tests.FeaturesTests.CommandsTests.DeleteGenderCommandTests;

public class DeleteGenderCommandHandlerTests {
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = IUnitOfWorkMock.GetMock();

    private readonly DeleteGenderCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGenderCommandHandlerTests() {
        _unitOfWork = _unitOfWorkMock.Object;
        _handler = new DeleteGenderCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handle_WithNonExistingGender_ShouldRaiseNotFoundExceptionAsync() {
        // Arrange
        var command = new DeleteGenderCommand(1);

        _unitOfWorkMock.Setup(x => x.GenderRepository.FindByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Gender?) null);

        // Act & Assert
        await _handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WithExistingGender_ShouldDeleteAndPersistChanges() {
        // Arrange
        var command = new DeleteGenderCommand(1);
        var gender = new Gender { Name = Guid.NewGuid().ToString(), Id = 1 };

        _unitOfWorkMock.Setup(x => x.GenderRepository.FindByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(gender);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(x => x.GenderRepository.Delete(gender), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);

        result.Should().BeOfType<Unit>();
        result.Should().Be(Unit.Value);
    }
}
