using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.UpdateGenderCommand;

public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, Unit> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateGenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateGenderCommand request, CancellationToken cancellationToken) {
        var gender = await _unitOfWork.GenderRepository.FindByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Gender), request.Id);

        var validator = new UpdateGenderCommandValidator(_unitOfWork);
        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid) throw new BadRequestException(validation);

        gender = _mapper.Map(request, gender);

        _unitOfWork.GenderRepository.Update(gender);
        _unitOfWork.Commit();

        return Unit.Value;
    }
}
