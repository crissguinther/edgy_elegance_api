using AutoMapper;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Application.Models.RequestModels;
using EdgyElegance.Application.Models.ResponseModels;
using EdgyElegance.Identity.Entities;
using System.Linq.Expressions;
using System.Transactions;

namespace EdgyElegance.Persistence.Services {
    public class UserService : IUserService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> AddToRoleByEmailAsync(string email, string role, bool saveOnSuccess = false) {
            UserResponse response = new();
            ApplicationUser? user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
            
            if (user is null) 
                response.Errors!.Add("User not found");

            response.MapIdentityResult(await _unitOfWork.UserRepository.AddToRoleAsync(user!, role));

            if (response.Success && saveOnSuccess)
                _unitOfWork.Commit();
            if (!response.Success)
                _unitOfWork.Rollback();

            return response;
        }

        public async Task<UserResponse> CreateUserAsync(CreateUserRequest userModel, string role) {
            using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            ApplicationUser user = _mapper.Map<ApplicationUser>(userModel);
            var response = new UserResponse(await _unitOfWork.UserRepository.CreateAsync(user, userModel.Password!));
            if (response.Success) {
                _unitOfWork.Commit();
                await this.AddToRoleByEmailAsync(userModel.Email!, role);
                ts.Complete();
            } else {
                _unitOfWork.Rollback();
                ts.Dispose();
            }

            return response;
        }

        public ApplicationUser? GetUser(Expression<Func<ApplicationUser, bool>> predicate) 
            => _unitOfWork.UserRepository.Get(predicate);

        public bool UserExists(Expression<Func<ApplicationUser, bool>> predicate)
            => _unitOfWork.UserRepository.UserExists(predicate);
    }
}
