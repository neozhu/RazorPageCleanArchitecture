using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class ClearfixModel : PageModel
    {
        private readonly ILogger<ClearfixModel> _logger;

        public ClearfixModel(ILogger<ClearfixModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
