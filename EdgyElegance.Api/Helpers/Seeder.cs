using EdgyElegance.Identity;
using EdgyElegance.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace EdgyElegance.Api.Helpers {
    public static class Seeder {
        public static async void Seed(this WebApplicationBuilder builder) {
            ServiceProvider sp = builder.Services.BuildServiceProvider();

            EdgyEleganceIdentityContext context = sp.GetRequiredService<EdgyEleganceIdentityContext>();
            UserManager<ApplicationUser> userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();

            if (Assembly.GetEntryAssembly() == Assembly.GetExecutingAssembly()) {
                if (!context.Roles.Any()) {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                    context.SaveChanges();
                }

                if (!context.Users.Any()) {
                    var defaultUser = new ApplicationUser {
                        FirstName = builder.Configuration["DefaultUser:FirstName"]!,
                        LastName = builder.Configuration["DefaultUser:LastName"]!,
                        Email = builder.Configuration["DefaultUser:Email"],
                        UserName = builder.Configuration["DefaultUser:Email"]
                    };

                    await userManager.CreateAsync(defaultUser, builder.Configuration["DefultUser:Password"]!);
                    context.SaveChanges();

                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                    context.SaveChanges();
                }
            }
        }
    }
}

