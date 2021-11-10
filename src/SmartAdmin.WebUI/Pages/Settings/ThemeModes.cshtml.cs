using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Settings
{
    public class ThemeModesModel : PageModel
    {
        private readonly ILogger<ThemeModesModel> _logger;

        public ThemeModesModel(ILogger<ThemeModesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
