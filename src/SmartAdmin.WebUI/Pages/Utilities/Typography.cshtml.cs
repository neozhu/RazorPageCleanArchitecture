using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class TypographyModel : PageModel
    {
        private readonly ILogger<TypographyModel> _logger;

        public TypographyModel(ILogger<TypographyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
