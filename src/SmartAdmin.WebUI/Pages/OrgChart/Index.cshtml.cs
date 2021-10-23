using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;

namespace SmartAdmin.WebUI.Pages.OrgChart
{
    [Authorize(policy: Permissions.OrgChart.View)]
    public class IndexModel : PageModel
    {
        private readonly IStringLocalizer<IndexModel> _localizer;

        public IndexModel(
            IStringLocalizer<IndexModel> localizer
            )
        {
            _localizer = localizer;
        }
        public   void OnGet()
        {
         
        }
        

      
        

    }
}
