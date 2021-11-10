using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class ResponsiveGridModel : PageModel
    {
        private readonly ILogger<ResponsiveGridModel> _logger;

        public ResponsiveGridModel(ILogger<ResponsiveGridModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
