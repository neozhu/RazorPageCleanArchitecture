using CleanArchitecture.Razor.Infrastructure.Identity;
using CleanArchitecture.Razor.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var administratorRole = new ApplicationRole("Administrator") { Description= "Administrator" };
            var userRole = new ApplicationRole("Users") { Description = "Users" };

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
                await roleManager.CreateAsync(userRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator" , IsActive=true,Site="Razor",DisplayName="Administrator", Email = "administrator@localhost" , EmailConfirmed=true};
            var demo = new ApplicationUser { UserName = "Demo", IsActive = true, Site = "Razor", DisplayName = "Demo", Email = "demo@localhost", EmailConfirmed = true };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Password123!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                await userManager.CreateAsync(demo, "Password123!");
                await userManager.AddToRolesAsync(demo, new[] { userRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            //if (!context.TodoLists.Any())
            //{
            //    context.TodoLists.Add(new TodoList
            //    {
            //        Title = "Shopping",
            //        Colour = Colour.Blue,
            //        Items =
            //        {
            //            new TodoItem { Title = "Apples", Done = true },
            //            new TodoItem { Title = "Milk", Done = true },
            //            new TodoItem { Title = "Bread", Done = true },
            //            new TodoItem { Title = "Toilet paper" },
            //            new TodoItem { Title = "Pasta" },
            //            new TodoItem { Title = "Tissues" },
            //            new TodoItem { Title = "Tuna" },
            //            new TodoItem { Title = "Water" }
            //        }
            //    });

            //    await context.SaveChangesAsync();
            //}
        }
    }
}
