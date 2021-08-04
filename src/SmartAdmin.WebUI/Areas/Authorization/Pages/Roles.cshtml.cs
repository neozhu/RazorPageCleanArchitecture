using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Areas.Authorization.Pages
{
    [Authorize]
    public class RoleModel : PageModel
    {
    }
}
