using EdgyElegance.Application.Interfaces.Repositories;

namespace EdgyElegance.Application.Interfaces {
    public interface IUnitOfWork {
        IUserRepository UserRepository { get; }
        IAuthRepository AuthRepository { get; }

        /// <summary>
        /// Commit all the changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback the changes
        /// </summary>
        void Rollback();
    }
}
