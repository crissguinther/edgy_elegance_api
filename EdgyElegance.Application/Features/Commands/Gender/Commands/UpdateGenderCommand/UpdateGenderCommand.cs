using MediatR;

namespace EdgyElegance.Application.Features.Commands.Gender.Commands.UpdateGenderCommand;
public class UpdateGenderCommand : IRequest<Unit> {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
