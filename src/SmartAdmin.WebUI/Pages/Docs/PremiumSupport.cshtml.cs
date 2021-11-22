using Microsoft.AspNetCore.Mvc.RazorPages;

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
