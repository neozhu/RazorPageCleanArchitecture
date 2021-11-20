using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Pages.Statistics
{
    public class ChartjsModel : PageModel
    {
        private readonly ILogger<ChartjsModel> _logger;

        public ChartjsModel(ILogger<ChartjsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
