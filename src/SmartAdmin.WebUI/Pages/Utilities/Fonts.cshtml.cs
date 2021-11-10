using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class FontsModel : PageModel
    {
        private readonly ILogger<FontsModel> _logger;

        public FontsModel(ILogger<FontsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
