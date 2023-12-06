using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EdgyElegance.Application.Features.Commands.Image.UpdateProductImagesCommand;

public class UpdateProductImagesCommandHandler : IRequestHandler<UpdateProductImagesCommand, Unit> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public UpdateProductImagesCommandHandler(IUnitOfWork unitOfWork, IImageService imageService) {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<Unit> Handle(UpdateProductImagesCommand request, CancellationToken cancellationToken) {
        var product = await _unitOfWork.ProductRepository.FindByIdAsync(
            request.ProductId, 
            x => x.Images,
            x => (x.Images as ProductImage).Thumbnail
        ) ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.ProductId);

        var validator = new UpdateProductImagesCommandHandlerValidator();

        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid) throw new BadRequestException(validation);

        List<string> imagePaths = new();
        List<ProductImage> images = new();

        try {
            if (product.Images.Count > 0) {
                IEnumerable<ProductImageThumbnail?> thumbnails = product.Images.Select(i => i.Thumbnail);
                IEnumerable<ProductImage?> productImages = product.Images;

                if (thumbnails?.Count() > 0) _imageService.DeleteImages(thumbnails!);
                if (productImages?.Count() > 0) _imageService.DeleteImages(productImages!);

                product.Images.Clear();
            };

            foreach(IFormFile file in request.Images) {
                string imagePath = _imageService.StoreFileImage(file);
                string thumbnailPath = _imageService.CreateThumbnail(file);

                imagePaths.Add(imagePath);
                imagePaths.Add(thumbnailPath);

                images.Add(new ProductImage {
                    Product = product,
                    ProductId = product.Id,
                    Path = imagePath,
                    Thumbnail = new ProductImageThumbnail {
                        Path = thumbnailPath
                    }
                });
            }

            images.ForEach(i => product.Images.Add(i));
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return Unit.Value;
        } catch {
            throw new System.Exception("Error while updating the entry");
        }
    }
}
