using MediatR;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.CreateGenderCommand;

public class CreateGenderCommand : IRequest<int> {
    public string Name { get; set; } = string.Empty;
}
