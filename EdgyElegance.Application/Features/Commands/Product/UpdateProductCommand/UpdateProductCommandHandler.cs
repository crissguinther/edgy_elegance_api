using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Product.UpdateProductCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        Domain.Entities.Product product = await _unitOfWork.ProductRepository.FindByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

        var validator = new UpdateProductCommandHandlerValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid) throw new BadRequestException(validation);

        product.Genders.Clear();
        product.Categories.Clear();

        product = _mapper.Map(request, product);

        List<Domain.Entities.Gender> genders = await _unitOfWork.GenderRepository
            .GetManyAsync(x => request.Genders.Contains(x.Id));
        List<Domain.Entities.Category> categories = await _unitOfWork.CategoryRepository
            .GetManyAsync(x => request.Categories.Contains(x.Id));

        genders.ForEach(g => product.Genders.Add(g));
        categories.ForEach(c => product.Categories.Add(c));

        _unitOfWork.ProductRepository.Update(product);
        _unitOfWork.Commit();

        return Unit.Value;
    }
}
