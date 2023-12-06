using EdgyElegance.Domain.Entities;

namespace EdgyElegance.Application.Contracts.Persistence;

public interface IImageRepository {
    Task<ProductImage?> GetProductImage(int id);
    Task<ProductImageThumbnail?> GetProductImageThumbnail(int id);
}
