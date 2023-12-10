using EdgyElegance.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EdgyElegance.Infrastructure;

public static class InfrastructureServicesInjector {
    public static void InjectInfrastructureServices(this IServiceCollection services) {
        services.AddScoped<IImageService, ImageService>();
    }
}
