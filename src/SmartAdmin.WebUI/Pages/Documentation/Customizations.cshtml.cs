using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class CustomizationsModel : PageModel
    {
        private readonly ILogger<CustomizationsModel> _logger;

        public CustomizationsModel(ILogger<CustomizationsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
