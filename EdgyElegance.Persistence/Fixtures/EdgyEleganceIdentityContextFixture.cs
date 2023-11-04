using EdgyElegance.Identity;
using Microsoft.EntityFrameworkCore;

namespace EdgyElegance.Persistence.Fixtures {
    public class EdgyEleganceIdentityContextFixture : IDisposable {
        private readonly EdgyEleganceIdentityContext _context;

        public EdgyEleganceIdentityContextFixture() {
            DbContextOptionsBuilder<EdgyEleganceIdentityContext> optionsBuilder
                = new();

            optionsBuilder.UseInMemoryDatabase("IdentityContextFixture");

            _context = new EdgyEleganceIdentityContext(optionsBuilder.Options);
        }

        public EdgyEleganceIdentityContext GetFixture() => _context;

        public void Dispose() {
            _context.Dispose();
        }
    }
}
