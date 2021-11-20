using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class HelpersModel : PageModel
    {
        private readonly ILogger<HelpersModel> _logger;

        public HelpersModel(ILogger<HelpersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
