using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    public class SettingsModel : PageModel
    {
        private readonly ILogger<SettingsModel> _logger;

        public SettingsModel(ILogger<SettingsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
