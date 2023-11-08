using EdgyElegance.Application.Models.BaseModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EdgyElegance.Application.Models.ResponseModels {
    public class BadRequestResponse : BaseResponse {
        public BadRequestResponse() { }

        public BadRequestResponse(ModelStateDictionary modelState) {
            if (modelState.Any(e => e.Value?.Errors.Count > 0))
                Errors = modelState.Where(ms => ms.Value is not null && ms.Value.Errors.Any(e => !string.IsNullOrEmpty(e.ErrorMessage)))
                    .SelectMany(e => e.Value!.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();
        }
    }
}
