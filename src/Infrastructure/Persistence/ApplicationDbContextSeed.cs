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

            var administrator = new ApplicationUser { UserName = "administrator" , IsActive=true,Site="Razor",DisplayName="Administrator", Email = "new163@163.com" , EmailConfirmed=true};
            var demo = new ApplicationUser { UserName = "Demo", IsActive = true, Site = "Razor", DisplayName = "Demo", Email = "neozhu@126.com", EmailConfirmed = true };

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
            //Seed, if necessary
            if (!context.DocumentTypes.Any())
                {
                context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "Document", Description = "Document" });
                context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "PDF", Description = "PDF" });
                context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "Image", Description = "Image" });
                context.DocumentTypes.Add(new Domain.Entities.DocumentType() { Name = "Other", Description = "Other" });
                await context.SaveChangesAsync();
                }
            if (!context.KeyValues.Any())
            {
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value= "initialization",Text= "initialization", Description = "Status of workflow" });
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value = "processing", Text = "processing", Description = "Status of workflow"  });
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value = "pending", Text = "pending", Description = "Status of workflow"  });
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Status", Value = "finished", Text = "finished", Description = "Status of workflow"  });
                await context.SaveChangesAsync();
            }
        }
    }
}
