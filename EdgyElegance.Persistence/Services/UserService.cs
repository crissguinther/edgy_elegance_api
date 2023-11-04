using AutoMapper;
using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models;
using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;

namespace EdgyElegance.Persistence.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository) {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserResponse> AddToRoleByEmailAsync(string email, string role) {
            UserResponse response = new();
            ApplicationUser? user = await _userRepository.GetByEmailAsync(email);
            
            if (user is null) {
                response.Errors!.Add("User not found");
                return response;
            }

            response.MapIdentityResult(await _userRepository.AddToRoleAsync(user, role));

            return response;
        }

        public async Task<UserResponse> CreateUserAsync(UserModel userModel) {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userModel);
            return new UserResponse(await _userRepository.CreateAsync(user, userModel.Password!));
        }
    }
}
