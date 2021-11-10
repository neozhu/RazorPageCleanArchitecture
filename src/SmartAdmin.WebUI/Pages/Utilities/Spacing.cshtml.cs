using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class SpacingModel : PageModel
    {
        private readonly ILogger<SpacingModel> _logger;

        public SpacingModel(ILogger<SpacingModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
