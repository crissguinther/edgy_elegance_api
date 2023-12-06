using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Features.Commands.Gender.Commands.CreateGenderCommand;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Tests.Mocks;
using EdgyElegance.Domain.Entities;

namespace EdgyElegance.Application.Tests.FeaturesTests.CommandsTests.GenderTests.CreateGenderCommandTests;

public class CreateGenderCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateGenderCommandHandler _handler;

    public CreateGenderCommandHandlerTests()
    {
        _unitOfWorkMock = IUnitOfWorkMock.GetMock();

        _mapper = new Mock<IMapper>().Object;
        _unitOfWork = _unitOfWorkMock.Object;

        _handler = new CreateGenderCommandHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async void Handle_WithNullOrEmpty_ShouldRaiseBadRequestException()
    {
        // Arrange
        var command = new CreateGenderCommand { Name = "" };

        // Act
        async Task act() => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(act);
    }

    [Fact]
    public async void Handle_WithNonExistingGender_ShouldCallUnitOfWorkCommit()
    {
        // Arrange
        var genderName = Guid.NewGuid().ToString();
        var command = new CreateGenderCommand { Name = genderName };

        _unitOfWorkMock.Setup(uowm => uowm.GenderRepository.AddAsync(It.IsAny<Gender>()))
            .ReturnsAsync(new Gender { Name = genderName, Id = 1 });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(m => m.Commit(), Times.Once);
        result.Should().BeOfType(typeof(int));
    }

    [Fact]
    public async void Handle_WithExistingGender_ShouldRaiseBadRequestException()
    {
        // Arrange
        var genderName = Guid.NewGuid().ToString();
        var command = new CreateGenderCommand { Name = genderName };

        _unitOfWorkMock.Setup(x => x.GenderRepository.ExistsAsync(genderName))
            .ReturnsAsync(true);

        // Act 
        async Task act() => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(act);
    }
}
