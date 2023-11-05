using AutoMapper;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models;
using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;

namespace EdgyElegance.Persistence.Services {
    public class UserService : IUserService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> AddToRoleByEmailAsync(string email, string role) {
            UserResponse response = new();
            ApplicationUser? user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
            
            if (user is null) 
                response.Errors!.Add("User not found");

            response.MapIdentityResult(await _unitOfWork.UserRepository.AddToRoleAsync(user!, role));

            if (response.Success)
                _unitOfWork.Commit();
            else 
                _unitOfWork.Rollback();

            return response;
        }

        public async Task<UserResponse> CreateUserAsync(UserModel userModel) {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userModel);
            var response = new UserResponse(await _unitOfWork.UserRepository.CreateAsync(user, userModel.Password!));
            if (response.Success) _unitOfWork.Commit();
            else _unitOfWork.Rollback();

            return response;
        }
    }
}
