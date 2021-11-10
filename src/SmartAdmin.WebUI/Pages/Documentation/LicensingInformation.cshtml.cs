using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
