using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class BordersModel : PageModel
    {
        private readonly ILogger<BordersModel> _logger;

        public BordersModel(ILogger<BordersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
