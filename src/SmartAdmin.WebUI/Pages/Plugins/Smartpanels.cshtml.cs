using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class SmartpanelsModel : PageModel
    {
        private readonly ILogger<SmartpanelsModel> _logger;

        public SmartpanelsModel(ILogger<SmartpanelsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
