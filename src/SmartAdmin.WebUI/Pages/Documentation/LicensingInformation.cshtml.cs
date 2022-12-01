using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class LicensingInformationModel : PageModel
    {
        private readonly ILogger<LicensingInformationModel> _logger;

        public LicensingInformationModel(ILogger<LicensingInformationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
