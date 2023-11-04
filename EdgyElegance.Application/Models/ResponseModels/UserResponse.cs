using Microsoft.AspNetCore.Identity;

namespace EdgyElegance.Application.Models.ResponseModels {
    public class UserResponse {
        public bool Success { get; set; } = false;
        public List<string>? Errors { get; set; } = new List<string>();

        public UserResponse() { }

        public UserResponse(IdentityResult result) {
            this.MapIdentityResult(result);
        }

        public void MapIdentityResult(IdentityResult result) {
            Success = result.Succeeded;

            if (result.Errors is not null)
                Errors = result.Errors.Select(e => e.Description).ToList();
        }
    }
}
