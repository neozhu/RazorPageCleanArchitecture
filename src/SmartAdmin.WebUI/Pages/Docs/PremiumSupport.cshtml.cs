using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class PremiumSupportModel : PageModel
    {
        private readonly ILogger<PremiumSupportModel> _logger;

        public PremiumSupportModel(ILogger<PremiumSupportModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
