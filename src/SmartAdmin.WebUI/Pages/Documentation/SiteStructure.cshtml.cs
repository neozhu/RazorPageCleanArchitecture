using Microsoft.AspNetCore.Mvc.RazorPages;

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
