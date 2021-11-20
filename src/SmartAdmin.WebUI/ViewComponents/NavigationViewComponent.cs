using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Models;

namespace SmartAdmin.WebUI.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;
        private readonly string[] _roles;
        public NavigationViewComponent(
            UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService,
            IIdentityService  identityService
            )
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _identityService = identityService;
            var userId = _currentUserService.UserId;
            _roles = _userManager.Users.Where(x=>x.Id== userId).Include(x=>x.UserRoles).ThenInclude(x=>x.Role).SelectMany(x=>x.UserRoles).Select(x=>x.Role.Name).ToArray();
        }
        public IViewComponentResult Invoke()
        {
            
            var items = NavigationModel.GetNavigation(x=>!x.Roles.Any()  || (x.Roles.Any() && _roles.Any() && x.Roles.Where(x=>_roles.Contains(x)).Any()) );

            return View(items);
        }
    }
}
