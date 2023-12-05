using MediatR;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.DeleteGenderCommand;

public record DeleteGenderCommand(int Id) : IRequest<Unit>;
