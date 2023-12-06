using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Features.Commands.Image.UpdateProductImagesCommand;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Tests.Mocks;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Text;

namespace EdgyElegance.Application.Tests.FeaturesTests.CommandsTests.ImageTests.UpdateProductImagesCommandTest;

public class UpdateProductImagesCommandTest {
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IImageService> _imageServiceMock;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    private readonly UpdateProductImagesCommandHandler _handler;

    public UpdateProductImagesCommandTest() {
        _unitOfWorkMock = IUnitOfWorkMock.GetMock();
        _imageServiceMock = new();
        _unitOfWork = _unitOfWorkMock.Object;
        _imageService = _imageServiceMock.Object;
        _handler = new UpdateProductImagesCommandHandler(_unitOfWork, _imageService);
    }

    [Fact]
    public async Task Handle_WithNonExistingProduct_ShouldRaiseNotFoundExceptionAsync() {
        // Arrange
        byte[] bytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
        using var stream = new MemoryStream(bytes);
        var image = new FormFile(stream, 0, bytes.Length, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()) {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };
        var command = new UpdateProductImagesCommand {
            ProductId = 1,
            Images = new List<IFormFile> { image }
        };

        _unitOfWorkMock.Setup(x => x.ProductRepository.FindByIdAsync(command.ProductId))
            .ReturnsAsync((Product)null!);

        // Act & Assert
        await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async void Handle_WithNonImagePosted_ShouldRaiseBadRequestException() {
        // Arrange
        byte[] bytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
        using var stream = new MemoryStream(bytes);
        var image = new FormFile(stream, 0, bytes.Length, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()) {
            Headers = new HeaderDictionary(),
            ContentType = Guid.NewGuid().ToString()
        };
        var command = new UpdateProductImagesCommand {
            ProductId = 1,
            Images = new List<IFormFile> { image }
        };

        _unitOfWorkMock.Setup(x => x.ProductRepository.FindByIdAsync(command.ProductId, It.IsAny<Expression<Func<Domain.Entities.Product, object>>[]>()))
            .ReturnsAsync(new Product { Id = 1 });

        // Act & Assert
       await _handler.Invoking(x => x.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async void Handle_WithValidImages_ShouldClearOldImagesAndStoreNewOnes() {
        // Arrange
        byte[] bytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
        using var stream = new MemoryStream(bytes);
        var image = new FormFile(stream, 0, bytes.Length, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()) {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };
        var command = new UpdateProductImagesCommand {
            ProductId = 1,
            Images = new List<IFormFile> { image }
        };
        var product = new Product { Images = new List<ProductImage> {
            new ProductImage{ 
                Path = Guid.NewGuid().ToString(),
                Thumbnail = new ProductImageThumbnail { Path = Guid.NewGuid().ToString() }
            }
        }, Id = 1 };

        _unitOfWorkMock.Setup(x => x.ProductRepository.FindByIdAsync(command.ProductId, It.IsAny<Expression<Func<Domain.Entities.Product, object>>[]>()))
            .ReturnsAsync(product);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _imageServiceMock.Verify(x => x.DeleteImages(product.Images), Times.AtLeastOnce());
        _imageServiceMock.Verify(x => x.StoreFileImage(It.IsAny<IFormFile>()), Times.AtLeastOnce());
        _unitOfWorkMock.Verify(x => x.ProductRepository.Update(product), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }
}
