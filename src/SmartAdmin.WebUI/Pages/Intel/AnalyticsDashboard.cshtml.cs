using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Intel
{
    public class AnalyticsDashboardModel : PageModel
    {
        private readonly ILogger<AnalyticsDashboardModel> _logger;

        public AnalyticsDashboardModel(ILogger<AnalyticsDashboardModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
