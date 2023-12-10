using FluentValidation.Results;

namespace EdgyElegance.Application.Exception;
public class BadRequestException :  System.Exception {
    public IDictionary<string, string[]> ValidationErrors { get; set; } = new Dictionary<string, string[]>();

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(ValidationResult validationResult) : base("One or more validation errors ocurred") {
        ValidationErrors = validationResult.ToDictionary();
    }
}
