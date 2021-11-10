using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
