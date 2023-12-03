using FluentValidation.Results;

namespace EdgyElegance.Application.Exception;
public class BadRequestException :  System.Exception {
    public IEnumerable<string>? ValidationErrors { get; set; }

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(ValidationResult validationResult) : base("One or more validation errors ocurred") {
        ValidationErrors = validationResult.Errors
            .Where(e => !string.IsNullOrEmpty(e.ErrorMessage))
            .Select(e => e.ErrorMessage).ToList();
    }
}
