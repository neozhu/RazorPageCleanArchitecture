using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class DisplayPropertyModel : PageModel
    {
        private readonly ILogger<DisplayPropertyModel> _logger;

        public DisplayPropertyModel(ILogger<DisplayPropertyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
