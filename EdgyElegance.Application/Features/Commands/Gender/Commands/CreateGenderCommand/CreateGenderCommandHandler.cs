using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.CreateGenderCommand;

public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, int> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateGenderCommand request, CancellationToken cancellationToken) {
        var validator = new CreateGenderCommandHandlerValidator(_unitOfWork);
        var validation = await validator.ValidateAsync(request);

        if (!validation.IsValid) throw new BadRequestException(validation);

        var gender = _mapper.Map<Domain.Entities.Gender>(request);
        var response = await _unitOfWork.GenderRepository.AddAsync(gender);
        _unitOfWork.Commit();

        return response.Id;
    }
}
