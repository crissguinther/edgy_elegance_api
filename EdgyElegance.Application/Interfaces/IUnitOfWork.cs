using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Application.Interfaces.Repositories;

namespace EdgyElegance.Application.Interfaces {
    public interface IUnitOfWork {
        IUserRepository UserRepository { get; }
        IAuthRepository AuthRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IGenderRepository GenderRepository { get; }
        IProductRepository ProductRepository { get; }
        IImageRepository ImageRepository { get; }

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
