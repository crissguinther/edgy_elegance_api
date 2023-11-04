using EdgyElegance.Application.Models;
using EdgyElegance.Application.Models.ResponseModels;

namespace EdgyElegance.Application.Interfaces.Services {
    public interface IUserService {
        Task<UserResponse> CreateUserAsync(UserModel userModel);
        Task<UserResponse> AddToRoleByEmailAsync(string email, string role);
    }
}
