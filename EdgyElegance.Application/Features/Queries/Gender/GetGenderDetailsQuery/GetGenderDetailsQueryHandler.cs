using AutoMapper;
using EdgyElegance.Application.Exception;
using EdgyElegance.Application.Interfaces;
using MediatR;

namespace EdgyElegance.Application.Features.Queries.Gender.GetGenderDetailsQuery;

public class GetGenderDetailsQueryHandler : IRequestHandler<GetGenderDetailsQuery, GenderDetailsDto> {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGenderDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    async Task<GenderDetailsDto> IRequestHandler<GetGenderDetailsQuery, GenderDetailsDto>.Handle(GetGenderDetailsQuery request, CancellationToken cancellationToken) {
        var gender = await _unitOfWork.GenderRepository.FindByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Gender), request.Id);

        var dto = _mapper.Map<GenderDetailsDto>(gender);

        return dto;
    }
}
