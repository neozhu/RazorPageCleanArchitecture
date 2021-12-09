using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminLTE.WebUI.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdminLTE.WebUI.ViewComponents;

public class NavigationViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly bool _isManager;
    private readonly string[] _roles;
    public NavigationViewComponent(
        IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService,
        ICurrentUserService currentUserService,
        IIdentityService identityService
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
        _currentUserService = currentUserService;
        _identityService = identityService;
        var userId = _currentUserService.UserId;
        _roles = _currentUserService.GetRoles().ToArray();

        _isManager = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, "Manager").Result.Succeeded;
    }
    public IViewComponentResult Invoke()
    {

        var items = NavigationModel.GetNavigation(x => !x.Roles.Any() ||
                    (x.Roles.Any() && _roles.Any() && x.Roles.Where(x => _roles.Contains(x)).Any()) ||
                    (x.Roles.Any(x=>x=="Manager") && _isManager)
                    );

        return View(items);
    }
}
