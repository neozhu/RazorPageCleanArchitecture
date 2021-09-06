using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Infrastructure.Constants.ClaimTypes;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using CleanArchitecture.Razor.Infrastructure.Identity;
using CleanArchitecture.Razor.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var administratorRole = new ApplicationRole("Admin") { Description= "Admin Group" };
            var userRole = new ApplicationRole("Basic") { Description = "Basic Group" };

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
                await roleManager.CreateAsync(userRole);
                var Permissions = GetAllPermissions();
                foreach(var permission in Permissions)
                {
                    await roleManager.AddClaimAsync(administratorRole, new System.Security.Claims.Claim(ApplicationClaimTypes.Permission, permission));
                }
            }

            var administrator = new ApplicationUser { UserName = "administrator" , IsActive=true,Site="Razor",DisplayName="Administrator", Email = "new163@163.com" , EmailConfirmed=true, ProfilePictureDataUrl=$"https://cdn.v2ex.com/gravatar/{"new163@163.com".ToMD5()}?s=120&d=retro" };
            var demo = new ApplicationUser { UserName = "Demo", IsActive = true, Site = "Razor", DisplayName = "Demo", Email = "neozhu@126.com", EmailConfirmed = true, ProfilePictureDataUrl = $"https://cdn.v2ex.com/gravatar/{"neozhu@126.com".ToMD5()}?s=120&d=retro" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Password123!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                await userManager.CreateAsync(demo, "Password123!");
                await userManager.AddToRolesAsync(demo, new[] { userRole.Name });
            }

        }
        private static IEnumerable<string> GetAllPermissions()
        {
            var allPermissions = new List<string>();
            var modules = typeof(Permissions).GetNestedTypes();

            foreach (var module in modules)
            {
                var moduleName = string.Empty;
                var moduleDescription = string.Empty;

                var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                foreach (var fi in fields)
                {
                    var propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                        allPermissions.Add( propertyValue.ToString() );
                }
            }

            return allPermissions;
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
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Region", Value = "CNC", Text = "CNC", Description = "Region of Customer" });
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Region", Value = "CNN", Text = "CNN", Description = "Region of Customer" });
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Region", Value = "CNS", Text = "CNS", Description = "Region of Customer" });
                context.KeyValues.Add(new Domain.Entities.KeyValue() { Name = "Region", Value = "Oversea", Text = "Oversea", Description = "Region of Customer" });
                await context.SaveChangesAsync();
            }
            if (!context.Customers.Any())
            {
                context.Customers.Add(new Domain.Entities.Customer() { Name = "上海电气工业", Address= "上海浦东", Sales= "Sales",Region= "CNC",   PartnerType= Domain.Enums.PartnerType.Customer,Contract= "采购联系人", Email= "采购电子邮件" });
                await context.SaveChangesAsync();
            }
        }
    }
}
