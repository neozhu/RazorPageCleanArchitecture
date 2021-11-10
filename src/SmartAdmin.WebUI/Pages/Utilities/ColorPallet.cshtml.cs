using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class ColorPalletModel : PageModel
    {
        private readonly ILogger<ColorPalletModel> _logger;

        public ColorPalletModel(ILogger<ColorPalletModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
