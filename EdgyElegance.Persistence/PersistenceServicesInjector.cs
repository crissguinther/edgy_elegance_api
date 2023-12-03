using EdgyElegance.Application.Constants;
using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Persistence.DatabaseContexts;
using EdgyElegance.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EdgyElegance.Persistence;

public static class PersistenceServicesInjector {
    public static void InjectPersistenceServices(this IServiceCollection collection) {
        string connectionString = ApplicationConstants.CONNECTION_STRING;

        if (string.IsNullOrEmpty(connectionString)) throw new Exception("Connection string is not defined");

        collection.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

        collection.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
