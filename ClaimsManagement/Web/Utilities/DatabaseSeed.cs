using Data;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Web.Utilities
{
    public static class DatabaseSeed
    {
        public static async Task Plant(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var dbContext = scope.ServiceProvider.GetService<ClaimsDbContext>();

                if (!await roleManager.RoleExistsAsync("Administrator"))
                {
                    var adminRole = new IdentityRole("Administrator");
                    await roleManager.CreateAsync(adminRole);
                    var memberRole = new IdentityRole("Member");
                    await roleManager.CreateAsync(memberRole);

                    var vladi = new User
                    {
                        Email = "vladivital@abv.bg",
                        UserName = "vladivital@abv.bg"
                    };
                    await userManager.CreateAsync(vladi, "vladivital");
                    await userManager.AddToRoleAsync(vladi, "Member");
                    await userManager.AddToRoleAsync(vladi, "Administrator");
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
