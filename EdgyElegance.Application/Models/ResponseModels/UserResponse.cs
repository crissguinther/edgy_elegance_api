using EdgyElegance.Application.Models.BaseModels;
using Microsoft.AspNetCore.Identity;

namespace EdgyElegance.Application.Models.ResponseModels {
    public class UserResponse : BaseResponse {
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
