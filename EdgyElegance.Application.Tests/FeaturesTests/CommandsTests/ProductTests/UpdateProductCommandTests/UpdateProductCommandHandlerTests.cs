using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Features.Commands.Product.UpdateProductCommand;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Tests.Mocks;
using EdgyElegance.Domain.Entities;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Tests.FeaturesTests.CommandsTests.ProductTests.UpdateProductCommandTests;

public class UpdateProductCommandHandlerTests {
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    private readonly UpdateProductCommandHandler _handler;
    

    public UpdateProductCommandHandlerTests() {
        _unitOfWorkMock = IUnitOfWorkMock.GetMock();
        _mapperMock = new();

        _unitOfWork = _unitOfWorkMock.Object;
        _mapper = _mapperMock.Object;

        _handler = new UpdateProductCommandHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_WithNonExistingProduct_ShouldThrowNotFoundException() {
        // Arrange
        var command = new UpdateProductCommand {
            Id = 1,
            Name = Guid.NewGuid().ToString(),
            Price = 10.00m
        };

        _unitOfWorkMock.Setup(x => x.ProductRepository.FindByIdAsync(command.Id))
            .ReturnsAsync((Product) null!);

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WithEmptyOrNullName_ShouldThrowBadRequestException() {
        // Arrange
        var command = new UpdateProductCommand {
            Id = 1,
            Name = string.Empty,
            Price = 10.00m
        };

        _unitOfWorkMock.Setup(x => x.ProductRepository.FindByIdAsync(command.Id))
            .ReturnsAsync(new Product { Id = 1, Name = Guid.NewGuid().ToString(), Price = 10.00m});

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldMapAndStoreChanges() {
        // Arrange
        var newName = Guid.NewGuid().ToString();
        var command = new UpdateProductCommand {
            Id = 1,
            Name = Guid.NewGuid().ToString(),
            Price = 20.00m,
            Categories = new List<int> { 1 },
            Genders = new List<int> { 1 }
        };
        var product = new Product {
            Id = command.Id, Name = command.Name, Price = command.Price, Genders = new List<Gender>(), Categories = new List<Category>()
        };

        _mapperMock.Setup(x => x.Map(It.IsAny<UpdateProductCommand>(), It.IsAny<Product>())).Returns(product);

        _unitOfWorkMock.Setup(x => x.ProductRepository.FindByIdAsync(command.Id))
            .ReturnsAsync(new Product { Id = command.Id, Name = newName, Price = 15.00m });

        _unitOfWorkMock.Setup(x => x.GenderRepository.GetManyAsync(It.IsAny<Expression<Func<Domain.Entities.Gender, bool>>>()))
            .ReturnsAsync(new List<Gender> { new Gender { Id = 1 } });

        _unitOfWorkMock.Setup(x => x.CategoryRepository.GetManyAsync(It.IsAny<Expression<Func<Domain.Entities.Category, bool>>>()))
            .ReturnsAsync(new List<Category> { new Category { Id = 1 } });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        _unitOfWorkMock.Verify(x => x.ProductRepository.Update(product), Times.Once);
        _unitOfWorkMock.Verify(x => x.GenderRepository.GetManyAsync(It.IsAny<Expression<Func<Domain.Entities.Gender, bool>>>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CategoryRepository.GetManyAsync(It.IsAny<Expression<Func<Domain.Entities.Category, bool>>>()), Times.Once);
        _mapperMock.Verify(x => x.Map(It.IsAny<UpdateProductCommand>(), It.IsAny<Product>()), Times.Once);
    }
}
