using EdgyElegance.Api.Middlewares;
using EdgyElegance.Application.Interfaces;
using EdgyElegance.Application.Interfaces.Repositories;
using EdgyElegance.Application.Interfaces.Services;
using EdgyElegance.Identity;
using EdgyElegance.Identity.Classes;
using EdgyElegance.Identity.Entities;
using EdgyElegance.Persistence;
using EdgyElegance.Persistence.Repositories;
using EdgyElegance.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EdgyElegance.Api.Helpers {
    public static class ServicesInjector {
        public static void InjectServices(this WebApplicationBuilder builder) {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<EdgyEleganceIdentityContext>(options =>
                options.UseSqlServer(builder.Configuration["Security:ConnectionString"])
            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                options.User.RequireUniqueEmail = true
            ).AddEntityFrameworkStores<EdgyEleganceIdentityContext>()
            .AddDefaultTokenProviders();

            // Sets the jwt secret as a environment variable, so it can be used in another parts of the code
            string jwtSecurityKey = builder.Configuration["Security:JwtSecret"]!;
            Environment.SetEnvironmentVariable("JwtSecret", jwtSecurityKey);

            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = false,
                        ValidIssuer = builder.Configuration["Security:ValidIssuer"],
                        ValidateAudience = false, // Change on production
                        ValidAudience = builder.Configuration["Security:ValidAudience"],
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey))
                    };
                });

            builder.Services.AddTransient<ExceptionHandlerMiddleware>();
            builder.Services.AddScoped<IUserStore<ApplicationUser>, ApplicationUserStore>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAuthRepository, AuthRepository>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<UserManager<ApplicationUser>>();
        }
    }
}
