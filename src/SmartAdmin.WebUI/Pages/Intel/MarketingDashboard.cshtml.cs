using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Intel
{
    public class MarketingDashboardModel : PageModel
    {
        private readonly ILogger<MarketingDashboardModel> _logger;

        public MarketingDashboardModel(ILogger<MarketingDashboardModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
