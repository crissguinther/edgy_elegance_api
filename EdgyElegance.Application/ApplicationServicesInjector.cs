using Microsoft.Extensions.DependencyInjection;

namespace EdgyElegance.Application;

public static class ApplicationServicesInjector {
    public static void InjectApplicationServices(this IServiceCollection collection) {
        collection.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationServicesInjector).Assembly));
    }
}
