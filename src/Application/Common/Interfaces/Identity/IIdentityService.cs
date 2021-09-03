using CleanArchitecture.Razor.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Razor.Application.Common.Interfaces.Identity
{
    public interface IIdentityService:IService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<IDictionary<string, string>> FetchUsers(string roleName);
    }
}
