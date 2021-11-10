using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class ThrottleModel : PageModel
    {
        private readonly ILogger<ThrottleModel> _logger;

        public ThrottleModel(ILogger<ThrottleModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
