using MediatR;

namespace EdgyElegance.Application.Features.Queries.Gender.GetGenderDetailsQuery;

public record GetGenderDetailsQuery(int Id) : IRequest<GenderDetailsDto> ;
