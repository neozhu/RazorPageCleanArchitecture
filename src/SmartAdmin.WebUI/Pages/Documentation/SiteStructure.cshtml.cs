using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class SiteStructureModel : PageModel
    {
        private readonly ILogger<SiteStructureModel> _logger;

        public SiteStructureModel(ILogger<SiteStructureModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
