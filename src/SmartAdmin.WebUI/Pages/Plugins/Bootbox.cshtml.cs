using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Plugins
{
    public class BootboxModel : PageModel
    {
        private readonly ILogger<BootboxModel> _logger;

        public BootboxModel(ILogger<BootboxModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
