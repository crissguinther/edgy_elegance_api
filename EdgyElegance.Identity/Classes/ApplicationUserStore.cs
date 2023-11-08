using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EdgyElegance.Identity.Classes {
    public class ApplicationUserStore : UserStore<ApplicationUser> {
        public ApplicationUserStore(EdgyEleganceIdentityContext context) : base (context) {
            AutoSaveChanges = false; // Stores the users only when SaveDbChanges is called
        }
    }
}
