using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Models;

namespace SmartAdmin.WebUI.Extensions
{
    public static class IdentityExtensions
    {
        [DebuggerStepThrough]
        private static bool HasRole(this ClaimsPrincipal principal, params string[] roles)
        {
            if (principal == null)
                return default;

            var claims = principal.FindAll(ClaimTypes.Role).Select(x => x.Value).ToSafeList();

            return claims?.Any() == true && claims.Intersect(roles ?? new string[] { }).Any();
        }

        [DebuggerStepThrough]
        public static IEnumerable<ListItem> AuthorizeFor(this IEnumerable<ListItem> source, ClaimsPrincipal identity)
            => source.Where(x => x.Roles.IsNullOrEmpty() || (x.Roles.HasItems() && identity.HasRole(x.Roles))).ToSafeList();

        [DebuggerStepThrough]
        public static HtmlString AsRaw(this string value) => new HtmlString(value);

        [DebuggerStepThrough]
        public static string ToPage(this string href) => System.IO.Path.GetFileNameWithoutExtension(href)?.ToLower();

        [DebuggerStepThrough]
        public static bool IsVoid(this string href) => href?.ToLower() == NavigationModel.Void;

        [DebuggerStepThrough]
        public static bool IsRelatedTo(this ListItem item, string pageName) => item?.Type == ItemType.Parent && item?.Href?.ToPage() == pageName?.ToLower();

        [DebuggerStepThrough]
        public static async Task<IdentityResult> UpdateAsync<T>(this ApplicationDbContext context, T model, string id) where T : class
        {
            var entity = await context.FindAsync<T>(id);

            if (entity == null)
            {
                return IdentityResult.Failed();
            }

            context.Entry((object)entity).CurrentValues.SetValues(model);

            await context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        [DebuggerStepThrough]
        public static async Task<IdentityResult> DeleteAsync<T>(this ApplicationDbContext context, string id) where T : class
        {
            var entity = await context.FindAsync<T>(id);

            if (entity == null)
            {
                return IdentityResult.Failed();
            }

            context.Remove((object)entity);

            await context.SaveChangesAsync();

            return IdentityResult.Success;
        }
    }
}
