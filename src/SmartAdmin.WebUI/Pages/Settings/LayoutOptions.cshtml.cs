using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Settings
{
    public class LayoutOptionsModel : PageModel
    {
        private readonly ILogger<LayoutOptionsModel> _logger;

        public LayoutOptionsModel(ILogger<LayoutOptionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
