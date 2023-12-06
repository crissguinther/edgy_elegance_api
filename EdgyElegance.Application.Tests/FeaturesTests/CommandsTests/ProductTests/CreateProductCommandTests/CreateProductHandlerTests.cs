using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Features.Commands.Product.CreateProductCommand;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Tests.Mocks;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Infrastructure.Services;
using System.Linq.Expressions;

namespace EdgyElegance.Application.Tests.FeaturesTests.CommandsTests.ProductTests.CreateProductCommandTests;

public class CreateProductHandlerTests {
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IImageService> _imageServiceMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    private readonly CreateProductCommandHandler _handler;

    public CreateProductHandlerTests() {
        _unitOfWorkMock = IUnitOfWorkMock.GetMock();
        _imageServiceMock = new();
        _mapperMock = new();

        _unitOfWork = _unitOfWorkMock.Object;
        _imageService = _imageServiceMock.Object;
        _mapper = _mapperMock.Object;

        _handler = new CreateProductCommandHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async void Handle_WithNullOrEmptyName_ShouldRaiseBadRequestException() {
        // Arrange
        var command = new CreateProductCommand { Name = "", Categories = new List<int> { 1 } };

        _unitOfWorkMock.Setup(x => x.GenderRepository.GetManyAsync(It.IsAny<Expression<Func<Gender, bool>>>()))
           .ReturnsAsync(new List<Gender> { new Gender { Id = 1 }, new Gender { Id = 2 } });

        _unitOfWorkMock.Setup(x => x.CategoryRepository.GetManyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(new List<Category> { new Category { Id = 1 }, new Category { Id = 2 } });

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async void Handle_WithProductAlreadyStored_ShouldRaiseBadRequestException() {
        // Arrange
        var productName = Guid.NewGuid().ToString();
        var command = new CreateProductCommand { Name = productName };

        _unitOfWorkMock.Setup(x => x.GenderRepository.GetManyAsync(It.IsAny<Expression<Func<Gender, bool>>>()))
           .ReturnsAsync(new List<Gender> { new Gender { Id = 1 }, new Gender { Id = 2 } });

        _unitOfWorkMock.Setup(x => x.CategoryRepository.GetManyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(new List<Category> { new Category { Id = 1 }, new Category { Id = 2 } });

        _unitOfWorkMock.Setup(m => m.ProductRepository.ExistsAsync(productName))
            .ReturnsAsync(true);

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async void Handle_WithInvalidCategories_ShouldRaiseBadRequestException() {
        // Arrange
        var productName = Guid.NewGuid().ToString();
        var command = new CreateProductCommand { Name = productName, Categories = new List<int> { 1, 2 } };

        _unitOfWorkMock.Setup(x => x.GenderRepository.GetManyAsync(It.IsAny<Expression<Func<Gender, bool>>>()))
           .ReturnsAsync(new List<Gender> { new Gender { Id = 1 }, new Gender { Id = 2 } });

        _unitOfWorkMock.Setup(x => x.CategoryRepository.GetManyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(new List<Category> { new Category { Id = 1 } });

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async void Handle_WithInvalidGenders_ShouldRaiseBadRequestException() {
        // Arrange
        var productName = Guid.NewGuid().ToString();
        var command = new CreateProductCommand { Name = productName, Categories = new List<int> { 1, 2 }, Genders = new List<int> { 1, 2 } };

        _unitOfWorkMock.Setup(x => x.GenderRepository.GetManyAsync(It.IsAny<Expression<Func<Gender, bool>>>()))
            .ReturnsAsync(new List<Gender> { new Gender { Id = 2 } });

        _unitOfWorkMock.Setup(x => x.CategoryRepository.GetManyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(new List<Category> { new Category { Id = 1 }, new Category { Id = 2 } });

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async void Handle_WithValidInformation_ShouldPersistChangesAndStoreImages() {
        // Arrange
        var productName = Guid.NewGuid().ToString();
        var command = new CreateProductCommand {
            Name = productName,
            Categories = new List<int> { 1, 2 },
            Genders = new List<int> { 1, 2 },
            Price = 10.00m
        };
        var product = new Product {
            Name = command.Name
        };

        _unitOfWorkMock.Setup(x => x.GenderRepository.GetManyAsync(It.IsAny<Expression<Func<Gender, bool>>>()))
           .ReturnsAsync(new List<Gender> { new Gender { Id = 1 }, new Gender { Id = 2 } });

        _unitOfWorkMock.Setup(x => x.CategoryRepository.GetManyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(new List<Category> { new Category { Id = 1 }, new Category { Id = 2 } });

        _unitOfWorkMock.Setup(m => m.ProductRepository.ExistsAsync(productName))
            .ReturnsAsync(false);

        _unitOfWorkMock.Setup(m => m.ProductRepository.AddAsync(product))
            .ReturnsAsync(new Product {
                Id = 1,
                Name = command.Name
            });

        _mapperMock.Setup(m => m.Map<Domain.Entities.Product>(command))
            .Returns(product);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(x => x.Map<Product>(command), Times.Once);
        _unitOfWorkMock.Verify(x => x.ProductRepository.AddAsync(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }
}
