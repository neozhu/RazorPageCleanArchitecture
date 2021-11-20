using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class SizingModel : PageModel
    {
        private readonly ILogger<SizingModel> _logger;

        public SizingModel(ILogger<SizingModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
