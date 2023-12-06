using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Product.CreateProductCommand;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int> {    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
        var validator = new CreateProductCommandValidator(_unitOfWork);
        var validation =  await validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid) throw new BadRequestException(validation);

        List<Domain.Entities.Category> categories = await _unitOfWork.CategoryRepository.GetManyAsync(x => request.Categories.Contains(x.Id));
        List<Domain.Entities.Gender> genders = await _unitOfWork.GenderRepository.GetManyAsync(x => request.Genders.Contains(x.Id));

        Domain.Entities.Product product = _mapper.Map<Domain.Entities.Product>(request);

        categories.ToList().ForEach(c => product.Categories.Add(c));
        genders.ToList().ForEach(g => product.Genders.Add(g));

        var result = await _unitOfWork.ProductRepository.AddAsync(product);

        _unitOfWork.Commit();

        return result.Id;        
    }
}
