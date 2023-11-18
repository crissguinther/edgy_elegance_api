using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Identity;
using EdgyElegance.Identity.Entities;
using EdgyElegance.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EdgyElegance.Persistence {
    public class UnitOfWork : IUnitOfWork {
        private readonly EdgyEleganceIdentityContext _identityContext;
        public IUserRepository UserRepository { get; private set; }
        public IAuthRepository AuthRepository { get; private set; }

        public UnitOfWork(EdgyEleganceIdentityContext identityContext, UserManager<ApplicationUser> userManager) {
            _identityContext = identityContext;

            UserRepository = new UserRepository(userManager);
            AuthRepository = new AuthRepository(_identityContext);
        }

        public void Commit() {
            _identityContext.SaveChanges();
        }

        public void Rollback() {
            // Code inspired by: https://www.c-sharpcorner.com/UploadFile/ff2f08/discard-changes-without-disposing-dbcontextobjectcontext-in/
            _identityContext.ChangeTracker.Entries().ToList().ForEach(e => {
                switch(e.State) {
                    case EntityState.Modified:
                        e.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        e.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        e.Reload();
                        break;
                    default: break;
                }
            });
        }
    }
}
