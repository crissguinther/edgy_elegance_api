using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Queries.Images.GetProductImageQuery;

public class GetProductThumbnailImageQueryHandler : IRequestHandler<GetProductThumbnailImageQuery, Stream> {
    private readonly IUnitOfWork _unitOfWork;

    public GetProductThumbnailImageQueryHandler(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<Stream> Handle(GetProductThumbnailImageQuery request, CancellationToken cancellationToken) {
        var image = await _unitOfWork.ImageRepository.GetProductImage(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.ProductImage), request.Id);

        byte[] bytes = File.ReadAllBytes(image.Path);
        var stream = new MemoryStream(bytes);
        return stream;
    }
}
