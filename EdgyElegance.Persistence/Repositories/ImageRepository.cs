using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Domain.Entities;
using EdgyElegance.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace EdgyElegance.Persistence.Repositories;

internal class ImageRepository : IImageRepository {
    private readonly ApplicationContext _context;

    public ImageRepository(ApplicationContext context) {
        _context = context;
    }

    public Task<ProductImage?> GetProductImage(int id) {
        return _context.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<ProductImageThumbnail?> GetProductImageThumbnail(int id) {
        return _context.ProductImageThumbnails.FirstOrDefaultAsync(x => x.Id == id);
    }
}
