using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EdgyElegance.Identity {
    public class EdgyEleganceIdentityContext : IdentityDbContext {
        public EdgyEleganceIdentityContext(DbContextOptions<EdgyEleganceIdentityContext> options) : base(options) { }
    }
}
