using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class LicensingModel : PageModel
    {
        private readonly ILogger<LicensingModel> _logger;

        public LicensingModel(ILogger<LicensingModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
